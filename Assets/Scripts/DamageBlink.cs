using System.Collections;
using UnityEngine;

public class DamageBlink : MonoBehaviour
{
    [SerializeField] private CarCollisionController _collisionController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _damageBlinkTime;
    [SerializeField] private int _numBlinks;

    private void Awake()
    {
        _collisionController.CarDamagedAction += HandleCarDamaged;
    }

    private void HandleCarDamaged()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        var waitForSeconds = new WaitForSeconds(_damageBlinkTime);

        for (int i = 0; i < _numBlinks; i++)
        {
            _spriteRenderer.enabled = false;
            yield return waitForSeconds;
            _spriteRenderer.enabled = true;
            yield return waitForSeconds;
        }
    }
}
