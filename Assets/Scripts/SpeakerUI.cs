using UnityEngine;
using UnityEngine.UI;

public class SpeakerUI : MonoBehaviour
{
    public Image portrait;
    public Text displayName;

    private Character _speaker;
    public Character Speaker
    {
        get { return _speaker; }
        set {
            _speaker = value;
            portrait.sprite = _speaker.portrait;
            displayName.text = _speaker.displayName;
        }
    }

    public bool SpeakerIs(Character character)
    {
        return _speaker == character;
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