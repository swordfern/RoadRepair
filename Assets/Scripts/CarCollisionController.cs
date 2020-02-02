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

        //Play audio for picking up item
        GameObject.Find("Main Camera").GetComponent<SoundManager>().pickUpSource.PlayOneShot(GameObject.Find("Main Camera").GetComponent<SoundManager>().collisionClip, 0.30f);

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
