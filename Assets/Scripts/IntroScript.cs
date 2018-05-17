using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]

public class IntroScript : MonoBehaviour
{
    // Public properties
    public AudioClip masterSchiefAudioClip;
    [Space]
    public Text introText;
    public Text testo1;
    public Text testo2;
    public Text testo3;
    [Space]
    public Image black;
    public Image fade;
    public Image chara1;
    public Image chara2;
    public Image chara3;
    public Image chara4;
    [Space]
    public Image title;

    [Space]
    public Button skipButton;

    // Private properties
    private AudioSource audioSource;
    private VideoPlayer videoPlayer;

    private bool isSkipping = false;

    void Start()
    {
        // Initializing private Component properties.
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        introText.DOFade(0, 0);

        fade.DOFade(0, 0);
        fade.enabled = false;

        chara1.enabled = false;

        testo1.enabled = false;
        testo2.enabled = false;
        testo3.enabled = false;

        chara2.enabled = false;

        chara4.enabled = false;
        title.enabled = false;

        StartCoroutine(Intro());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipIntro();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isSkipping && !skipButton.gameObject.activeSelf)
            {
                skipButton.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator Intro()
    {
        yield return new WaitForSeconds(3.0f);
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(5.0f);
        introText.DOFade(0, 1);
        yield return new WaitForSeconds(3.0f);
        fade.enabled = false;

        black.enabled = false;
        videoPlayer.Play();

        yield return new WaitForSeconds(29.0f);
        
        chara1.enabled = true;
        testo1.enabled = true;
        chara1.rectTransform.DOMoveY(-40, 15);

        yield return new WaitForSeconds(12.0f);

        chara2.enabled = true;
        testo1.enabled = false;
        testo2.enabled = true;

        yield return new WaitForSeconds(2.0f);
        chara3.rectTransform.DOMoveY(-30, 15);

        yield return new WaitForSeconds(15.0f);
        chara4.enabled = true;
        testo2.enabled = false;
        testo3.enabled = true;

        yield return new WaitForSeconds(7.4f);

        fade.enabled = true;
        fade.DOFade(1, 0);
        yield return new WaitForSeconds(0.2f);

        title.enabled = true;
        title.transform.DOScale(new Vector3(1.2f, 1.2f, 0), 7.6f);
        title.transform.GetChild(0).GetComponent<Text>().enabled = true;

        audioSource.PlayOneShot(masterSchiefAudioClip, 1.2f);
        videoPlayer.Pause();
        yield return new WaitForSeconds(6.0f);

        SceneManager.LoadScene("Menu");

    }

    public void SkipIntro()
    {
        if (isSkipping) return;

        StartCoroutine(SkipCoroutine());
    }
    private IEnumerator SkipCoroutine()
    {
        isSkipping = true;

        testo1.enabled = false;
        testo2.enabled = false;
        testo3.enabled = false;

        skipButton.gameObject.SetActive(false);

        fade.enabled = true;
        fade.DOFade(1, 1);
        videoPlayer.Pause();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }
}
