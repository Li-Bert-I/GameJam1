using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalinBar : MonoBehaviour
{
    public Image Bar;
    public float fill;

    void Start()
    {
        fill = 1f;
    }

    void Update()
    {
        fill -= Time.deltaTime * 0.05f;
        Bar.fillAmount = fill;
    }
}
