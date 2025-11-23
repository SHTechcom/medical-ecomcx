using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] Slider sliderVolume;

    [Header("Mute Button")]
    public Button mute;
    public Sprite normalicon, muteicon;
    public Image muteImage;

    private bool isMuted = false;
    private float lastVolume = 0.5f; // lưu âm lượng trước khi mute

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        sliderVolume.minValue = 0;
        sliderVolume.maxValue = 1;
        sliderVolume.onValueChanged.AddListener(OnVolumeChanged);

        // Load volume cũ
        lastVolume = PlayerPrefs.GetFloat("LastVolume", 0.5f);
        isMuted = PlayerPrefs.GetInt("isMuted", 0) == 1;

        if (isMuted)
        {
            audioSource.volume = 0;
            sliderVolume.value = 0;
            muteImage.sprite = muteicon;
        }
        else
        {
            audioSource.volume = lastVolume;
            sliderVolume.value = lastVolume;
            muteImage.sprite = normalicon;
        }

        mute.onClick.AddListener(ToggleMute);
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnVolumeChanged(float value)
    {
        if (!isMuted) // chỉ lưu khi không mute
        {
            audioSource.volume = value;
            lastVolume = value;
            PlayerPrefs.SetFloat("LastVolume", lastVolume);
        }
    }

    // ⭐ Nút mute / unmute
    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            lastVolume = sliderVolume.value;        // lưu lại trước khi tắt
            sliderVolume.value = 0;
            audioSource.volume = 0;
            muteImage.sprite = muteicon;
            PlayerPrefs.SetInt("isMuted", 1);
        }
        else
        {
            sliderVolume.value = lastVolume;
            audioSource.volume = lastVolume;
            muteImage.sprite = normalicon;
            PlayerPrefs.SetInt("isMuted", 0);
        }
    }
}
