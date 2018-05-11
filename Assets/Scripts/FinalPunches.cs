using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class FinalPunches : MonoBehaviour {

    [BoxGroup("Testi")] public Text counterText;                                                // Mostra il conteggio della pestata finale
    [BoxGroup("Immagini")] public Image pressButtonImage;                                       // Immagine del Tasto da premere

    [BoxGroup("Pestata Finale")] public int punches;                                            // Click della scazzottata finale
    [BoxGroup("Pestata Finale")] public float maxTime;                                          // Durata della Pestata (Velocità della barra quando scala) 
    
    [HideInInspector] public int clickCounter = 0;                                              // Conteggio dei Pugni finali

    public void Update()
    {
        counterText.text = "" + (punches - clickCounter) + "/" + punches;                       // Mostra il testo (Totale colpi finali da dare - 1 colpo)
    }
}
