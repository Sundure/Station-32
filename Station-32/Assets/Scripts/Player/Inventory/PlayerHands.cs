using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public GameObject[] ItemSlots = new GameObject[Inventory.InventoryObjects.Length];

    private GameObject _currentItem;

    private Item _item;

    private int _previousSlot;

    private void Start()
    {
        for (int i = 0; i < Inventory.InventoryObjects.Length; i++)
        {
            GameObject gameObject = new();

            GameObject createdItem = Instantiate(gameObject, transform);

            createdItem.name = $"Slot {i + 1}";

            createdItem.SetActive(false);

            ItemSlots[i] = createdItem;
        }
    }

    private void Update()
    {
        if (_item != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _item.Use();
            }
        }
    }

    public void DropItem()
    {
        if (_item != null)
        {
            _currentItem.transform.parent = null;

            _item.RB.useGravity = true;
            _item.RB.isKinematic = false;

            Vector3 vector3 = (PlayerCamera.CreateRaycastPoint() - _item.RB.position).normalized;

            Vector3 force = vector3 * 100;

            _item.RB.AddForce(force);
        }
    }

    public void SwitchItem(GameObject gameObject)
    {
        ItemSlots[_previousSlot].SetActive(false);

        ItemSlots[Inventory.CurrentSlot].SetActive(true);

        _previousSlot = Inventory.CurrentSlot;

        if (gameObject != null)
        {
            _currentItem = gameObject;

            _item = _currentItem.GetComponent<Item>();

            return;
        }

        _currentItem = null;
        _item = null;
    }
}
