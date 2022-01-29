using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    // Name to show on the UI for this character.
    public string displayName;

    // Sprite to use as this character's portrait in the UI.
    public Sprite portrait;
}
