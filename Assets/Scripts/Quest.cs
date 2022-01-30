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

    public bool givesItem = false;
    public ItemObject itemToGive;
    //public int givesItemStep;

    public bool expectsItem = false;
    public ItemObject expectedItem;
    //public int expectsItemStep;

    void Update()
    {
        // TODO: falta verificar que estés en proximidad
        // con el character
        if (Input.GetKeyDown(keyCode))
        {
            StartConversation();
        }
    }

    void StartConversation()
    {
        if (_currentConversationIndex == 0)
        {
            ShowConversation();
            _currentConversationIndex = 1;
        } else if (_currentConversationIndex == 1)
        {
            if (expectsItem && player.inventory.HasItem(expectedItem))
            {
                _currentConversationIndex = 2;
            }

            // will show 1 or 2
            ShowConversation();

            if (givesItem)
            {
                player.inventory.AddItem(itemToGive);
                _currentConversationIndex = 2;
            }

            if (expectsItem && _currentConversationIndex == 2)
            {
                _currentConversationIndex = 3;
            }
        }
        else
        {
            ShowConversation();
        }
    }

    void ShowConversation()
    {
        dialogDisplay.conversation = conversations[_currentConversationIndex];
        dialogDisplay.ShowDialogPanel();
    }
}
