using System;
using UnityEngine;

public class CrouchSystem : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private float _crouchVelocity;
    [SerializeField] private float _crouchSpeed;

    [Header("Player Speed Multiplier")]
    [SerializeField] private float _crouchSpeedMultilier;

    public event Action<bool> PlayerCrouch;

    private void Awake()
    {
        PlayerInputSystem.OnInputControl += SwitchCrouch;

        enabled = false;
    }

    private void Update()
    {
        if (_playerProperties.Crouch)
            Crouch();

        else
            StandUp();
    }

    private void SwitchCrouch()
    {
        if (_playerProperties.Grounded == false)
            return;


        if (_playerProperties.Crouch == false)
            SetCrouch();
        else
        {
            float velocity = _playerProperties.PlayerCollider.bounds.size.y;

            Vector3 crouchCheckPosition = transform.position;
            crouchCheckPosition.y = transform.position.y + velocity / 2;

            if (!Physics.Raycast(crouchCheckPosition, Vector3.up, out RaycastHit _, velocity))
                SetStandUp();
        }
    }

    private void SetCrouch()
    {
        _playerProperties.Crouch = true;

        PlayerMoveSystem.AddSpeedMultipler(this, _crouchSpeedMultilier);

        PlayerCrouch?.Invoke(_playerProperties.Crouch);

        enabled = true;
    }

    private void SetStandUp()
    {
        _playerProperties.Crouch = false;

        PlayerMoveSystem.RemoveSpeedMultipler(this);

        PlayerCrouch?.Invoke(_playerProperties.Crouch);

        enabled = true;
    }

    private void StandUp()
    {
        Vector3 vector3 = transform.localScale;

        vector3.y += Time.deltaTime * _crouchSpeed;

        float previousHeight = _playerProperties.PlayerCollider.bounds.size.y;

        transform.localScale = vector3;

        if (vector3.y >= 1)
        {
            vector3.y = 1;

            transform.localScale = vector3;

            enabled = false;
        }

        float moveDistance = (_playerProperties.PlayerCollider.bounds.size.y - previousHeight) / 2;

        _player.CharacterController.Move(new(0, moveDistance, 0));
    }

    private void Crouch()
    {
        Vector3 vector3 = transform.localScale;

        vector3.y -= Time.deltaTime * _crouchSpeed;

        float previousHeight = _playerProperties.PlayerCollider.bounds.size.y;

        transform.localScale = vector3;

        if (vector3.y <= _crouchVelocity)
        {
            vector3.y = _crouchVelocity;

            transform.localScale = vector3;

            enabled = false;
        }

        float moveDistance = (_playerProperties.PlayerCollider.bounds.size.y - previousHeight) / 2;

        _player.CharacterController.Move(new(0, moveDistance, 0));
    }

    private void OnDestroy()
    {
        PlayerInputSystem.OnInputControl -= SwitchCrouch;
    }
}
