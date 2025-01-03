using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public GameObject[] ItemSlots = new GameObject[PlayerProperties.MaxInventorySlot];

    private GameObject _currentItem;

    private Item _item;

    private int _previousSlot;

    private void Start()
    {
        for (int i = 0; i < _inventory.InventoryObjects.Length; i++)
        {
            GameObject createdItem = new();

            createdItem.transform.parent = transform;

            createdItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            createdItem.name = $"Slot {i + 1}";

            createdItem.SetActive(false);

            ItemSlots[i] = createdItem;

            SwitchItem(null);
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
            Debug.Log("Drop");
            _currentItem.transform.parent = null;

            _item.RB.useGravity = true;
            _item.RB.isKinematic = false;

            _item.IgnoredRBLayers.AddIgnoredRBLayer(PlayerProperties.PlayerLayer, 0.5f);

            Vector3 vector3 = (PlayerCamera.CreateRaycastPoint() - _item.RB.position).normalized;

            Vector3 force = vector3 * 100;

            _item.RB.AddForce(force);

            _item = null;
        }
    }

    public void SwitchItem(GameObject gameObject)
    {
        ItemSlots[_previousSlot].SetActive(false);

        ItemSlots[_inventory.CurrentSlot].SetActive(true);

        _previousSlot = _inventory.CurrentSlot;

        if (gameObject != null)
        {
            _currentItem = gameObject;

            _item = _currentItem.GetComponent<Item>();
            _item.IgnoredRBLayers.AddIgnoredRBLayer(PlayerProperties.PlayerLayer);

            return;
        }

        _currentItem = null;
        _item = null;
    }
}
