using UnityEngine;
using DG.Tweening;

public class FadeOut : MonoBehaviour {

    public float fadeSpeed = 1;
	
	void Update () {
        GetComponent<SpriteRenderer>().DOFade(0, fadeSpeed);     
	}
}
