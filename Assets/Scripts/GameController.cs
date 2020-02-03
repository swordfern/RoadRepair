using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarCollisionController carCollisionController;
    [SerializeField] private CarMovementBehaviour carMovementBehaviour;
    [SerializeField] private HUDController hudController;

    private GameObject[] menuSharedObjects;
    private GameObject[] mainMenuObjects;
    private GameObject[] settingsMenuObjects;
    private GameObject[] aboutMenuObjects;
    private GameObject[] gameOverMenuObjects;

    private GameObject[] levelEndObjects;
    private GameObject[] gameplayObjects;

    private SoundManager _soundManager;
    private SoundManager SoundManager
    {
        get
        {
            if (_soundManager == null)
            {
                _soundManager = Camera.main.GetComponent<SoundManager>();
            }
            return _soundManager;
        }
    }

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
        gameOverMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOverMenu");
        
        levelEndObjects = GameObject.FindGameObjectsWithTag("ShowOnLevelEnd");

        gameplayObjects = GameObject.FindGameObjectsWithTag("ShowOnGameplay");

        HideLevelEndObjects();
        HideGameplayObjects();

        HideAboutMenuObjects();
        HideSettingsMenuObjects();
        HideGameOverObjects();

        DisplayGameMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CarCollisionController_CarDestroyed()
    {
        // play car destruction music
        SoundManager.pickUpSource.PlayOneShot(SoundManager.destroyClip, 0.30f);

        // display game over menu
        ShowGameOverObjects();
    }

    public void CarCollisionController_CarDamaged(DamageApplier damageApplier)
    {
        var clip = damageApplier.CustomAudioClip == null
            ? SoundManager.collisionClip
            : damageApplier.CustomAudioClip;

        SoundManager.pickUpSource.PlayOneShot(clip, 0.30f);

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

        SoundManager.StopMenuMusic();
        SoundManager.BeginLevelMusic();

        Time.timeScale = 1.0f;
    }

    public void OnRestartButtonClick()
    {
        HideGameOverObjects();
        DisplayGameMenu();
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

        SoundManager.BeginMenuMusic();

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

    private void ShowGameOverObjects()
    {
        foreach (GameObject g in gameOverMenuObjects)
        {
            g.SetActive(true);
        }
    }

    private void HideGameOverObjects()
    {
        foreach (GameObject g in gameOverMenuObjects)
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
