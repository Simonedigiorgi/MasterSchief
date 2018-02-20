using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    [BoxGroup("Components")] public AudioSource soundManager;

    [BoxGroup("Voci fuori campo")] public AudioClip getReady;

    [BoxGroup("Voci dello Chef")] public AudioClip intro;
    [BoxGroup("Voci dello Chef")] public AudioClip outro;
    [BoxGroup("Voci dello Chef")] public AudioClip charged;
    [BoxGroup("Voci dello Chef")] public AudioClip counter;
    [BoxGroup("Voci dello Chef")] public AudioClip counterAttack;


    public AudioClip[] chefHits;
    public AudioClip[] punchHits;

    public void PlayCharged()
    {
        soundManager.PlayOneShot(charged);
    }

    public void PlayCounter()
    {
        soundManager.PlayOneShot(counter);
    }
    
    public void PlayChefHits()
    {
        soundManager.PlayOneShot(chefHits[Random.Range(0, chefHits.Length)]);
    }

    public void PlayPunchHits()
    {
        soundManager.PlayOneShot(punchHits[Random.Range(0, punchHits.Length)], 0.4f);
    }

    public void PlayIntro()
    {
        soundManager.PlayOneShot(intro);
    }

    public void PlayOutro()
    {
        soundManager.PlayOneShot(outro);
    }

    public void PlayGetReady()
    {
        soundManager.PlayOneShot(getReady, 0.1f);
    }

    public void PlayCounterAttack()
    {
        soundManager.PlayOneShot(counterAttack);
    }

}
