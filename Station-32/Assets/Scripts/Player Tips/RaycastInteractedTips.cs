using System;
using UnityEngine;

public class RaycastInteractedTips : MonoBehaviour
{
    public static event Action<InteractedType> OnRaycastHit;
    public static event Action OnRaycastUnhit;

    private GameObject _hitedGameObject;

    private float _timer;
    [SerializeField] private float _raycastCheckInteval;

    private bool _lastCheckResult;

    private void Awake()
    {
        Inventory.OnChangeItem += Unhit;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _raycastCheckInteval)
        {
            _timer = 0;

            TryHit();
        }
    }

    private void TryHit()
    {
        GameObject gameObject = PlayerCamera.GetGameObjectRaycast();

        if (_hitedGameObject == gameObject)
            return;

        _hitedGameObject = gameObject;

        if (_hitedGameObject != null && _hitedGameObject.TryGetComponent(out IInteracted interacted))
        {
            Hit(interacted);
        }
        else if (_lastCheckResult == true)
            Unhit();
    }

    private void Hit(IInteracted interacted)
    {
        if (interacted.InteractedTypes == InteractedType.Item)
        {
            GameObject itemObject = interacted is Component comp ? comp.gameObject : null; //Get GameObject From IInteracted Based Class

            Item item = itemObject.GetComponent<Item>();

            if (item.OnInventory)
                return;

            item.ItemBehaviorManager.EnableOutline();
        }

        OnRaycastHit?.Invoke(interacted.InteractedTypes);
        _lastCheckResult = true;

    }

    private void Unhit()
    {
        OnRaycastUnhit?.Invoke();
        _lastCheckResult = false;
    }

    private void OnDestroy()
    {
        Inventory.OnChangeItem -= Unhit;
    }
}
