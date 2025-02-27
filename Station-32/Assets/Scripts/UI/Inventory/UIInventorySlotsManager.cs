using UnityEngine;

public class UIInventorySlotsManager : MonoBehaviour
{
    private readonly GameObject[] _uiSlot = new GameObject[PlayerProperties.MAX_INVENTORY_SLOT];
    private readonly InventoryUISlot[] _inventorySlotsUI = new InventoryUISlot[PlayerProperties.MAX_INVENTORY_SLOT];

    [SerializeField] private GameObject _uiSlotPrefab;

    [SerializeField] private Transform _itemSlotFolder;

    private InventoryUISlot _previousChosenSlot;

    private void Start()
    {
        for (int i = 0; i < _uiSlot.Length; i++)
        {
            _uiSlot[i] = Instantiate(_uiSlotPrefab, _itemSlotFolder);

            _inventorySlotsUI[i] = _uiSlot[i].GetComponent<InventoryUISlot>();

            SetInventoryDefaultInventoryIcon(i);
        }

        SelectInventorySlot(0);

        Inventory.OnSwitchItem += SelectInventorySlot;
        Inventory.OnAddItem += SetInventorySlotProperties;
        Inventory.OnItemDrop += SetInventoryDefaultInventoryIcon;
    }

    private void SelectInventorySlot(int slot)
    {
        if (_inventorySlotsUI[slot] == _previousChosenSlot)
            return;

        Texture2D texture2D = new(1, 1);

        Color color = new(1, 1, 1, 0.2f);

        texture2D.SetPixel(0, 0, color);

        texture2D.Apply();

        _inventorySlotsUI[slot].SetFrameTexture(texture2D);

        SetDefaultPreviousInventoryIcon();

        _previousChosenSlot = _inventorySlotsUI[slot];
    }

    private void SetDefaultPreviousInventoryIcon()
    {
        if (_previousChosenSlot != null)
            _previousChosenSlot.SetDefaultFrameTexture();
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
        Inventory.OnSwitchItem -= SelectInventorySlot;
    }
}
