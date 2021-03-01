using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalinBar : MonoBehaviour
{
    public float speed = 0.05f;
    public float maxVolume = 0.5f;

    private AudioSource audioSrc;
    private Image Bar;

    void Start ()
    {
        audioSrc = GetComponent<AudioSource> ();
        Bar = GetComponent<Image> ();
    }

    void Update()
    {
        Bar.fillAmount -= Time.deltaTime * speed;
        if (Bar.fillAmount < 0)
            Bar.fillAmount = 0;

        audioSrc.volume = maxVolume * Mathf.Pow(Bar.fillAmount, 4.0f);
    }
}
