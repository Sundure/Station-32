using UnityEngine;

public class PlayerGroundCheckSystem : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private Player _player;

    [SerializeField] private Transform _playerFeet;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _jumpCheckRadius;

    [SerializeField] private LayerMask _ignoredLayer;

    [SerializeField] private float _fallSpeed;

    private void FixedUpdate()
    {
        if (_playerProperties.Jumped)
        {
            _playerProperties.Grounded = false;
            _playerProperties.CanJump = false;

            _fallSpeed = 0;

            return;
        }

        if (Physics.CheckSphere(_playerFeet.position, _jumpCheckRadius, ~_ignoredLayer))
        {
            _playerProperties.CanJump = true;
        }
        else
            _playerProperties.CanJump = false;


        if (Physics.CheckSphere(_playerFeet.position, _groundCheckRadius, ~_ignoredLayer))
        {
            _playerProperties.Grounded = true;

            _fallSpeed = 0;

            return;
        }

        _playerProperties.Grounded = false;

        _fallSpeed += Time.fixedTime / 250;

        _player.CharacterController.Move(Time.fixedDeltaTime * _playerProperties.PlayerFallSpeed * _fallSpeed * Vector3.down);
    }
}
