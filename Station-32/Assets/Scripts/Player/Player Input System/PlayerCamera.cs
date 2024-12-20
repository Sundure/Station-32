using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _body;

    [SerializeField] private float _mouseSens;

    private float _xRotation;

    private static Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        PlayerInputSystem.OnAxisCameraInputXY += Move;
    }

    private void Move(float x, float y)
    {
        x *= _mouseSens * Time.timeScale;
        y *= _mouseSens * Time.timeScale;

        _xRotation -= y;

        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        _body.Rotate(x * Vector3.up);
    }

    /// <summary>
    /// Return Raycast Hit Point From Camera Ray 
    /// </summary>
    /// <returns></returns>
    public static Vector3 CreateRaycastPoint()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        return ray.GetPoint(PlayerProperties.InteractRange);
    }

    /// <summary>
    /// Return Component From Camera Ray
    /// </summary>
    /// <returns></returns>
    public static T GetComponentRaycast<T>() where T : class
    {
        Ray ray = _camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, PlayerProperties.InteractRange))
        {
           if (hit.collider.TryGetComponent(out T component))
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
