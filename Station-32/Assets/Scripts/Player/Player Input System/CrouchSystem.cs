using UnityEngine;

public class CrouchSystem : MonoBehaviour
{
    [SerializeField] private bool _crouch;

    [SerializeField] private float _crouchVelocity;

    [SerializeField] private float _crouchSpeedMultilier;

    private void Awake()
    {
        PlayerInputSystem.OnInputControl += SwitchCrouch;

        enabled = false;
    }

    private void Update()
    {
        if (_crouch)
        {
            Crouch();
        }
        else
        {
            StandUp();
        }
    }

    private void SwitchCrouch()
    {
        if (_crouch == false)
        {
            SetCrouch();
        }
        else
        {
            // Absolutely Broken System

            Vector3 crouchCheckPosition = transform.position;

            crouchCheckPosition.y = transform.position.y + transform.position.y / 2;

            if (!Physics.Raycast(crouchCheckPosition, Vector3.up, out RaycastHit _, _crouchVelocity + 0.3f))
                SetStandUp();
        }
    }

    private void SetCrouch()
    {
        _crouch = true;

        MoveSystem.AddSpeedMultipler(this, _crouchSpeedMultilier);

        enabled = true;
    }

    private void SetStandUp()
    {
        _crouch = false;

        MoveSystem.RemoveSpeedMultipler(this);

        enabled = true;
    }

    private void StandUp()
    {
        Vector3 vector3 = transform.localScale;

        vector3.y += Time.deltaTime;

        float previousHeight = transform.localScale.y;

        transform.localScale = vector3;

        if (vector3.y >= 1)
        {
            vector3.y = 1;

            transform.localScale = vector3;

            enabled = false;
        }

        float sizeChange = previousHeight - vector3.y;

        vector3 = transform.position;

        vector3.y -= sizeChange;

        transform.position = vector3;
    }

    private void Crouch()
    {
        Vector3 vector3 = transform.localScale;

        vector3.y -= Time.deltaTime;

        float previousHeight = transform.localScale.y;

        transform.localScale = vector3;

        if (vector3.y <= _crouchVelocity)
        {
            vector3.y = _crouchVelocity;

            transform.localScale = vector3;

            enabled = false;
        }

        float sizeChange = previousHeight - vector3.y;

        vector3 = transform.position;

        vector3.y -= sizeChange;

        transform.position = vector3;
    }

    private void OnDestroy()
    {
        PlayerInputSystem.OnInputControl -= SwitchCrouch;
    }
}
