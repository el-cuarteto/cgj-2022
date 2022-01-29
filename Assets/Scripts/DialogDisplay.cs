using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;
    public Text dialog;


    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI _speakerUILeft;
    private SpeakerUI _speakerUIRight;

    private int _activeLineIndex = 0;
    private bool _isActiveDialog = true;
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

        _speakerUILeft.Speaker = conversation.speakerLeft;
        _speakerUIRight.Speaker = conversation.speakerRight;
    }

    void Update()
    {
        if (_isActiveDialog && Input.GetKeyDown(_keyToPress))
        {
            AdvanceConversation();
        }
    }

    void AdvanceConversation()
    {
        if (_activeLineIndex < conversation.lines.Length)
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
        }
    }

    void DisplayLine()
    {
        Line line = conversation.lines[_activeLineIndex];
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
