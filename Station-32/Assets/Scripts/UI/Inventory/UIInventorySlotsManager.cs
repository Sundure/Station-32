using UnityEngine;

public class UIInventorySlotsManager : MonoBehaviour
{
    private readonly GameObject[] _uiSlot = new GameObject[PlayerStats.MaxInventorySlot];
    private readonly InventoryUISlot[] _inventorySlotsUI = new InventoryUISlot[PlayerStats.MaxInventorySlot];

    [SerializeField] private GameObject _uiSlotPrefab;

    [SerializeField] private Transform _itemSlotFolder;

    private void Start()
    {
        for (int i = 0; i < _uiSlot.Length; i++)
        {
            _uiSlot[i] = Instantiate(_uiSlotPrefab, _itemSlotFolder);

            _inventorySlotsUI[i] = _uiSlot[i].GetComponent<InventoryUISlot>();
        }
    }

    private void SetInventorySlotProperties(int slot, GameObject gameObject)
    {
        Item item = gameObject.GetComponent<Item>();

        _inventorySlotsUI[slot].ChangeIcon(item.ItemIcon);
    }

    private void OnDestroy()
    {
        
    }
}
