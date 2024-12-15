using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlotsManager : MonoBehaviour
{
    private readonly GameObject[] _uiSlot = new GameObject[PlayerProperties.MaxInventorySlot];
    private readonly InventoryUISlot[] _inventorySlotsUI = new InventoryUISlot[PlayerProperties.MaxInventorySlot];

    [SerializeField] private GameObject _uiSlotPrefab;

    [SerializeField] private Transform _itemSlotFolder;

    private void Start()
    {
        for (int i = 0; i < _uiSlot.Length; i++)
        {
            _uiSlot[i] = Instantiate(_uiSlotPrefab, _itemSlotFolder);

            _inventorySlotsUI[i] = _uiSlot[i].GetComponent<InventoryUISlot>();

            SetInventoryDefaultInventoryIcon(i);
            AddFrame(_uiSlot[i]);
        }

        Inventory.OnAddItem += SetInventorySlotProperties;
        Inventory.OnItemDrop += SetInventoryDefaultInventoryIcon;
    }

    private void AddFrame(GameObject gameObject)
    {
        Texture2D texture = new(1, 1);

        Color color = new(0.5f, 0.5f, 0.5f, 0.3f);

        texture.SetPixel(0, 0, color);

        texture.Apply();


        GameObject frame = new() { name = "Frame" };

        frame.transform.parent = gameObject.transform;

        frame.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        frame.transform.localScale = Vector3.one;

        frame.AddComponent<RawImage>().texture = texture;
    }

    private void SetInventoryDefaultInventoryIcon(int slot)
    {
        Texture2D texture = new(1, 1);

        Color color = new(0, 0, 0, 0);

        texture.SetPixel(0, 0, color);

        texture.Apply();

        _inventorySlotsUI[slot].ChangeIcon(texture);
    }

    private void SetInventorySlotProperties(int slot, GameObject gameObject)
    {
        Item item = gameObject.GetComponent<Item>();

        _inventorySlotsUI[slot].ChangeIcon(item.ItemIcon);
    }

    private void OnDestroy()
    {
        Inventory.OnAddItem -= SetInventorySlotProperties;
        Inventory.OnItemDrop -= SetInventoryDefaultInventoryIcon;
    }
}
