using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalinBar : MonoBehaviour
{
    public Image Bar;
    public float speed = 0.05f;

    void Update()
    {
        Bar.fillAmount -= Time.deltaTime * speed;
        if (Bar.fillAmount < 0)
            Bar.fillAmount = 0;
    }
}
