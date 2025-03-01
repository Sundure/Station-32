using UnityEngine;

public class PlayerFallSystem : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private Player _player;

    [SerializeField] private Transform _playerSphereCheckPoint;

    [SerializeField] private LayerMask _ignoredLayer;

    [SerializeField] private float _fallSpeedDebuff;

    private float _fallSpeed;

    private const float GROUND_CHECK_RADIUS = 0.5f;

    private void Update()
    {
        if (_playerProperties.Jumped)
        {
            _playerProperties.Grounded = false;
            _playerProperties.CanJump = false;

            _fallSpeed = 0;

            return;
        }

        if (Physics.CheckSphere(_playerSphereCheckPoint.position, GROUND_CHECK_RADIUS, ~_ignoredLayer))
        {
            _playerProperties.Grounded = true;
            _playerProperties.CanJump = true;

            _fallSpeed = 0;

            PlayerMoveSystem.RemoveSpeedMultipler(this);

            return;
        }

        _playerProperties.Grounded = false;
        _playerProperties.CanJump = false;

        _fallSpeed += Time.deltaTime / 2;

        _player.CharacterController.Move(Time.deltaTime * _playerProperties.PlayerFallSpeed * _fallSpeed * Vector3.down);

        PlayerMoveSystem.AddSpeedMultipler(this, _fallSpeedDebuff);
    }
}
