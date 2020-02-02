using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarCollisionController carCollisionController;
    [SerializeField] private MenuButtonController[] menuButtonControllers;

    private GameObject[] mainMenuObjects;
    private GameObject[] damageNotificationObjects;
    private GameObject[] levelEndObjects;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        carCollisionController.CarDestroyedAction += CarCollisionController_CarDestroyed;
        carCollisionController.CarDamagedAction += CarCollisionController_CarDamaged;
        carCollisionController.ReachedEndOfLevelAction += CarCollisionController_ReachedEndOfLevel;

        foreach(MenuButtonController mbc in menuButtonControllers)
        {
            mbc.StartGameAction += MenuButtonController_StartGame;
        }

        mainMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnMenuVisible");
        damageNotificationObjects = GameObject.FindGameObjectsWithTag("ShowOnDamage");
        levelEndObjects = GameObject.FindGameObjectsWithTag("ShowOnLevelEnd");

        HideDamageNotificationObjects();
        HideLevelEndObjects();

        DisplayLevelMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MenuButtonController_StartGame()
    {
        Debug.Log("start button was clicked");
        HideLevelMenu();
    }

    public void CarCollisionController_CarDestroyed()
    {
        Debug.Log("Car is destroyed");
    }

    public void CarCollisionController_CarDamaged()
    {
        ShowDamageNotificationObjects();
    }

    public void CarCollisionController_ReachedEndOfLevel()
    {
        Time.timeScale = 0;

        ShowLevelEndObjects();
    }

    private void DisplayLevelMenu()
    {
        Time.timeScale = 0;
    }

    private void HideLevelMenu()
    {
        foreach(GameObject g in mainMenuObjects)
        {
            Debug.Log("hiding menu objects");
            g.SetActive(false);
        }
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
        yield return new WaitForSeconds(seconds);
        foreach (GameObject g in objects)
        {
            g.SetActive(false);
        }
    }
}
