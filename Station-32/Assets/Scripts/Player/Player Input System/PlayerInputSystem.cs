using System;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    public static event Action OnInputSpace;
    public static event Action OnInputControl;
    public static event Action OnInputF;

    public static event Action<float, float> OnAxisMoveInputXY;
    public static event Action<float, float> OnAxisCameraInputXY;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnInputControl?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnInputSpace?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnInputF?.Invoke();
        }

        GetAxisInput();
    }

    private void GetAxisInput()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        OnAxisMoveInputXY?.Invoke(x, y);

        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        OnAxisCameraInputXY?.Invoke(x,y);
    }
}
