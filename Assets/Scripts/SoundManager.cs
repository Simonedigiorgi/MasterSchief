using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip intro;
    public AudioClip outro;
    public AudioClip charged;
    public AudioClip counter;
    public AudioClip getReady;

    public AudioClip[] chefHits;
    public AudioClip[] punchHits;



     public AudioSource introSource;
    public AudioSource outroSource;
    public AudioSource chargedSource;
    public AudioSource counterSource;
    public AudioSource getReadySource;

    public AudioSource chefHitsSource;
    public AudioSource punchHitsSource;




	// Use this for initialization
	void Start () {
        //introSource = GameObject.Find("IntroSource").GetComponent<AudioSource>();
        //outroSource = GameObject.Find("OutroSource").GetComponent<AudioSource>();
        //chargedSource = GameObject.Find("ChargedSource").GetComponent<AudioSource>();
        //counterSource = GameObject.Find("CounterSource").GetComponent<AudioSource>();
        //getReadySource = GameObject.Find("GetReadySource").GetComponent<AudioSource>();
        //chefHitsSource = GameObject.Find("ChefHitsSource").GetComponent<AudioSource>();
        //punchHitsSource = GameObject.Find("PunchHitsSource").GetComponent<AudioSource>();

    }






    public void PlayCharged()
    {
        chargedSource.PlayOneShot(charged);
    }

    public void PlayCounter()
    {
        counterSource.PlayOneShot(counter);
    }
    
    public void PlayChefHits()
    {
        chefHitsSource.PlayOneShot(chefHits[Random.Range(0, chefHits.Length)]);
    }

    public void PlayPunchHits()
    {
        punchHitsSource.PlayOneShot(punchHits[Random.Range(0, punchHits.Length)]);
    }

    public void PlayIntro()
    {
        introSource.PlayOneShot(intro);
    }

    public void PlayOutro()
    {
        outroSource.PlayOneShot(outro);
    }

    public void PlayGetReady()
    {
        getReadySource.PlayOneShot(getReady);
    }

}
