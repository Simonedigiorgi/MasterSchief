using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]

public class IntroScript : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource source;

    public AudioClip masterschiaff;

    public Text introText;

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

        chara1.enabled = false;
        chara2.enabled = false;

        chara4.enabled = false;
        title.enabled = false;

        StartCoroutine(Intro());
	}
	
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space) && movie.isPlaying)
        {
            movie.Pause();
        }
		
	}

    public IEnumerator Intro()
    {
        yield return new WaitForSeconds(3.0f);
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(5.0f);
        introText.DOFade(0,1);
        yield return new WaitForSeconds(3.0f);
        fade.enabled = false;

        movie.Play();
        source.Play();

        yield return new WaitForSeconds(29.0f);

        source.volume = 0.3f;
        chara1.enabled = true;
        chara1.rectTransform.DOMoveY(-40, 15);

        yield return new WaitForSeconds(12.0f);

        chara2.enabled = true;

        yield return new WaitForSeconds(2.0f);
        chara3.rectTransform.DOMoveY(4, 15);

        yield return new WaitForSeconds(15.0f);
        chara4.enabled = true;

        yield return new WaitForSeconds(6.0f);

        title.enabled = true;

        movie.Pause();


    }
}
