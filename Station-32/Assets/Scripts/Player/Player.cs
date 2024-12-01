using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;

    [SerializeField] private CharacterController _characterController;
    public CharacterController CharacterController { get { return _characterController; } }

    public void TakeDamage(float damage)
    {
        _playerProperties.Health -= damage;

        if (_playerProperties.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Console.WriteLine("Player Died");
    }

    private void Update()
    {
        if (_playerProperties.Heat <= 0)
        {
            _playerProperties.Health -= Time.deltaTime;

            if (_playerProperties.Health <= 0)
            {
                Die();
            }
        }
    }
}
