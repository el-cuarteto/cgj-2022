using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    // Reference to the player and its inventory
    public PlayerWithInventory player;

    // Reference to the DialogDisplay
    public DialogDisplay dialogDisplay;

    // Just for development
    public KeyCode keyCode;

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
        // TODO: falta verificar que estés en proximidad
        // con el character
        if (Input.GetKeyDown(keyCode))
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
