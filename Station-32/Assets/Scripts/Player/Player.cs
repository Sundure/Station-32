using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;

    public void TakeDamage(float damage)
    {
        _playerStats.Health -= damage;

        if (_playerStats.Health <= 0)
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
        if (_playerStats.Heat <= 0)
        {
            _playerStats.Health -= Time.deltaTime;

            if (_playerStats.Health <= 0)
            {
                Die();
            }
        }
    }
}
