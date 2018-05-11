using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;                                                                                    // Menu Panel
    public GameObject firstObject;

    private EventSystem system;
    private bool isMenuOpen;                                                                                        // Used to navigate on menu

    void Start () {
        system = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        pauseMenu.SetActive(false);
    }
	
	void Update () {

        if (Input.GetButtonDown("Start") && !isMenuOpen)
        {
            
                                                                                    // If true you can move between options
            MenuOpen();
        }
        else if(Input.GetButtonDown("Start") && isMenuOpen)
        {
                                                                                // If false you can't move between options
            MenuClose();
        }
    }

    public void MenuOpen()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isMenuOpen = true;

        // Deselect and reselect first button to correctly display highlight after enabling parent GO
        system.SetSelectedGameObject(null);
        system.SetSelectedGameObject(firstObject);
    }

    public void MenuClose()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isMenuOpen = false;
    }

    public void MainMenu()
    {
        // Select Main Menu Scene
    }

    public void SelectLevel()
    {
        // Select Level Scene
    }
}

