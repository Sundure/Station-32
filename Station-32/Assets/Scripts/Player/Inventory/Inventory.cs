using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static readonly GameObject[] InventoryObjects = new GameObject[PlayerStats.MaxInventorySlot];

    public static int CurrentSlot { get; private set; } = 0;

    [SerializeField] private PlayerHands _playerHands;

    public static event Action<int, GameObject> OnItemChange;

    private void Start()
    {
        InventoryInputSystem.ChangeItemSlot += SwitchItem;
        InventoryInputSystem.DropItem += DropItem;
        Item.AddItem += ChangeItem;
    }

    private void SwitchItem(int slot)
    {
        if (CurrentSlot == -1 || slot > InventoryObjects.Length)
        {
            return;
        }

        CurrentSlot = slot -1;

        if (CurrentSlot <= InventoryObjects.Length)
        {
            _playerHands.SwitchItem(InventoryObjects[CurrentSlot]);
        }

        OnItemChange?.Invoke(CurrentSlot, InventoryObjects[CurrentSlot]);
    }

    /// <summary>
    /// Change Selected Item With A New One
    /// </summary>
    /// <param name="item"></param>
    private void ChangeItem(GameObject item)
    {
        if (InventoryObjects[CurrentSlot] != null)
        {
            DropItem();
        }

        item.transform.parent = _playerHands.ItemSlots[CurrentSlot].transform;
        item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        InventoryObjects[CurrentSlot] = item;
    }

    private void DropItem()
    {
        _playerHands.DropItem();

        InventoryObjects[CurrentSlot] = null;
    }

    private void OnDestroy()
    {
        InventoryInputSystem.ChangeItemSlot -= SwitchItem;
        InventoryInputSystem.DropItem -= DropItem;
        Item.AddItem -= ChangeItem;
    }

}
