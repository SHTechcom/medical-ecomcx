using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]Slider sliderVolume;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sliderVolume.minValue = 0;
        sliderVolume.maxValue = 1;
        sliderVolume.onValueChanged.AddListener(OnVolumeChanged);
        sliderVolume.value = 0.5f;
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void OnVolumeChanged(float value)
    {
        audioSource.volume = value;
    }
}
