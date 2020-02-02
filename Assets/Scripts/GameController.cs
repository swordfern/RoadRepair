using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarCollisionController carCollisionController;

    private GameObject[] damageNotificationObjects;
    private GameObject[] levelEndObjects;

    // Start is called before the first frame update
    void Start()
    {
        carCollisionController.CarDestroyedAction += CarCollisionController_CarDestroyed;
        carCollisionController.CarDamagedAction += CarCollisionController_CarDamaged;
        carCollisionController.ReachedEndOfLevelAction += CarCollisionController_ReachedEndOfLevel;

        damageNotificationObjects = GameObject.FindGameObjectsWithTag("ShowOnDamage");
        levelEndObjects = GameObject.FindGameObjectsWithTag("ShowOnLevelEnd");

        HideDamageNotificationObjects();
        HideLevelEndObjects();
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

        ShowDamageNotificationObjects();
    }

    public void CarCollisionController_ReachedEndOfLevel()
    {
        Debug.Log("Reached end of map");

        Time.timeScale = 0;

        ShowLevelEndObjects();
    }

    private void ShowDamageNotificationObjects()
    {
        foreach(GameObject g in damageNotificationObjects)
        {
            g.SetActive(true);
        }

        StartCoroutine(HideAfterSeconds(1, damageNotificationObjects));
    }

    private void HideDamageNotificationObjects()
    {
        foreach (GameObject g in damageNotificationObjects)
        {
            g.SetActive(false);
        }
    }

    private void ShowLevelEndObjects()
    {
        foreach(GameObject g in levelEndObjects)
        {
            g.SetActive(true);
        }
    }

    private void HideLevelEndObjects()
    {
        foreach(GameObject g in levelEndObjects)
        {
            g.SetActive(false);
        }
    }

    IEnumerator HideAfterSeconds(int seconds, GameObject[] objects)
    {
        Debug.Log("hiding after " + seconds + " seconds");
        yield return new WaitForSeconds(seconds);
        foreach (GameObject g in objects)
        {
            g.SetActive(false);
        }
    }
}
