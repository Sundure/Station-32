using UnityEngine;

public class InventoryVisibleChanger : MonoBehaviour
{
    [SerializeField] private float _fadeDelay;
    [SerializeField] private float _fadeTime;

    private float _dinamicFadeDelay;

    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        Inventory.OnAddItem += OnAddItem;
        Inventory.OnItemDrop += OnItemDrop;
        Inventory.OnSwitchItem += OnSwitchItem;
    }

    private void Update()
    {
        _dinamicFadeDelay -= Time.deltaTime;

        if (_dinamicFadeDelay < 0)
        {
            _canvasGroup.alpha -= Time.deltaTime / _fadeTime;

            if (_canvasGroup.alpha <= 0)
            {
                _canvasGroup.alpha = 0;

                enabled = false;
            }
        }
    }

    private void OnAddItem(int i, GameObject gameObject)
    {
        enabled = true;

        _dinamicFadeDelay = _fadeTime * 1.5f;

        _canvasGroup.alpha = 1;
    }
    private void OnItemDrop(int i)
    {
        enabled = true;

        _dinamicFadeDelay = _fadeTime;

        _canvasGroup.alpha = 1;
    }
    private void OnSwitchItem()
    {
        enabled = true;

        _dinamicFadeDelay = _fadeTime / 2;

        _canvasGroup.alpha = 1;
    }

    private void OnDestroy()
    {
        Inventory.OnAddItem -= OnAddItem;
        Inventory.OnItemDrop -= OnItemDrop;
        Inventory.OnSwitchItem -= OnSwitchItem;
    }
}
