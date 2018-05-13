using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Image[] loadingImages;
    public Text text;
    public int i = 0;

    private bool canGoToNextStep = true;
    private int currentStep = 0;

    private void Start()
    {
        // Initializing the first tutorial step.
        GoToLoadingStep(0);
    }

    private void Update()
    {
        if (Input.GetButtonUp("PS4 X"))
        {
            if (canGoToNextStep)
            {
                GoToLoadingStep(++i);
            }
        }

        //// Mouse Version
        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (canGoToNextStep)
        //    {
        //        GoToLoadingStep(++i);
        //    }
        //}
    }

    /// <summary>
    /// Moves to a specific step.
    /// </summary>
    /// <param name="step">Number of the step in which move.</param>
    private void GoToLoadingStep(int step)
    {
        // If there is a previous step and the previous step has an image, then its image is disabled.
        if (currentStep >= 0 && currentStep < loadingImages.Length)
        {
            loadingImages[currentStep].enabled = false;
        }

        // Now the current step will be the new step.
        currentStep = step;

        // If this new step has an image, then its image is enabled.
        if (step < loadingImages.Length)
        {
            loadingImages[step].enabled = true;
        }

        // Check the step value in order to activate the current text.
        switch (step)
        {
            case 0:
                text.text = "Quando vedi queste icone premi il tasto corrispondente del tuo controller per attaccare lo chef";
                break;
            case 1:
                text.text = "Messaggio 2";
                break;
            case 2:
                text.text = "Messaggio 3";
                break;
            default: // Last state, or a casual number in input.
                canGoToNextStep = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}
