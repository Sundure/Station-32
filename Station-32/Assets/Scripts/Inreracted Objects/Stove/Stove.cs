using UnityEngine;

public class Stove : MonoBehaviour
{
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _fuel;

    [SerializeField] private float _heatStrengh;

    private bool _enabled = false;

    private int _playerLayer;

    private void Start()
    {
        enabled = false;

        _playerLayer = LayerMask.NameToLayer("Player");
    }

    private void AddFuel(float fuel)
    {
        if (_fuel <= 0)
        {
            enabled = true;

            _enabled = true;
        }

        _fuel += fuel;

        _fuel = Mathf.Clamp(_fuel, 0, _maxFuel);
    }

    private void Update()
    {
        if (_fuel > 0)
        {
            _fuel -= Time.deltaTime;
        }
        else
        {
            enabled = false;

            _enabled = false;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (_enabled)
        {
            if (collision.collider.gameObject.layer == _playerLayer)
            {
               // Player.Heat += _heatStrengh;
            }
        }
    }
}
