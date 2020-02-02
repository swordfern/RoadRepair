using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class CarCollisionController : MonoBehaviour
{
    [SerializeField] private CarHealth _carHealth;

    public event Action CarDestroyedAction;
    public event Action CarDamagedAction;
    public event Action ReachedEndOfLevelAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if you collided with the end of the level
        if(collision.tag == "Finish")
        {
            ReachedEndOfLevelAction?.Invoke();
            return;
        }

        // check if you collided with an obstacle
        var damageApplier = collision.GetComponent<DamageApplier>();
        if (damageApplier == null || damageApplier.IsDisabled)
        {
            return;
        }

        var damage = damageApplier.DamageAmount;
        var carIsDetroyed = _carHealth.ApplyDamageAndTryDestroy(damage);
        if (carIsDetroyed)
        {
            CarDestroyedAction?.Invoke();
        }
        else
        {
            CarDamagedAction?.Invoke();
        }
    }
}
