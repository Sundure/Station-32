using UnityEngine;

public class PlayerFallSystem : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private Player _player;

    [SerializeField] private Transform _playerFeet;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _jumpCheckRadius;

    [SerializeField] private LayerMask _ignoredLayer;

    [SerializeField] private float _speedDebuff;

    private float _fallSpeed;
    private void Update()
    {
        if (_playerProperties.Jumped)
        {
            _playerProperties.Grounded = false;
            _playerProperties.CanJump = false;

            _fallSpeed = 0;

            return;
        }

        _playerProperties.CanJump = Physics.CheckSphere(_playerFeet.position, _jumpCheckRadius, ~_ignoredLayer);
           

        if (Physics.CheckSphere(_playerFeet.position, _groundCheckRadius, ~_ignoredLayer))
        {
            _playerProperties.Grounded = true;

            _fallSpeed = 0;

            PlayerMoveSystem.RemoveSpeedMultipler(this);

            return;
        }

        _playerProperties.Grounded = false;

        _fallSpeed += Time.deltaTime / 2;

        _player.CharacterController.Move(Time.deltaTime * _playerProperties.PlayerFallSpeed * _fallSpeed * Vector3.down);

        PlayerMoveSystem.AddSpeedMultipler(this, _speedDebuff);
    }
}
