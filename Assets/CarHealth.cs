using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _health;

    private void Awake()
    {
        ResetHealth();
    }

    private void ResetHealth()
    {
        _health = _maxHealth;
    }

    public bool ApplyDamageAndTryDestroy(int damage)
    {
        _health -= damage;
        return _health <= 0;
    }
}
