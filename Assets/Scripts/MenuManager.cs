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
    public AudioClip voice1;
    public AudioClip voice2;
    public AudioClip voice3;
    public AudioClip voice4;
    public AudioClip music;


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

    public Transform crdst;
    public Transform cntrls;

    Vector3 startPos;

    Vector3 creditsStartPos;
    Vector3 controlsStartPos;

    void Start () {
        source = GetComponent<AudioSource>();
        fade.enabled = false;
        fade.DOFade(0, 0);
        source.PlayOneShot(music, 0.3f);
        startPos = startgamePivot.position;
        creditsStartPos = crdst.position;
        controlsStartPos = cntrls.position;
    }

    void Update () {


	}

    public void StartGame()
    {
        source.PlayOneShot(voice1, 5.5f);
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
        StartCoroutine(StartGameRoutine());
    }

    public void Controls()
    {
        source.PlayOneShot(voice2, 5.5f);
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
        StartCoroutine(ControlsRoutine());
    }

    public void Credits()
    {
        source.PlayOneShot(voice3, 5.5f);
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
        StartCoroutine(CreditsRoutine());
    }

    public void Quit()
    {
        source.PlayOneShot(voice4, 5.5f);
        source.PlayOneShot(punch, 0.4f);
        source.PlayOneShot(smash, 0.3f);
        StartCoroutine(QuitRoutine());
    }

    public void Back()
    {
        StartCoroutine(BackCoroutine());
    }

    public void ReturnCredits()
    {
        StartCoroutine(BackCreditsCoroutine());
    }

    public IEnumerator StartGameRoutine()
    {
        fade.enabled = true;
        newGameClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;

        newGame.image.enabled = false;
        newGameHand.enabled = true;
        yield return new WaitForSeconds(2.0f);
        fade.DOFade(1, 2);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator ControlsRoutine()
    {
        newGame.enabled = false;
        controls.enabled = false;
        credits.enabled = false;
        quit.enabled = false;

        controls.image.enabled = false;
        controlHand.enabled = true;
        controlClick.enabled = true;
        yield return new WaitForSeconds(1.0f);
        startgamePivot.DOMoveY(-Screen.height, 0.8f);
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator CreditsRoutine()
    {
        newGame.enabled = false;
        controls.enabled = false;
        credits.enabled = false;
        quit.enabled = false;

        credits.image.enabled = false;
        creditsHand.enabled = true;
        creditsClick.enabled = true;
        yield return new WaitForSeconds(1.0f);
        startgamePivot.DOMoveX(-Screen.width/2, 0.8f);
        yield return new WaitForSeconds(1.0f);

    }

    public IEnumerator QuitRoutine()
    {
        quitClick.enabled = true;
        newGame.enabled = false;
        controls.enabled = false;
        quit.enabled = false;

        quit.image.enabled = false;
        quitHand.enabled = true;
        quitClick.enabled = true;
        yield return new WaitForSeconds(2.0f);
        Application.Quit();
        yield return null;
    }




    public IEnumerator BackCoroutine()
    {
        newGame.enabled = true;
        controls.enabled = true;
        credits.enabled = true;
        quit.enabled = true;

        controls.image.enabled = true;
        controlHand.enabled = false;
        controlClick.enabled = false;
        startgamePivot.DOMove(startPos,0.8f);
        yield return null;
    }

    public IEnumerator BackCreditsCoroutine()
    {
        newGame.enabled = true;
        controls.enabled = true;
        credits.enabled = true;
        quit.enabled = true;

        credits.image.enabled = true;
        creditsHand.enabled = false;
        creditsClick.enabled = false;
        startgamePivot.DOMove(startPos, 0.8f);
        yield return null;
    }
}
