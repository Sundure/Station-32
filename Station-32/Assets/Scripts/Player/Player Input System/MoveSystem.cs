using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] Player _player;

    [SerializeField] private float _speed;

    private void Update()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector3 vector3 = transform.right * x + transform.forward * y;

        _player.CharacterController.Move(0.1f * _speed * Time.deltaTime * vector3);
    }
}
