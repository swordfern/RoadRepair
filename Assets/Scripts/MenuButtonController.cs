using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuButtonController : MonoBehaviour
{
    public bool startButtonPress;
    public bool quitButtonPress;

    public event Action StartGameAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        Debug.Log("tapping");
        if (startButtonPress)
        {
            Debug.Log("trying to invoke game start action");
            StartGameAction?.Invoke();
        }
        else if (quitButtonPress)
        {
            Application.Quit();
        }
    }
}
