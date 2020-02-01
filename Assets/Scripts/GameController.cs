using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarCollisionController carCollisionController;

    // Start is called before the first frame update
    void Start()
    {
        carCollisionController.CarDestroyedAction += CarCollisionController_CarDestroyed;
        carCollisionController.CarDamagedAction += CarCollisionController_CarDamaged;
        carCollisionController.ReachedEndOfLevelAction += CarCollisionController_ReachedEndOfLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CarCollisionController_CarDestroyed()
    {
        Debug.Log("Car is destroyed");
    }

    public void CarCollisionController_CarDamaged()
    {
        Debug.Log("Car took damage.");
    }

    public void CarCollisionController_ReachedEndOfLevel()
    {
        Debug.Log("Reached end of map");
    }
}
