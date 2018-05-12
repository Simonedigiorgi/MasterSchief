using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {

    public Image[] loadingImages;
    public Text text;
    public int i = 0;

	void Update () {

        loadingImages[i].enabled = true;

        if (Input.GetButtonUp("Button Down/Left"))
        {
            i++;
        }

        if(i == 0)
        {
            text.text = "Quando vedi queste icone premi il tasto corrispondente del tuo controller per attaccare lo chef";
        }

        if(i == 1)
        {
            text.text = "Messaggio 2";
        }

        if (i == 2)
        {
            text.text = "Messaggio 3";
        }

        if(i == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
