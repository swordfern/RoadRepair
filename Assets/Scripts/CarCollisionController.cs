using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CarCollisionController : MonoBehaviour
{
    [SerializeField] private CarHealth _carHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageApplier = collision.GetComponent<DamageApplier>();
        if (damageApplier == null)
        {
            return;
        }

        var damage = damageApplier.DamageAmount;
        var carIsDetroyed = _carHealth.ApplyDamageAndTryDestroy(damage);
        if (carIsDetroyed)
        {
            BroadcastMessage(MessageStrings.CarDestroyed);
        }
    }
}
