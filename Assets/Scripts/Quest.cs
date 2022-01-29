using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    // Reference to the player and its inventory
    public PlayerWithInventory player;

    // Reference to the DialogDisplay
    public DialogDisplay dialogDisplay;

    // Conversations should have 4 entries:
    // 0: initial conversation
    // 1: quest not finished
    // 2: gives / receives item
    // 3: quest succeeded
    public List<Conversation> conversations = new List<Conversation>();
    private int _currentConversationIndex = 0;

    public bool givesItem = false;
    public ItemObject itemToGive;

    public bool expectsItem = false;
    public ItemObject expectedItem;

    void Update()
    {
        if (Input.GetKeyDown("e"))
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

            if (_currentConversationIndex == 2)
            {
                if (givesItem)
                {
                    player.inventory.AddItem(itemToGive);
                }
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
