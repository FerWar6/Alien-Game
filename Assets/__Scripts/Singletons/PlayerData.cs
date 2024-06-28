using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    // Singleton instance of PlayerData
    public static PlayerData instance { get; private set; }

    // Transform representing the player's position
    public Transform playerPos;

    // Player's health value
    public int playerHealth = 100;

    // List to keep track of inventory items (GameObjects)
    public List<GameObject> inventoryGameobjects;

    // Unity events to notify changes in player data
    public UnityEvent OnHealthChanged = new UnityEvent();
    public UnityEvent OnPlayerPosSet = new UnityEvent();
    public UnityEvent OnInventoryUpdate = new UnityEvent();
    public UnityEvent OnBreakerActive = new UnityEvent();
    public UnityEvent OnAlarmDeactivate = new UnityEvent();


    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Implementing the Singleton pattern to ensure only one instance of PlayerData exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the instance persists across scenes
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the inventory list
        inventoryGameobjects = new List<GameObject>();
    }

    // Method to add a GameObject to the inventory
    public void AddGameobjectToInventory(GameObject addGameObject)
    {
        inventoryGameobjects.Add(addGameObject);
        OnInventoryUpdate.Invoke(); // Trigger the inventory update event
    }

    // Method to destroy all items in the inventory
    public void DestroyAllItemsInInventory()
    {
        foreach (GameObject item in inventoryGameobjects)
        {
            Destroy(item); // Destroy each item
        }
        inventoryGameobjects.Clear(); // Clear the inventory list
    }

    // Method to check if the inventory contains an item by name
    public bool ListContainsItemByName(string name)
    {
        foreach (GameObject item in inventoryGameobjects)
        {
            if (item.name == name)
            {
                return true; // Return true if an item with the specified name is found
            }
        }
        return false; // Return false if no such item is found
    }

    // Method to modify the player's health
    public void ModifyPlayerHealth(int number, bool add)
    {
        if (add)
        {
            playerHealth += number; // Add to the player's health
        }
        else
        {
            playerHealth -= number; // Subtract from the player's health
            if (playerHealth <= 0)
                OnPlayerDead(); // Handle player's death if health drops to 0 or below
        }
        OnHealthChanged.Invoke(); // Trigger the health changed event
    }

    // Method to reset the player's health to 100
    public void ResetPlayerHealth()
    {
        playerHealth = 100;
        Debug.Log("Reset Health");
    }

    // Method to set the player's position
    public void SetPlayer(Transform player)
    {
        playerPos = player;
        OnPlayerPosSet.Invoke(); // Trigger the player position set event
    }

    // Method to handle player's death
    private void OnPlayerDead()
    {
        SceneManager.LoadScene("AllMenus"); // Load the specified scene
        ResetPlayerHealth(); // Reset player's health
    }
}
