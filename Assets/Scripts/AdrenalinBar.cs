using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalinBar : MonoBehaviour
{
    public float speed = 0.05f;

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

        audioSrc.volume = Mathf.Pow(Bar.fillAmount, 3.0f) * 0.0f;
    }
}
