using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private const string PREF_LAST_VOLUME = "LastVolume";
    private const string PREF_IS_MUTED = "IsMuted";
    private const string PREF_VOICE_IS_MALE = "VoiceIsMale";

    public static AudioController Instance { get; private set; }

    private AudioSource audioSource;

    [Header("Volume & Mute")]
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Button mute;
    [SerializeField] private Button resetvolume;
    [SerializeField] private Sprite normalicon;
    [SerializeField] private Sprite muteicon;
    [SerializeField] private Image muteImage;

    [Header("Voice Switch (Nam / Nữ)")]
    // KHÔNG bắt buộc gán bằng code, có thể gán trong Inspector
    public Button voiceSwitchButton;
    public Image giong;
    public Sprite giongNam;
    public Sprite giongNu;

    private bool isMuted = false;
    private float lastVolume = 0.5f;
    private bool isMaleVoice = true; // true = Nam, false = Nữ

    public bool IsMaleVoice => isMaleVoice;

    // InfoButton hiện đang được chọn (để gọi lại khi đổi voice)
    [HideInInspector] public InfoButton currentInfoButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        // Volume & mute
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

        // Voice
        isMaleVoice = PlayerPrefs.GetInt(PREF_VOICE_IS_MALE, 1) == 1;
        UpdateVoiceIcon();

        // Có thể hook bằng code hoặc gán trong Inspector đều được
        if (voiceSwitchButton != null)
            voiceSwitchButton.onClick.AddListener(OnVoiceSwitchClicked);
    }

    // ---------- Volume / Mute ----------
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

    // ---------- Voice switch ----------
    // HÀM NÀY GÁN VÀO BUTTON SWITCH VOICE (OnClick) TRONG INSPECTOR
    public void OnVoiceSwitchClicked()
    {
        isMaleVoice = !isMaleVoice;
        PlayerPrefs.SetInt(PREF_VOICE_IS_MALE, isMaleVoice ? 1 : 0);
        UpdateVoiceIcon();

        // Dừng audio cũ
        if (audioSource.isPlaying)
            audioSource.Stop();

        // Gọi InfoButton hiện tại chơi lại audio theo giọng mới
        if (currentInfoButton != null)
        {
            Debug.Log("[AudioController] Switch voice, replay from InfoButton: " + currentInfoButton.name);
            currentInfoButton.PlayAudioForCurrentVoice();
        }
        else
        {
            Debug.Log("[AudioController] Switch voice but NO currentInfoButton");
        }
    }

    private void UpdateVoiceIcon()
    {
        if (giong != null)
            giong.sprite = isMaleVoice ? giongNam : giongNu;
    }

    // ---------- Play / Stop ----------
    public void Play(AudioClip clip)
    {
        if (clip == null) return;
        if (isMuted) return;

        audioSource.clip = clip;
        audioSource.time = 0f;
        audioSource.Play();
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
