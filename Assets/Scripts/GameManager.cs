using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public IEnumerator currentCoroutine;                                                            // COROUTINE

    private PlayerActions playerAction;                                                             // PLAYERACTION
    private HealthBar healthBar;                                                                    // HEALTHBAR
    private SoundManager soundManager;                                                              // SOUNDMANAGER


    public GameObject[] buttonPunch;                                                                // Array dei Tasti

    [FoldoutGroup("Animator")] public Animator chefAnimator;                                        // CHEFANIMATOR

    [FoldoutGroup("Immagini")] public Image fade;                                                   // Immagine di Fade

    [FoldoutGroup("Controlli")] public float startAfter;                                            // Secondi del primo attacco dello Chef (Inizio Partita)
    [FoldoutGroup("Controlli")] public string nextScene = "";                                       // Prossima scena

    [InfoBox("Tempo di spawn tra un tasto e l'altro")]
    [FoldoutGroup("Controller dei Tasti")] [Range(0.1f, 3)] public float buttonDelay = 1;           // Tempo di spawn tra un tasto e l'altro
    [InfoBox("Quanto tempo i Bottoni rimangono a schermo prima di sparire")]
    [FoldoutGroup("Controller dei Tasti")] [Range(0.5f, 5)] public float buttonTime = 1;            // Quanto tempo i Bottoni rimangono a schermo prima di sparire
    [InfoBox("Più vicino allo 0 aumenta la possibilità di Attacco dello Chef")]
    [FoldoutGroup("Controller dei Tasti")] [Range(0, 1)] public float buttonEvent;                  // (Più vicino allo 0 aumenta la possibilità di Attacco dello Chef)
    [InfoBox("Quanti tasti posso apparire insieme")]
    [FoldoutGroup("Controller dei Tasti")] [Range(1, 6)] public int buttonSpawn;                    // Quanti tasti posso apparire insieme

    [InfoBox("Dopo la metà dell'energia dello Chef")]
    [FoldoutGroup("Fase Incazzata")]
    [Range(0.1f, 3)]
    public float buttonDelayAfter = 1;           // Tempo di spawn tra un tasto e l'altro
    [InfoBox("Quanto tempo i Bottoni rimangono a schermo prima di sparire")]
    [FoldoutGroup("Fase Incazzata")]
    [Range(0.5f, 5)]
    public float buttonTimeAfter = 1;            // Quanto tempo i Bottoni rimangono a schermo prima di sparire
    [InfoBox("Più vicino allo 0 aumenta la possibilità di Attacco dello Chef")]
    [FoldoutGroup("Fase Incazzata")]
    [Range(0, 1)]
    public float buttonEventAfter;                  // (Più vicino allo 0 aumenta la possibilità di Attacco dello Chef)
    [InfoBox("Quanti tasti posso apparire insieme")]
    [FoldoutGroup("Fase Incazzata")]
    [Range(1, 6)]
    public int buttonSpawnAfter;                    // Quanti tasti posso apparire insieme

    public Text impiattaloText;

    private void Start()
    {
        fade.enabled = true;
        fade.DOFade(0, 0);

        playerAction = FindObjectOfType<PlayerActions>();
        healthBar = FindObjectOfType<HealthBar>();
        soundManager = FindObjectOfType<SoundManager>();

        // Disattiva i Bottoni

        foreach (GameObject button in buttonPunch)
        {
            button.SetActive(false);
        }

        StartCoroutine(SecondsBeforeStart());

        buttonPunch[0].GetComponent<SpriteRenderer>().enabled = true;
        buttonPunch[1].GetComponent<SpriteRenderer>().enabled = true;
        buttonPunch[2].GetComponent<SpriteRenderer>().enabled = true;
        buttonPunch[3].GetComponent<SpriteRenderer>().enabled = true;
        buttonPunch[4].GetComponent<SpriteRenderer>().enabled = true;
        buttonPunch[5].GetComponent<SpriteRenderer>().enabled = true;
    }

    public IEnumerator SecondsBeforeStart()
    {
        soundManager.PlayGetReady();

        yield return new WaitForSeconds(4);

        soundManager.PlayIntro();

        yield return new WaitForSeconds(startAfter);
        playerAction.isActive = true;

        if (Random.value <= buttonEvent)
        {
            currentCoroutine = YouPunch();
            StartCoroutine(currentCoroutine);
        }
        else
        {
            currentCoroutine = YouParry();
            StartCoroutine(currentCoroutine);
        }
    }

    public IEnumerator YouPunch()
    {
        float nextCoroutine = 0;
        int value = Random.Range(1, buttonSpawn);

        for(int i = 0; i < value; i++)
        {
            GetRandomButton().SetActive(true);
            yield return new WaitForSeconds(buttonDelay);
        }
        
        nextCoroutine += buttonDelay > buttonTime ? 0 : buttonTime - buttonDelay;

        yield return new WaitForSeconds(nextCoroutine);

        if (Random.value <= buttonEvent)
        {
            currentCoroutine = YouPunch();
            StartCoroutine(currentCoroutine);
        }
        else
        {
            currentCoroutine = YouParry();
            StartCoroutine(currentCoroutine);
        }
    }

    public GameObject GetRandomButton()
    {
        int index = Random.Range(0, buttonPunch.Length);
        while(buttonPunch[index].activeSelf)
        {
            index = Random.Range(0, buttonPunch.Length);
        }

        return buttonPunch[index];
    }

    public IEnumerator YouParry()
    {
        yield return null;
        chefAnimator.SetTrigger("ChargePunch");
    }

    public IEnumerator CheckIfParrying()
    {
        bool trigger = false;
        bool trigger2 = false;

        if (playerAction.isParrying && !trigger2)
        {
            soundManager.PlayCharged();
            trigger2 = true;
            //playerAction.SpawnParat();
        }
        else if (!trigger)
        {
            trigger = true;
            healthBar.TakeDamage();
            playerAction.SpawnChargeInfame();
        }

        while (!playerAction.canParry)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        if (Random.value <= buttonEvent)
        {
            currentCoroutine = YouPunch();
            StartCoroutine(currentCoroutine);
        }
        else
        {
            currentCoroutine = YouParry();
            StartCoroutine(currentCoroutine);
        }
    }

    public void BlockCoroutine()
    {
        StopCoroutine(currentCoroutine);
    }

    public void CutScene(float number)
    {
        StopCoroutine(currentCoroutine);
        StartCoroutine(Wait(number));
    }

    // COROUTINES

    public IEnumerator LevelFailed()
    {
        yield return new WaitForSeconds(1);
        fade.DOFade(1, 3);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1);
        fade.DOFade(1, 3);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(nextScene);
    }

    public IEnumerator Wait (float number)
    {
        impiattaloText.enabled = true;
        impiattaloText.text = "Lo Chef è incazzato";
        impiattaloText.GetComponent<Animation>().Play("MoveFromLeft");
        yield return new WaitForSeconds(number);

        buttonDelay = buttonDelayAfter;
        buttonTime = buttonTimeAfter;
        buttonEvent = buttonEventAfter;
        buttonSpawn = buttonSpawnAfter;

        StartCoroutine(currentCoroutine);
    }
}
