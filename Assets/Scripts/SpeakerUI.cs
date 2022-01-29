using UnityEngine;
using UnityEngine.UI;

public class SpeakerUI : MonoBehaviour
{
    public Image portrait;
    public Text displayName;
    public Text dialog;

    private Character speaker;
    public Character Speaker
    {
        get { return speaker; }
        set {
            speaker = value;
            portrait.sprite = speaker.portrait;
            displayName.text = speaker.displayName;
        }
    }

    public string Dialog
    {
        set { dialog.text = value; }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}