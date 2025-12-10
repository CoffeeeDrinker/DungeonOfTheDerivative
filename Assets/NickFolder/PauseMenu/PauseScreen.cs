using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    bool screenPaused;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Canvas canvas;
    [SerializeField] Inventory inventory;
    float playerSpeed;

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(inventory.InventoryOpen())
            {
                inventory.ToggleInventory();
                return;
            }

            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!screenPaused)
        {
            canvas.enabled = true;
            playerSpeed = playerMovement.speed;
            playerMovement.speed = 0;
            screenPaused = true;
        }
        else
        {
            canvas.enabled = false;
            playerMovement.speed = playerSpeed;
            screenPaused = false;
        }
    }
}
