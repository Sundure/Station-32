using System;
using UnityEngine;

public class RaycastInteractedTips : MonoBehaviour
{
    public static event Action OnRaycastHit;
    public static event Action OnRaycastUnhit;

    private GameObject _hitedGameObject;

    private float _timer;
    [SerializeField] private float _raycastCheckInteval;

    private bool _lastCheckResult;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _raycastCheckInteval)
        {
            _timer = 0;

            GameObject gameObject = PlayerCamera.GetGameObjectRaycast();

            if (_hitedGameObject == gameObject)
                return;

            _hitedGameObject = gameObject;

            if (_hitedGameObject != null && _hitedGameObject.TryGetComponent(out IInteracted _))
            {
                OnRaycastHit?.Invoke();
                _lastCheckResult = true;
            }
            else if (_lastCheckResult == true)
            {
                OnRaycastUnhit?.Invoke();
               _lastCheckResult = false;
            }
        }
    }
}
