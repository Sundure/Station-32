using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _cameraPosition;

    [SerializeField] private float _mouseSens;

    private float _xRotation;

    private static Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        PlayerInputSystem.OnAxisCameraInputXY += Move;
    }

    private void Update()
    {
        transform.localPosition = _cameraPosition.localPosition * _cameraPosition.lossyScale.y;
    }

    private void Move(float x, float y)
    {
        x *= _mouseSens * Time.timeScale;
        y *= _mouseSens * Time.timeScale;

        _xRotation -= y;

        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        _player.Rotate(x * Vector3.up);
    }

    /// <summary>
    /// Return GameObject From Raycast Hit
    /// </summary>
    /// <returns></returns>
    public static GameObject GetGameObjectRaycast()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, PlayerProperties.INTERACT_RANGE))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    /// <summary>
    /// Return Raycast Hit Point From Camera Ray 
    /// </summary>
    /// <returns></returns>
    public static Vector3 CreateRaycastPoint()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        return ray.GetPoint(PlayerProperties.INTERACT_RANGE);
    }

    /// <summary>
    /// Return Component From Camera Ray
    /// </summary>
    /// <returns></returns>
    public static TComponent GetComponentRaycast<TComponent>() where TComponent : class
    {
        Ray ray = _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, PlayerProperties.INTERACT_RANGE))
        {
           if (hit.collider.TryGetComponent(out TComponent component))
           {
               return component;
           }
        }

        return default;
    }

    private void OnDestroy()
    {
        PlayerInputSystem.OnAxisCameraInputXY -= Move;
    }
}
