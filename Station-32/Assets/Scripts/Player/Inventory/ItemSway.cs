using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [SerializeField] float _swayMultiplier;
    [SerializeField] float _smoth;

    private void Update()
    {

        float x = Input.GetAxis("Mouse X") * _swayMultiplier * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * _swayMultiplier * Time.deltaTime;

        Quaternion rotY = Quaternion.AngleAxis(y, Vector3.right);
        Quaternion rotX = Quaternion.AngleAxis(-x, Vector3.up);

        Quaternion targetRot = rotX * rotY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, _smoth * Time.deltaTime);
    }
}
