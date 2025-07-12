using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;   // Drag your AudioMixer here in Inspector
    public Slider volumeSlider;     // Drag your Slider here

    void Start()
    {
        float currentVolume;
        if (audioMixer.GetFloat("Volume", out currentVolume))
        {
            // Map dB back to 0–100 range for the slider
            float sliderValue = Mathf.InverseLerp(-80f, 0f, currentVolume) * 100f;
            volumeSlider.value = sliderValue;
        }

        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    public void OnSliderChanged(float value)
    {
        // Map 0–100 to -80 to 0 dB
        float dB = Mathf.Lerp(-80f, 0f, value / 100f);
        audioMixer.SetFloat("Volume", dB);
    }
}
