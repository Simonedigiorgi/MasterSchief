using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour {

    public Transform startgamePivot;

    private AudioSource source;
    private AudioClip smash;

    public Button newGame;
    public Button controls;
    public Button quit;

    public Image newGameClick;
    public Image controlClick;
    public Image quitClick;

    public Image fade;

    // Use this for initialization
    void Start () {
        fade.enabled = false;
        fade.DOFade(0, 0);
	}
	
	// Update is called once per frame
	void Update () {

        

	}

    /*public IEnumerator Intro()
    {
        introText.text = "2018";
        //ntroText.DOFade(1, 1);
        yield return new WaitForSeconds(2);
        introText.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        introText.text = "L'umanità è all'apice del progresso tecnologico, grazie alle nuove scoperte scientifiche finalmente è possibile mandare sms senza consumare credito, fare la spesa via internet, compreso le pesanti confezioni d'acqua da 2 Litri e mangiare la Nutella senza il pericolosissimo olio di palma";
        yield return new WaitForSeconds(2);
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(25);
        introText.DOFade(0, 1);
        yield return new WaitForSeconds(2);
        introText.text = "Ma alcuni nuovi nemici dell'umanità sono venuti a distruggere la realtà per come la conosciamo";
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(6);
        introText.DOFade(0, 1);
        yield return new WaitForSeconds(2);
        introText.text = " I TERRAPIATTISTI";
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(2);
        introText.DOFade(0, 1);
    }*/

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public void Controls()
    {
        StartCoroutine(ControlsRoutine());
    }

    public void Quit()
    {
        StartCoroutine(QuitRoutine());
    }

    public IEnumerator StartGameRoutine()
    {
        fade.enabled = true;
        newGameClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;
        yield return new WaitForSeconds(2.0f);
        fade.DOFade(1, 2);
        yield return new WaitForSeconds(2.0f);
        //SceneManager.LoadScene();
    }

    public IEnumerator ControlsRoutine()
    {
        /*//controlClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;
        //yield return new WaitForSeconds(2.0f);
        startgamePivot.DOMoveX(-300, 0.8f);
        yield return new WaitForSeconds(2.0f);
        //SceneManager.LoadScene();*/
        fade.enabled = true;
        controlClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;
        yield return new WaitForSeconds(2.0f);
        fade.DOFade(1, 2);
        yield return new WaitForSeconds(2.0f);
        //SceneManager.LoadScene();
    }

    public IEnumerator QuitRoutine()
    {
        /*//controlClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;
        //yield return new WaitForSeconds(2.0f);
        startgamePivot.DOMoveX(-300, 0.8f);
        yield return new WaitForSeconds(2.0f);
        //SceneManager.LoadScene();*/
        fade.enabled = true;
        quitClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;
        yield return new WaitForSeconds(2.0f);
        fade.DOFade(1, 2);
        yield return new WaitForSeconds(2.0f);
        //SceneManager.LoadScene();
    }
}
