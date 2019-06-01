using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour {

    private AudioSource source;

    [FoldoutGroup("Components")] public Transform pivot;                        // Posizione del Pivot

    [FoldoutGroup("Immagini")] public Image fade;                               // Immagine di Fade
    public Button[] gameButtons = new Button[3];                                // New Game, Controls, Credits, Quit

    public AudioClip[] audioVoices;                                             // Voci
    public AudioClip[] audioEffects;                                            // Effetti sonori

    //private Vector3 startPos;                                                   // Posizione 

    public GameObject creditsTab;
    public GameObject tutorialTab;

    void Start () {

        source = GetComponent<AudioSource>();

        fade.enabled = false;

        //startPos = pivot.position;
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

    #region Metodi
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
    #endregion

    #region Coroutine
    public IEnumerator StartGameRoutine()
    {
        source.PlayOneShot(audioVoices[0], 5.5f);

        PlayAudioEffects();

        fade.enabled = true;

        gameButtons[0].enabled = false;
        gameButtons[1].enabled = false;
        gameButtons[3].enabled = false;

        gameButtons[0].image.enabled = false;
        gameButtons[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
        gameButtons[0].transform.GetChild(1).GetComponent<Image>().enabled = true;

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

        gameButtons[1].image.enabled = false;
        gameButtons[1].transform.GetChild(0).GetComponent<Image>().enabled = true;
        gameButtons[1].transform.GetChild(1).GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(1.0f);
        //pivot.DOMoveY(-Screen.height, 0.8f);
        tutorialTab.SetActive(true);
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator CreditsRoutine()
    {
        source.PlayOneShot(audioVoices[2], 5.5f);

        PlayAudioEffects();
        AllButtonsDisable();

        gameButtons[2].image.enabled = false;
        gameButtons[2].transform.GetChild(0).GetComponent<Image>().enabled = true;
        gameButtons[2].transform.GetChild(1).GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(1.0f);
        //pivot.DOMoveX(-Screen.width/2, 0.8f);
        creditsTab.SetActive(true);
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator QuitRoutine()
    {
        source.PlayOneShot(audioVoices[3], 5.5f);

        PlayAudioEffects();
        AllButtonsDisable();

        gameButtons[0].enabled = false;
        gameButtons[1].enabled = false;
        gameButtons[3].enabled = false;

        gameButtons[3].image.enabled = false;
        gameButtons[3].transform.GetChild(0).GetComponent<Image>().enabled = true;
        gameButtons[3].transform.GetChild(1).GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(2.0f);
        Application.Quit();
        yield return null;
    }

    public IEnumerator BackCoroutine()
    {
        AllButtonsEnable();

        gameButtons[1].image.enabled = true;
        gameButtons[1].transform.GetChild(0).GetComponent<Image>().enabled = false;
        gameButtons[1].transform.GetChild(1).GetComponent<Image>().enabled = false;

        //pivot.DOMove(startPos,0.8f);
        tutorialTab.SetActive(false);
        yield return null;
    }

    public IEnumerator BackCreditsCoroutine()
    {
        AllButtonsEnable();

        gameButtons[2].image.enabled = true;
        gameButtons[2].transform.GetChild(0).GetComponent<Image>().enabled = false;
        gameButtons[2].transform.GetChild(1).GetComponent<Image>().enabled = false;

        //pivot.DOMove(startPos, 0.8f);
        creditsTab.SetActive(false);
        yield return null;
    }
    #endregion

}
