using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // TODO: Map NPC to dialogue
    public List<DialogDisplay> dialogues = new List<DialogDisplay>();
    private int _dialogueIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && (_dialogueIndex < 0 || !dialogues[_dialogueIndex].isActiveDialog))
        {
            Instantiate(dialogues[++_dialogueIndex]);
        }
    }
}
