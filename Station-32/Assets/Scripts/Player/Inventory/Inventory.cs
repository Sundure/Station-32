using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static readonly GameObject[] _inventory = new GameObject[PlayerStats.MaxInventorySlot];

    private static int _currentSlot = 1;

    [SerializeField] private Hands _hands;

    public static event Action OnItemChange;

    private void Start()
    {
        InventoryInputSystem.ChangeItemSlot += ChangeItem;
        InventoryInputSystem.DropItem += DropItem;
        Item.AddItem += SwitchItem;
    }

    private void ChangeItem(int slot)
    {
        if (_currentSlot == -1)
        {
            return;
        }

        if (_currentSlot <= _inventory.Length)
        {
            if (_inventory[_currentSlot] != null)
            {
                _hands.TakeItem(_inventory[_currentSlot]);
            }
        }

        _currentSlot = slot;

        OnItemChange?.Invoke();
    }

    /// <summary>
    /// Switch Selected Item With A New One
    /// </summary>
    /// <param name="item"></param>
    public void SwitchItem(GameObject item)
    {
        if (_inventory[_currentSlot] != null)
        {
            DropItem();
        }

       item.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        _inventory[_currentSlot] = item;
    }

    private void DropItem()
    {
        _hands.DropItem();

        _inventory[_currentSlot] = null;
    }

    private void OnDestroy()
    {
        InventoryInputSystem.ChangeItemSlot -= ChangeItem;
        InventoryInputSystem.DropItem -= DropItem;
        Item.AddItem -= SwitchItem;
    }

}
