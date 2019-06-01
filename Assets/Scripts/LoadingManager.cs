using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public InputField playername;

    private void Start()
    {

    }

    public void StartGame()
    {
        HealthBar.chefName = "Aspirante Chef " + playername.text;
        SceneManager.LoadSceneAsync("Menu");
    }
}
