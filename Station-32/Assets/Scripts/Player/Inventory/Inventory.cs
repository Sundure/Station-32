using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public readonly GameObject[] InventoryObjects = new GameObject[PlayerProperties.MaxInventorySlot];

    public int CurrentSlot { get; private set; } = 0;

    [SerializeField] private PlayerHands _playerHands;

    public static event Action<int, GameObject> OnAddItem;
    public static event Action<int> OnItemDrop;
    public static event Action<int> OnSwitchItem;

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

        CurrentSlot = slot - 1;

        if (CurrentSlot <= InventoryObjects.Length)
        {
            _playerHands.SwitchItem(InventoryObjects[CurrentSlot]);

            OnSwitchItem?.Invoke(CurrentSlot);
        }
    }

    /// <summary>
    /// Place Item In The Item Slot
    /// </summary>
    /// <param name="item"></param>
    private void ChangeItem(GameObject item)
    {
        if (InventoryObjects[CurrentSlot] != null)
            DropItem();
        
        item.transform.parent = _playerHands.ItemSlots[CurrentSlot].transform;
        item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        item.transform.Rotate(item.GetComponent<Item>().StandartRotation);

        InventoryObjects[CurrentSlot] = item;

        OnAddItem?.Invoke(CurrentSlot, item);

        _playerHands.SwitchItem(InventoryObjects[CurrentSlot]);

    }

    private void DropItem()
    {
        _playerHands.DropItem();

        if (InventoryObjects[CurrentSlot] != null)
            OnItemDrop?.Invoke(CurrentSlot);

        InventoryObjects[CurrentSlot] = null;

    }

    private void OnDestroy()
    {
        InventoryInputSystem.ChangeItemSlot -= SwitchItem;
        InventoryInputSystem.DropItem -= DropItem;
        Item.AddItem -= ChangeItem;
    }

}
