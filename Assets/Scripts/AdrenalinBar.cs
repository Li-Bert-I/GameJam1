using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalinBar : MonoBehaviour
{
    public float speed = 0.05f;
    private AudioSource audioSrc;
    private float oldFill = 1.0f;
    private Image Bar;

    void Start ()
    {
        audioSrc = GetComponent<AudioSource> ();
        Bar = GetComponent<Image> ();
    }

    void Update()
    {
        if (Bar.fillAmount > oldFill) {
            if (!audioSrc.isPlaying)
                audioSrc.Play();
        }
        Bar.fillAmount -= Time.deltaTime * speed;
        if (Bar.fillAmount < 0)
            Bar.fillAmount = 0;
        oldFill = Bar.fillAmount;
    }
}
