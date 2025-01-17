using System.Collections;
using UnityEngine;

public class InventoryItemManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject arm; // The arm GameObject
        public Animator animator; // Animator for the arm
    }

    public InventoryItem[] inventoryItems; // Array of all inventory items
    private int currentActiveIndex = -1; // Keeps track of the currently active item

    private void Update()
    {
        HandleScrollInput();
    }

    private void HandleScrollInput()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel"); // Get scroll input

        if (scrollDelta > 0f)
        {
            // Scroll up
            int nextIndex = (currentActiveIndex + 1) % inventoryItems.Length;
            ActivateItem(nextIndex);
        }
        else if (scrollDelta < 0f)
        {
            // Scroll down
            int previousIndex = (currentActiveIndex - 1 + inventoryItems.Length) % inventoryItems.Length;
            ActivateItem(previousIndex);
        }
    }

    public void ActivateItem(int index)
    {
        if (index < 0 || index >= inventoryItems.Length) return;

        // If there is an active item, play its sleep animation
        if (currentActiveIndex != -1)
        {
            StartCoroutine(DeactivateItemRoutine(index));
        }
        else
        {
            // Directly activate the new item if no item is active
            ActivateNewItem(index);
        }
    }

    private IEnumerator DeactivateItemRoutine(int newIndex)
    {
        // Get the currently active item
        InventoryItem currentItem = inventoryItems[currentActiveIndex];
        currentItem.animator.SetBool("isActive", false);

        // Wait for the sleep animation to finish
        yield return new WaitForSeconds(0.4f);

        // Deactivate the current item
        currentItem.arm.SetActive(false);

        // Activate the new item
        ActivateNewItem(newIndex);
    }

    private void ActivateNewItem(int index)
    {
        InventoryItem newItem = inventoryItems[index];
        newItem.arm.SetActive(true);
        currentActiveIndex = index;
    }
}
