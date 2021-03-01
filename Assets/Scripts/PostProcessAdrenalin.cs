using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PostProcessAdrenalin : MonoBehaviour
{
    public Image Bar;
    public float powerSaturation;
    public float powerTint;
    public float powerChrom;
    public float minSaturation;
    public float maxSaturation;
    public float minTint;
    public float maxTint;
    public float minChrom;
    public float maxChrom;

    private PostProcessVolume volume;
    private ColorGrading color;
    private ChromaticAberration chrom;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out color);
        volume.profile.TryGetSettings(out chrom);
    }

    // Update is called once per frame
    void Update()
    {
        color.saturation.value = minSaturation + (maxSaturation - minSaturation) * Mathf.Pow(Bar.fillAmount, powerSaturation);
        color.tint.value = minTint + (maxTint - minTint) * Mathf.Pow(Bar.fillAmount, powerTint);
        chrom.intensity.value =  minChrom + (maxChrom - minChrom) * Mathf.Pow(Bar.fillAmount, powerChrom);
    }
}
