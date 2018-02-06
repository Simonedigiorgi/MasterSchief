using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour {

    public Image fade;
    public Text introText;

	// Use this for initialization
	void Start () {
        StartCoroutine(Intro());
	}
	
	// Update is called once per frame
	void Update () {

        

	}

    public IEnumerator Intro()
    {
        introText.text = "2018";
        //ntroText.DOFade(1, 1);
        yield return new WaitForSeconds(2);
        introText.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        introText.text = "L'umanità è all'apice del progresso tecnologico, grazie alle nuove scoperte scientifiche finalmente è possibile mandare sms senza consumare credito, fare la spesa via internet, compreso le pesanti confezioni d'acqua da 2 Litri e mangiare la Nutella senza il pericolosissimo olio di palma";
        yield return new WaitForSeconds(2);
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(25);
        introText.DOFade(0, 1);
        yield return new WaitForSeconds(2);
        introText.text = "Ma alcuni nuovi nemici dell'umanità sono venuti a distruggere la realtà per come la conosciamo";
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(6);
        introText.DOFade(0, 1);
        yield return new WaitForSeconds(2);
        introText.text = " I TERRAPIATTISTI";
        introText.DOFade(1, 1);
        yield return new WaitForSeconds(2);
        introText.DOFade(0, 1);
    }
}
