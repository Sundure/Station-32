using UnityEngine;

public class Hands : MonoBehaviour
{
    private GameObject _gameObject;
    private Item _item;

    private void Update()
    {
        if (_item != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _item.Use();
            }
        }
    }

    public void DropItem()
    {
        _gameObject.transform.parent = null;

        Rigidbody rb = _gameObject.GetComponent<Rigidbody>();

        rb.AddForce(PlayerCamera.CreateRaycastPoint(),ForceMode.Force);
    }

    public void TakeItem(GameObject gameObject)
    {
        gameObject.transform.parent = gameObject.transform;

        _gameObject = gameObject;

        _item = _gameObject.GetComponent<Item>();
    }
}
