using UnityEngine;

public class JumpSystem : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private Player _player;

    [SerializeField] private float _jumpStrength;

    [SerializeField] private float _jumpForce;

    private float _jumpTime; // value with decreasing which the flight force decreases

    [SerializeField] private Vector3 _currentForce;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_playerProperties.CanJump == false)
        {
            return;
        }

        _playerProperties.Grounded = false;
        _playerProperties.Jumped = true;

        _currentForce = Vector3.zero;

        _jumpTime = _jumpForce;
    }

    private void FixedUpdate()
    {
        if (_playerProperties.Jumped == true)
        {
            _currentForce.y = Mathf.Sqrt(_jumpTime * _jumpStrength * Time.fixedDeltaTime);

            _jumpTime -= Time.fixedDeltaTime;

            _player.CharacterController.Move(Time.fixedDeltaTime * _playerProperties.PlayerFallSpeed * _currentForce);

            if (_jumpTime <= 0)
            {
                _playerProperties.Jumped = false;
            }
        }
    }
}
