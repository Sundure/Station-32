using UnityEngine;

public class JumpSystem : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private Player _player;

    [SerializeField] private float _jumpStrength;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speedDebuff;

    [SerializeField] private Vector3 _currentForce;

    private float _jumpTime; // value with decreasing which the flight force decreases

    private void Awake()
    {
        PlayerInputSystem.OnInputSpace += Jump;

        enabled = false;
    }

    private void Update()
    {
        if (_playerProperties.Jumped == true)
        {
            Vector3 pastPos = transform.position;

            _currentForce.y = Mathf.Sqrt(_jumpTime * _jumpStrength * Time.deltaTime);

            _jumpTime -= Time.deltaTime;

            _player.CharacterController.Move(Time.deltaTime * _playerProperties.PlayerFallSpeed * _currentForce);

            if (_jumpTime <= 0 || transform.position == pastPos)
            {
                _jumpTime = 0;

                _playerProperties.Jumped = false;

                enabled = false;

                PlayerMoveSystem.RemoveSpeedMultipler(this);
            }
        }
    }

    private void Jump()
    {
        if (_playerProperties.CanJump == false || _playerProperties.Crouch)
            return;

        _playerProperties.Grounded = false;
        _playerProperties.Jumped = true;

        _currentForce = Vector3.zero;

        _jumpTime = _jumpForce;

        enabled = true;

        PlayerMoveSystem.AddSpeedMultipler(this, _speedDebuff);
    }

    private void OnDestroy()
    {
        PlayerInputSystem.OnInputSpace -= Jump;
    }
}
