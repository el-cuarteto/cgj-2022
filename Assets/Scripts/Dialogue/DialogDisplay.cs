using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogDisplay : MonoBehaviour
{
    // TODO: Quitar ultima ventana vacía
    public List<Conversation> conversations = new List<Conversation>();
    public Text dialog;

    public PlayerWithInventory player;
    public ItemObject itemToGive;

    public GameObject speakerLeft;
    public GameObject speakerRight;
    public GameObject dialogPanel;

    private SpeakerUI _speakerUILeft;
    private SpeakerUI _speakerUIRight;

    private int _activeLineIndex = 0;
    private bool _isActiveDialog = true;
    private Conversation _currentConversation;
    private string _keyToPress = "space";
    private float _dialogDelay = 0.05f;

    public string Dialog
    {
        set { dialog.text = value; }
    }

    void Start()
    {
        _speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        _speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        _currentConversation = conversations[0];
        SetSpeakers();

        dialogPanel.SetActive(false);
    }

    void SetSpeakers()
    {
        _speakerUILeft.Speaker = _currentConversation.speakerLeft;
        _speakerUIRight.Speaker = _currentConversation.speakerRight;
    }

    void ChangeConversation(int conversationIndex)
    {
        _currentConversation = conversations[conversationIndex];
        _activeLineIndex = 0;
        _isActiveDialog = true;
        SetSpeakers();
    }

    void Update()
    {
        if (Input.GetKeyDown(_keyToPress))
        {
            if (_isActiveDialog)
            {
                // TODO: Accept item only if it is not the first dialogue and itemToAccept != null
                AdvanceConversation();
                dialogPanel.SetActive(true);
            }
            else
            {
                HideDialogPanel();
            }
        }
    }

    void AdvanceConversation()
    {
        if (_activeLineIndex < _currentConversation.lines.Length)
        {
            DisplayLine();
            _activeLineIndex += 1;
        }
        else
        {
            _speakerUILeft.Hide();
            _speakerUIRight.Hide();
            _activeLineIndex = 0;
            _isActiveDialog = false;

            // TODO: Give item only if it is non-null
            player.inventory.AddItem(itemToGive);

            HideDialogPanel();
        }
    }

    void HideDialogPanel()
    {
        Dialog = "";
        dialogPanel.SetActive(false);
    }

    void DisplayLine()
    {
        Line line = _currentConversation.lines[_activeLineIndex];
        Character character = line.character;

        if (_speakerUILeft.SpeakerIs(character))
        {
            StartCoroutine(SetDialog(_speakerUILeft, _speakerUIRight, line.text));
        }
        else
        {
            StartCoroutine(SetDialog(_speakerUIRight, _speakerUILeft, line.text));
        }
    }

    IEnumerator SetDialog(
        SpeakerUI activeSpeakerUI,
        SpeakerUI inactiveSpeakerUI,
        string fullText
    ) {
        // Activating the right UI
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();

        // Displaying each letter after a delay
        for(int i = 0; i <= fullText.Length; i++) {
            yield return new WaitForSeconds(_dialogDelay);
            Dialog = fullText.Substring(0, i);
        }
    }
}
