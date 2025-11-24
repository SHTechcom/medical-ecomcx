using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private const string PREF_LAST_VOLUME = "LastVolume";
    private const string PREF_IS_MUTED = "IsMuted";
    private const string PREF_VOICE_IS_MALE = "VoiceIsMale";

    private AudioSource audioSource;

    [Header("Volume & Mute")]
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Button mute;
    [SerializeField] private Button resetvolume;
    [SerializeField] private Sprite normalicon;
    [SerializeField] private Sprite muteicon;
    [SerializeField] private Image muteImage;

    [Header("Voice Clips (Object 1 & 2)")]
    public AudioClip maleClipObj1;
    public AudioClip femaleClipObj1;
    public AudioClip maleClipObj2;
    public AudioClip femaleClipObj2;

    [Header("Voice Switch (1 button + 1 image)")]
    public Button voiceSwitchButton;   // CHỈ 1 BUTTON
    public Image giong;                // Image hiển thị icon
    public Sprite giongNam;            // Sprite icon Nam
    public Sprite giongNu;             // Sprite icon Nữ

    private bool isMuted = false;
    private float lastVolume = 0.5f;
    private bool isMaleVoice = true;   // true = Nam, false = Nữ

    // 0 = chưa bấm gì, 1 = Object1, 2 = Object2
    private int lastPlayedObjectId = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // ==== Volume & Mute ====
        sliderVolume.minValue = 0f;
        sliderVolume.maxValue = 1f;

        lastVolume = PlayerPrefs.GetFloat(PREF_LAST_VOLUME, 0.5f);
        isMuted = PlayerPrefs.GetInt(PREF_IS_MUTED, 0) == 1;

        if (isMuted)
        {
            audioSource.volume = 0f;
            sliderVolume.value = 0f;
            muteImage.sprite = muteicon;
        }
        else
        {
            audioSource.volume = lastVolume;
            sliderVolume.value = lastVolume;
            muteImage.sprite = normalicon;
        }

        sliderVolume.onValueChanged.AddListener(OnVolumeChanged);
        mute.onClick.AddListener(ToggleMute);
        resetvolume.onClick.AddListener(ResetVolume);

        // ==== Voice load & UI update ====
        isMaleVoice = PlayerPrefs.GetInt(PREF_VOICE_IS_MALE, 1) == 1; // default Nam
        UpdateVoiceIcon();

        if (voiceSwitchButton != null)
            voiceSwitchButton.onClick.AddListener(ToggleVoice);
    }

    // ================== VOLUME / MUTE ==================

    public void OnVolumeChanged(float value)
    {
        if (!isMuted)
        {
            audioSource.volume = value;
            lastVolume = value;
            PlayerPrefs.SetFloat(PREF_LAST_VOLUME, lastVolume);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            lastVolume = sliderVolume.value;
            sliderVolume.value = 0f;
            audioSource.volume = 0f;
            muteImage.sprite = muteicon;
            PlayerPrefs.SetInt(PREF_IS_MUTED, 1);
        }
        else
        {
            sliderVolume.value = lastVolume;
            audioSource.volume = lastVolume;
            muteImage.sprite = normalicon;
            PlayerPrefs.SetInt(PREF_IS_MUTED, 0);
        }
    }

    private void ResetVolume()
    {
        isMuted = false;
        PlayerPrefs.SetInt(PREF_IS_MUTED, 0);
        muteImage.sprite = normalicon;

        float defaultVolume = 0.5f;
        sliderVolume.value = defaultVolume;
        audioSource.volume = defaultVolume;

        lastVolume = defaultVolume;
        PlayerPrefs.SetFloat(PREF_LAST_VOLUME, lastVolume);
    }

    // ================== VOICE SWITCH (Nam ↔ Nữ) ==================

    private void ToggleVoice()
    {
        // Đổi trạng thái giọng
        isMaleVoice = !isMaleVoice;
        PlayerPrefs.SetInt(PREF_VOICE_IS_MALE, isMaleVoice ? 1 : 0);
        UpdateVoiceIcon();

        // Xác định object hiện tại dựa trên clip đang phát
        int currentObj = GetCurrentObjectId();
        Debug.Log("ToggleVoice - currentObj = " + currentObj + ", isMaleVoice = " + isMaleVoice);

        if (currentObj == 1)
        {
            PlayVoice_Object1();
        }
        else if (currentObj == 2)
        {
            PlayVoice_Object2();
        }
    }


    private void UpdateVoiceIcon()
    {
        if (giong != null)
            giong.sprite = isMaleVoice ? giongNam : giongNu;
    }

    // ================== PLAY AUDIO ==================

    private void PlayAudio(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();      // nếu đang chạy clip cũ -> tự restart bằng clip mới
    }

    public void PlayVoice_Object1()
    {
        lastPlayedObjectId = 1;
        AudioClip clip = isMaleVoice ? maleClipObj1 : femaleClipObj1;
        PlayAudio(clip);
    }

    public void PlayVoice_Object2()
    {
        lastPlayedObjectId = 2;
        AudioClip clip = isMaleVoice ? maleClipObj2 : femaleClipObj2;
        PlayAudio(clip);
    }
    private int GetCurrentObjectId()
    {
        if (audioSource.clip == maleClipObj1 || audioSource.clip == femaleClipObj1)
            return 1;

        if (audioSource.clip == maleClipObj2 || audioSource.clip == femaleClipObj2)
            return 2;

        return lastPlayedObjectId; // fallback
    }

}
