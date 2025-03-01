using System;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    public static event Action OnInputDownFire1;
    public static event Action OnInputFire1;
    public static event Action OnInputInteract;
    public static event Action OnInputDrop;

    public static event Action OnInputSpace;
    public static event Action OnInputControl;
    public static event Action OnInputF;

    public static event Action<float, float> OnAxisMoveInputXY;
    public static event Action<float, float> OnAxisCameraInputXY;

    public static event Action<byte> OnInputNumber;

    private void Update()
    {
        if (PauseManager.Pause)
            return;

        print(Time.timeScale);

        GetInputSystemInput();
        GetKeyDownInput();
        GetAxisInput();
        GetNumberInput();
    }

    private void GetInputSystemInput()
    {
        if (Input.GetButton("Fire1"))
            OnInputFire1?.Invoke();

        if (Input.GetButtonDown("Fire1"))
            OnInputDownFire1?.Invoke();

        if (Input.GetButtonDown("Interact"))
            OnInputInteract?.Invoke();

        if (Input.GetButtonDown("Drop"))
            OnInputDrop?.Invoke();
    }

    private void GetKeyDownInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            OnInputControl?.Invoke();

        if (Input.GetKeyDown(KeyCode.Space))
            OnInputSpace?.Invoke();

        if (Input.GetKeyDown(KeyCode.F))
            OnInputF?.Invoke();
    }

    private void GetAxisInput()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        OnAxisMoveInputXY?.Invoke(x, y);

        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        OnAxisCameraInputXY?.Invoke(x, y);
    }
    private void GetNumberInput()
    {
        string input = Input.inputString;

        if (!string.IsNullOrEmpty(input) && byte.TryParse(input, out byte number))
        {
            if (number == 0) return;

            OnInputNumber?.Invoke(number);
        }
    }
}
