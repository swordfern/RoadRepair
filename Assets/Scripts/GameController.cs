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

    private GameObject[] menuSharedObjects;
    private GameObject[] mainMenuObjects;
    private GameObject[] settingsMenuObjects;
    private GameObject[] aboutMenuObjects;
    private GameObject[] damageNotificationObjects;
    private GameObject[] levelEndObjects;
    private GameObject[] gameplayObjects;

    // Start is called before the first frame update
    void Start()
    {
        carCollisionController.CarDestroyedAction += CarCollisionController_CarDestroyed;
        carCollisionController.CarDamagedAction += CarCollisionController_CarDamaged;
        carCollisionController.ReachedEndOfLevelAction += CarCollisionController_ReachedEndOfLevel;

        menuSharedObjects = GameObject.FindGameObjectsWithTag("ShowOnMenuVisible");
        mainMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnMainMenu");
        settingsMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnSettingsMenu");
        aboutMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnAboutMenu");
        
        levelEndObjects = GameObject.FindGameObjectsWithTag("ShowOnLevelEnd");

        gameplayObjects = GameObject.FindGameObjectsWithTag("ShowOnGameplay");

        HideLevelEndObjects();
        HideGameplayObjects();

        HideAboutMenuObjects();
        HideSettingsMenuObjects();

        DisplayGameMenu();
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
        HideGameMenu();
        ShowGameplayObjects();
        hudController.CreateHealthSlider(carCollisionController.GetMaxHealth());
        carMovementBehaviour.SetGameStarted();

        GameObject.Find("Main Camera").GetComponent<SoundManager>().BeginLevelMusic();

        Time.timeScale = 1.0f;
    }

    public void OnBackToMainMenuClick(int menu)
    {
        if(menu == 1)
        {
            // came from settings menu
            HideSettingsMenuObjects();
        }else if(menu == 2)
        {
            // came from about menu
            HideAboutMenuObjects();
        }

        ShowMainMenuObjects();
    }

    public void OnSettingsButtonClick()
    {
        HideMainMenuObjects();
        ShowSettingsMenuObjects();
    }

    public void OnAboutButtonClick()
    {
        HideMainMenuObjects();
        ShowAboutMenuObjects();
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void DisplayGameMenu()
    {
        ShowSharedMenuObjects();
        ShowMainMenuObjects();

        Time.timeScale = 0;
    }

    private void HideGameMenu()
    {
        HideSharedMenuObjects();
        HideMainMenuObjects();
        HideAboutMenuObjects();
        HideSettingsMenuObjects();
    }

    private void ShowSharedMenuObjects()
    {
        foreach (GameObject g in menuSharedObjects)
        {
            g.SetActive(true);
        }
    }

    private void HideSharedMenuObjects()
    {
        foreach (GameObject g in menuSharedObjects)
        {
            g.SetActive(false);
        }
    }

    private void ShowMainMenuObjects()
    {
        foreach (GameObject g in mainMenuObjects)
        {
            g.SetActive(true);
        }
    }

    private void HideMainMenuObjects()
    {
        foreach (GameObject g in mainMenuObjects)
        {
            g.SetActive(false);
        }
    }

    private void ShowSettingsMenuObjects()
    {
        foreach (GameObject g in settingsMenuObjects)
        {
            g.SetActive(true);
        }
    }

    private void HideSettingsMenuObjects()
    {
        foreach (GameObject g in settingsMenuObjects)
        {
            g.SetActive(false);
        }
    }

    private void ShowAboutMenuObjects()
    {
        foreach (GameObject g in aboutMenuObjects)
        {
            g.SetActive(true);
        }
    }

    private void HideAboutMenuObjects()
    {
        foreach (GameObject g in aboutMenuObjects)
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
