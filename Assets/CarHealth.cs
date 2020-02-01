using UnityEngine;

public class CarHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _health;

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
    }

    public bool ApplyDamageAndTryDestroy(int damage)
    {
        _health -= damage;
        return _health <= 0;
    }
}
