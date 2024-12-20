using UnityEngine;

public class JumpSystem : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private Player _player;

    [SerializeField] private float _jumpStrength;

    [SerializeField] private float _jumpForce;

    private float _jumpTime; // value with decreasing which the flight force decreases

    [SerializeField] private Vector3 _currentForce;

    private void Awake()
    {
        PlayerInputSystem.OnInputSpace += Jump;

        enabled = false;
    }

    private void Update()
    {
        if (_playerProperties.Jumped == true)
        {
            _currentForce.y = Mathf.Sqrt(_jumpTime * _jumpStrength * Time.deltaTime);

            _jumpTime -= Time.deltaTime;

            _player.CharacterController.Move(Time.deltaTime * _playerProperties.PlayerFallSpeed * _currentForce);

            if (_jumpTime <= 0)
            {
                _playerProperties.Jumped = false;

                enabled = false;
            }
        }
    }

    private void Jump()
    {
        if (_playerProperties.CanJump == false)
            return;

        _playerProperties.Grounded = false;
        _playerProperties.Jumped = true;

        _currentForce = Vector3.zero;

        _jumpTime = _jumpForce;

        enabled = true;
    }

    private void OnDestroy()
    {
        PlayerInputSystem.OnInputSpace -= Jump;
    }
}
