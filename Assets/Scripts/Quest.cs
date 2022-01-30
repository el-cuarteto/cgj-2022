using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    // Reference to the NPC game object we interact with
    public GameObject npcObject;

    // Reference to the player game object (3D character)
    public GameObject playerObject;
    public GameObject dualCamera;

    // Reference to the player and its inventory
    public PlayerWithInventory player;

    // Reference to the DialogDisplay
    public DialogDisplay dialogDisplay;

    private KeyCode _keyCode = KeyCode.E;

    // Conversations that expect should have 4 entries:
    // 0: initial conversation
    // 1: quest not finished
    // 2: expects item
    // 3: quest succeeded
    // Conversations that give items should have 3 entries:
    // 0: initial conversation
    // 1: gives item
    // 2: quest succeeded
    public List<Conversation> conversations = new List<Conversation>();
    private int _currentConversationIndex = 0;
    private int _previousConversationIndex = 0;
    private float _distanceThreshold = 3.5f;

    public bool givesItem = false;
    public ItemObject itemToGive;
    public int givesItemStep;

    public bool expectsItem = false;
    public ItemObject expectedItem;
    public int expectsItemStep;

    private void Start()
    {
        dialogDisplay.endOfDialogueAction = ContinueConversation;
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(npcObject.transform.position, playerObject.transform.position);
        float dualDistance = Vector3.Distance(npcObject.transform.position, dualCamera.transform.position);

        if ((playerDistance < _distanceThreshold || dualDistance < _distanceThreshold) && 
            Input.GetKeyDown(_keyCode))
        {
            StartConversation();
        }
    }

    private void increaseConversationIndex()
    {
        _currentConversationIndex = Mathf.Min(_currentConversationIndex + 1, conversations.Count-1);
    }

    private bool isTakingItem()
    {
        return expectsItem && _currentConversationIndex == expectsItemStep && player.inventory.HasItem(expectedItem);
    }

    private bool isGivingItem()
    {
        return givesItem && _currentConversationIndex == givesItemStep;
    }

    void StartConversation()
    {
        ShowConversation();
        _previousConversationIndex = _currentConversationIndex;
        if (_currentConversationIndex == 0)
        {
            increaseConversationIndex();
        }
        else if (isTakingItem())
        {
            increaseConversationIndex();
            player.inventory.RemoveItem(expectedItem);
        }
        else if (isGivingItem())
        {
            increaseConversationIndex();
            player.inventory.AddItem(itemToGive);
        }
    }

    private void ContinueConversation()
    {
        if (_previousConversationIndex != _currentConversationIndex && (isTakingItem() || isGivingItem()))
        {
            StartConversation();
        }
    }

    void ShowConversation()
    {
        // We have to assign the action again so that it can evaluate new values of the fields
        dialogDisplay.endOfDialogueAction = ContinueConversation;

        dialogDisplay.conversation = conversations[_currentConversationIndex];
        dialogDisplay.ShowDialogPanel();
    }
}
