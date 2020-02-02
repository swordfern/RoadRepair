using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarCollisionController carCollisionController;
    [SerializeField] private CarMovementBehaviour carMovementBehaviour;
    [SerializeField] private HUDController hudController;

    private GameObject[] mainMenuObjects;
    private GameObject[] damageNotificationObjects;
    private GameObject[] levelEndObjects;
    private GameObject[] gameplayObjects;

    // Start is called before the first frame update
    void Start()
    {
        carCollisionController.CarDestroyedAction += CarCollisionController_CarDestroyed;
        carCollisionController.CarDamagedAction += CarCollisionController_CarDamaged;
        carCollisionController.ReachedEndOfLevelAction += CarCollisionController_ReachedEndOfLevel;

        mainMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnMenuVisible");
        damageNotificationObjects = GameObject.FindGameObjectsWithTag("ShowOnDamage");
        levelEndObjects = GameObject.FindGameObjectsWithTag("ShowOnLevelEnd");
        gameplayObjects = GameObject.FindGameObjectsWithTag("ShowOnGameplay");

        HideDamageNotificationObjects();
        HideLevelEndObjects();
        HideGameplayObjects();

        DisplayLevelMenu();
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
        GameObject.Find("Main Camera").GetComponent<SoundManager>().pickUpSource.PlayOneShot(GameObject.Find("Main Camera").GetComponent<SoundManager>().collisionClip, 0.30f);

        hudController.UpdateHealthSlider(carCollisionController.GetHealth(), carCollisionController.GetMaxHealth());
    }

    public void CarCollisionController_ReachedEndOfLevel()
    {
        Time.timeScale = 0;

        ShowLevelEndObjects();
    }

    public void OnStartButtonClick()
    {
        HideLevelMenu();
        ShowGameplayObjects();
        hudController.CreateHealthSlider(carCollisionController.GetMaxHealth());
        carMovementBehaviour.SetGameStarted();

        GameObject.Find("Main Camera").GetComponent<SoundManager>().BeginLevelMusic();

        Time.timeScale = 1.0f;
    }

    public void OnSettingsButtonClick()
    {
    }

    public void OnAboutButtonClick()
    {

    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void DisplayLevelMenu()
    {
        int count = 0;
        foreach (GameObject g in mainMenuObjects)
        {
            g.SetActive(true);
        }
        Time.timeScale = 0;
    }

    private void HideLevelMenu()
    {
        foreach (GameObject g in mainMenuObjects)
        {
            g.SetActive(false);
        }
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

    private void ShowGameplayObjects()
    {
        foreach (GameObject g in gameplayObjects)
        {
            g.SetActive(true);
        }
    }

    private void HideGameplayObjects()
    {
        foreach (GameObject g in gameplayObjects)
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
