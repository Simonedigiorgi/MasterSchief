using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class IntroScript : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource source;

    public AudioClip masterschiaff;

    public Button skip;

    public Text introText;
    public Text testo1;
    public Text testo2;
    public Text testo3;

    public Image black;
    public Image fade;
    public Image chara1;
    public Image chara2;
    public Image chara3;
    public Image chara4;

    public Image title;

    void Start () {
        introText.DOFade(0, 0);
        GetComponent<RawImage>().texture = movie as MovieTexture;
        source = GetComponent<AudioSource>();
        source.clip = movie.audioClip;

        skip.gameObject.SetActive(false);

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
	
	void Update () {

        /*if(Input.GetKeyDown(KeyCode.Space) && movie.isPlaying)
        {
            movie.Pause();
        }*/
		
	}

    public void Skip()
    {
        StartCoroutine(SkipCoroutine());
    }

    public IEnumerator Intro()
    {
        yield return new WaitForSeconds(3.0f);
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(5.0f);
        introText.DOFade(0,1);
        yield return new WaitForSeconds(3.0f);
        fade.enabled = false;

        skip.gameObject.SetActive(true);
        black.enabled = false;
        movie.Play();
        source.Play();

        yield return new WaitForSeconds(29.0f);

        source.volume = 0.7f;
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

        source.PlayOneShot(masterschiaff, 1.2f);
        movie.Pause();
        yield return new WaitForSeconds(6.0f);

        SceneManager.LoadScene("Menu");

    }

    public IEnumerator SkipCoroutine()
    {
        testo1.enabled = false;
        testo2.enabled = false;
        testo3.enabled = false;

        fade.enabled = true;
        fade.DOFade(1, 1);
        movie.Pause();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
}
