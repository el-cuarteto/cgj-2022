using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    // Which Character is stating this line in the
    // conversation.
    public Character character;

    // Text to display as part of this line.
    [TextArea(2, 5)]
    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
}
