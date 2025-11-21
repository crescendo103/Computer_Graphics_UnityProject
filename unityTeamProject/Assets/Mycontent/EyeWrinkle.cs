using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class EyeWrinkle : MonoBehaviour
{
    public Volume volume;
    public AnimationCurve curve;
    public float duration = 1f;

    private Vignette vignette;

    void Start()
    {
        volume.profile.TryGet(out vignette);
        StartCoroutine(PlayCurve());
    }

    IEnumerator PlayCurve()
    {
        float time = 0f;

        while (time < duration)
        {
            float v = curve.Evaluate(time / duration); // 0~1 ¡æ curve °ª
            SetIntensity(v);
            time += Time.deltaTime;
            yield return null;
        }

        SetIntensity(curve.Evaluate(1f));
    }

    void SetIntensity(float value)
    {
        vignette.intensity.value = value;
    }
}
