using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;                                                                                    // Pause Menu GameObject
    public GameObject firstSelection;                                                                               // First Button selected

    private EventSystem system;                                                                                     // Find EventSystem
    private bool isMenu;                                                                                            // Open/Close the Pause Menu Panel

    void Start () {
        system = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        pauseMenu.SetActive(false);
    }
	
	void Update () {

        if (Input.GetButtonDown("Start") && !isMenu)
            MenuOpen();
        else if(Input.GetButtonDown("Start") && isMenu)
            MenuClose();
    }

    public void MenuOpen()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isMenu = true;

        // Deselect and reselect first button to correctly display highlight after enabling parent GO
        system.SetSelectedGameObject(null);
        system.SetSelectedGameObject(firstSelection);
    }

    public void MenuClose()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isMenu = false;
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

