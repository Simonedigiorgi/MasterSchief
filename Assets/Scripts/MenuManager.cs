using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour {

    public Transform startgamePivot;

    private AudioSource source;
    public AudioClip smash;
    public AudioClip punch;

    public Button newGame;
    public Button controls;
    public Button credits;
    public Button quit;

    public Image newGameClick;
    public Image controlClick;
    public Image creditsClick;
    public Image quitClick;

    public Image newGameHand;
    public Image controlHand;
    public Image creditsHand;
    public Image quitHand;

    public Image fade;

    void Start () {
        source = GetComponent<AudioSource>();
        fade.enabled = false;
        fade.DOFade(0, 0);
	}
	
	void Update () {

        

	}

    public void StartGame()
    {
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
        StartCoroutine(StartGameRoutine());
    }

    public void Controls()
    {
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
        StartCoroutine(ControlsRoutine());
    }

    public void Credits()
    {
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
        StartCoroutine(CreditsRoutine());
    }

    public void Quit()
    {
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
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
        SceneManager.LoadScene("Barbieri");
    }

    public IEnumerator ControlsRoutine()
    {
        newGame.enabled = false;
        controls.enabled = false;
        credits.enabled = false;
        quit.enabled = false;

        controlClick.enabled = true;
        yield return new WaitForSeconds(2.0f);
        startgamePivot.DOMoveY(-350, 0.8f);
        yield return new WaitForSeconds(2.0f);
    }

    public IEnumerator CreditsRoutine()
    {
        newGame.enabled = false;
        controls.enabled = false;
        credits.enabled = false;
        quit.enabled = false;

        creditsClick.enabled = true;
        yield return new WaitForSeconds(2.0f);
        startgamePivot.DOMoveX(-320, 0.8f);
        yield return new WaitForSeconds(2.0f);

    }

    public IEnumerator QuitRoutine()
    {
        quitClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;

        quitClick.enabled = true;
        yield return new WaitForSeconds(2.0f);
        Application.Quit();
        yield return null;
    }


}
