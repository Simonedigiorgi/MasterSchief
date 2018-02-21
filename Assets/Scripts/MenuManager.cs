using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour {

    public Transform startgamePivot;

    private AudioSource source;

    public AudioClip[] audioVoices;
    public AudioClip[] audioEffects;

    public Button[] gameButtons;                                                            // New Game, Controls, Credits, Quit

    //public Button newGame;
    //public Button controls;
    //public Button credits;
    //public Button quit;

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

    /*Vector3 creditsStartPos;
    Vector3 controlsStartPos;*/

    void Start () {

        source = GetComponent<AudioSource>();

        fade.enabled = false;
        fade.DOFade(0, 0);
        //source.PlayOneShot(audioEffects[0], 0.3f);
        startPos = startgamePivot.position;
        /*creditsStartPos = crdst.position;
        controlsStartPos = cntrls.position;*/
    }

    #region OnClick

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public void Controls()
    {
        StartCoroutine(ControlsRoutine());
    }

    public void Credits()
    {
        StartCoroutine(CreditsRoutine());
    }

    public void Quit()
    {
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
    #endregion

    public IEnumerator StartGameRoutine()
    {
        source.PlayOneShot(audioVoices[0], 5.5f);

        PlayAudioEffects();


        fade.enabled = true;
        newGameClick.enabled = true;
        gameButtons[0].enabled = false;
        //newGame.enabled = false;
        gameButtons[1].enabled = false;
        gameButtons[3].enabled = false;

        gameButtons[0].image.enabled = false;
        newGameHand.enabled = true;
        yield return new WaitForSeconds(2.0f);
        fade.DOFade(1, 2);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator ControlsRoutine()
    {
        source.PlayOneShot(audioVoices[1], 5.5f);
        PlayAudioEffects();

        AllButtonsDisable();

        /*newGame.enabled = false;
        controls.enabled = false;
        credits.enabled = false;
        quit.enabled = false;*/

        gameButtons[1].image.enabled = false;
        controlHand.enabled = true;
        controlClick.enabled = true;
        yield return new WaitForSeconds(1.0f);
        startgamePivot.DOMoveY(-Screen.height, 0.8f);
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator CreditsRoutine()
    {
        source.PlayOneShot(audioVoices[2], 5.5f);
        PlayAudioEffects();

        AllButtonsDisable();

        /*newGame.enabled = false;
        controls.enabled = false;
        credits.enabled = false;
        quit.enabled = false;*/

        gameButtons[2].image.enabled = false;
        creditsHand.enabled = true;
        creditsClick.enabled = true;
        yield return new WaitForSeconds(1.0f);
        startgamePivot.DOMoveX(-Screen.width/2, 0.8f);
        yield return new WaitForSeconds(1.0f);

    }

    public IEnumerator QuitRoutine()
    {
        source.PlayOneShot(audioVoices[3], 5.5f);
        PlayAudioEffects();

        quitClick.enabled = true;
        gameButtons[0].enabled = false;
        gameButtons[1].enabled = false;
        gameButtons[3].enabled = false;

        gameButtons[3].image.enabled = false;
        quitHand.enabled = true;
        quitClick.enabled = true;
        yield return new WaitForSeconds(2.0f);
        Application.Quit();
        yield return null;
    }




    public IEnumerator BackCoroutine()
    {
        AllButtonsEnable();

        /*newGame.enabled = true;
        controls.enabled = true;
        credits.enabled = true;
        quit.enabled = true;*/

        gameButtons[1].image.enabled = true;
        controlHand.enabled = false;
        controlClick.enabled = false;
        startgamePivot.DOMove(startPos,0.8f);
        yield return null;
    }

    public IEnumerator BackCreditsCoroutine()
    {
        AllButtonsEnable();

        /*newGame.enabled = true;
        controls.enabled = true;
        credits.enabled = true;
        quit.enabled = true;*/

        gameButtons[2].image.enabled = true;
        creditsHand.enabled = false;
        creditsClick.enabled = false;
        startgamePivot.DOMove(startPos, 0.8f);
        yield return null;
    }

    private void AllButtonsEnable()
    {
        foreach (Button button in gameButtons)
        {
            button.enabled = true;
        }
    }

    private void AllButtonsDisable()
    {
        foreach (Button button in gameButtons)
        {
            button.enabled = false;
        }
    }

    private void PlayAudioEffects()
    {
        source.PlayOneShot(audioEffects[0], 0.3f);
        source.PlayOneShot(audioEffects[1], 0.4f);
    }
}
