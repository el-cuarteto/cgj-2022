using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DualLayout", menuName = "Cuarteto/DualLayout")]
public class DualSerializedObject : ScriptableObject
{
    [SerializeField]
    private Texture2D _Mix;

    public Texture2D Mix => _Mix;

    [SerializeField]
    private Texture2D _Layout;

    public Texture2D Layout => _Layout;
}
