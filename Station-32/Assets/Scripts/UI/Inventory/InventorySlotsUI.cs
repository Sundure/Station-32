using UnityEngine;

public class InventorySlotsUI : MonoBehaviour
{
    private readonly GameObject[] _uiSlot = new GameObject[PlayerStats.MaxInventorySlot];

    [SerializeField] private GameObject _uiSlotPrefab;

    [SerializeField] private Transform _itemSlotFolder;

    private void Start()
    {
        for (int i = 0; i < _uiSlot.Length; i++)
        {
            _uiSlot[i] = Instantiate(_uiSlotPrefab, _itemSlotFolder);
        }
    }

    private void SetInventorySlotProperties()
    {

    }

    private void OnDestroy()
    {
        
    }
}
