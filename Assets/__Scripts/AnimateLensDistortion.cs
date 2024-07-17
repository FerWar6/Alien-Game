using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class AnimateLensDistortion : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity = 0.5f;

    private LensDistortion lensDistortion;
    private float initialDistortion;
    private float initialScale;
    private float initialXMultiplier;
    private float initialYMultiplier;
    private float initialCenterX;
    private float initialCenterY;

    private float nextChangeTime = 0f;
    private float currentDistortionRange = 0.1f;
    private float currentScaleRange = 0.05f;
    private float currentXMultiplierRange = 0.1f;
    private float currentYMultiplierRange = 0.1f;
    private float currentCenterXRange = 0.05f;
    private float currentCenterYRange = 0.05f;

    private void Start()
    {
        PostProcessVolume volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out lensDistortion);

        if (lensDistortion != null)
        {
            initialDistortion = lensDistortion.intensity.value;
            initialScale = lensDistortion.scale.value;
            initialXMultiplier = lensDistortion.intensityX.value;
            initialYMultiplier = lensDistortion.intensityY.value;
            initialCenterX = lensDistortion.centerX.value;
            initialCenterY = lensDistortion.centerY.value;
        }
    }

    private void Update()
    {
        if (lensDistortion != null)
        {
            float time = Time.time * intensity;

            // Change ranges randomly over time
            if (Time.time >= nextChangeTime)
            {
                currentDistortionRange = Random.Range(0.1f, 0.5f);
                currentScaleRange = Random.Range(0.05f, 0.3f);
                currentXMultiplierRange = Random.Range(0.1f, 0.3f);
                currentYMultiplierRange = Random.Range(0.1f, 0.3f);
                currentCenterXRange = Random.Range(0.05f, 0.15f);
                currentCenterYRange = Random.Range(0.05f, 0.15f);
                nextChangeTime = Time.time + Random.Range(1f, 3f); // Change values every 1-3 seconds
            }

            lensDistortion.intensity.value = initialDistortion + Mathf.Sin(time) * currentDistortionRange * intensity;
            lensDistortion.scale.value = initialScale + Mathf.Cos(time * 1.2f) * currentScaleRange * intensity;
            lensDistortion.intensityX.value = initialXMultiplier + Mathf.Sin(time * 1.3f) * currentXMultiplierRange * intensity;
            lensDistortion.intensityY.value = initialYMultiplier + Mathf.Cos(time * 1.5f) * currentYMultiplierRange * intensity;
            lensDistortion.centerX.value = initialCenterX + Mathf.Sin(time * 1.7f) * currentCenterXRange * intensity;
            lensDistortion.centerY.value = initialCenterY + Mathf.Cos(time * 1.9f) * currentCenterYRange * intensity;
        }
    }
}
