using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private CharacterController _characterController;

    private void Update()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector3 vector3 = transform.right * x + transform.forward * y;


        _characterController.Move(0.1f * _speed * Time.deltaTime * vector3);
    }
}
