using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogDisplay : MonoBehaviour
{
    public UnityEvent onConversationStart = new UnityEvent();
    public UnityEvent onConversationEnd = new UnityEvent();

    public Text dialog;
    private IEnumerator _activeCoroutine;

    public GameObject speakerLeft;
    public GameObject speakerRight;
    public GameObject dialogPanel;

    private SpeakerUI _speakerUILeft;
    private SpeakerUI _speakerUIRight;

    private int _activeLineIndex = 0;
    private bool _isActiveDialog = false;

    // This is an action provided by the quest
    public System.Action endOfDialogueAction;

    private Conversation _currentConversation;
    public Conversation conversation {
        set {
            _currentConversation = value;
            _activeLineIndex = 0;
            _isActiveDialog = true;
            SetSpeakers();
        }
    }
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

        dialogPanel.SetActive(false);
    }

    void SetSpeakers()
    {
        _speakerUILeft.Speaker = _currentConversation.speakerLeft;
        _speakerUIRight.Speaker = _currentConversation.speakerRight;
    }

    void Update()
    {
        if (Input.GetKeyDown(_keyToPress))
        {
            if (_activeCoroutine != null) {
                StopCoroutine(_activeCoroutine);
            }

            if (_isActiveDialog)
            {
                AdvanceConversation();
            }
            else
            {
                HideDialogPanel();
            }
        }
    }


    void AdvanceConversation()
    {
        if (!_isActiveDialog) return;

        if (_activeLineIndex < _currentConversation.lines.Length)
        {
            DisplayLine();
            _activeLineIndex += 1;
        }
        else
        {
            _isActiveDialog = false;
            if (endOfDialogueAction != null)
            {
                endOfDialogueAction();
            }

            // It can be activated in the endOfDialogueAction (the dialogue continues)
            if (!_isActiveDialog)
            {
                _speakerUILeft.Hide();
                _speakerUIRight.Hide();
                _activeLineIndex = 0;
                HideDialogPanel();
            }
        }
    }

    void HideDialogPanel()
    {
        dialogPanel.SetActive(false);
        onConversationEnd.Invoke();
        Dialog = "";
    }

    public void ShowDialogPanel()
    {
        dialogPanel.SetActive(true);
        onConversationStart.Invoke();
        AdvanceConversation();
    }


    void DisplayLine()
    {
        Line line = _currentConversation.lines[_activeLineIndex];
        Character character = line.character;

        if (_speakerUILeft.SpeakerIs(character))
        {
            _activeCoroutine = SetDialog(_speakerUILeft, _speakerUIRight, line.text);
        }
        else
        {
            _activeCoroutine = SetDialog(_speakerUIRight, _speakerUILeft, line.text);
        }
        StartCoroutine(_activeCoroutine);
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
