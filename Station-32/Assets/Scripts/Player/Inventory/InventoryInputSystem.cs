using System;
using UnityEngine;

public class InventoryInputSystem : MonoBehaviour
{
    public static event Action<int> ChangeItemSlot;
    public static event Action DropItem;

    private void Update()
    {
        string input = Input.inputString;

        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int slotNumber))
        {
            if (slotNumber == 0)
            {
                return;
            }

            ChangeItemSlot?.Invoke(slotNumber--);
        }
        if (Input.GetButtonDown("Drop"))
        {
            DropItem?.Invoke();
        }
    }
}
