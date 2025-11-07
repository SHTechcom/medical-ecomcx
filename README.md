# 🎮 Base Project

Một bộ khung Unity cơ bản

---
## Chú ý khi build game:
- Build:
    + WebGL
    + Test:
        Code optimizztion: Shorter Build Time
    + Release:
        Dùng Scene nào thì add scene đấy (ví dụ: bài 1 -> scene Bai1)
        Manager Stripping Level: High (Ignore những thứ ko được dùng khi build)
        Code optimizztion: Runtime Speed with LTO

- Folder Practices:
    + Chung 20 bài trong 1 dự án
    + Chia các bài thành các folder riêng
    _Main:
        Common: 
            Script 
            Model
            ...
        Phan1:
            Bai1
                Script 
                Model
                ...
            Bai2
            ...
        Phan2:
            Bai1
                Script 
                Model
                ...
            Bai2
            ...
    + Folder Common sẽ chứa những hệ thống, model,... dùng chung xuyên suốt các  bài

## 📁 Quy ước đặt tên Asset

| Loại Asset           | Tiền tố     | Ví dụ                         |
|----------------------|-------------|-------------------------------|
| Sprite               | `spr_`      | `spr_coin`                    |
| UI Sprite            | `ui_`       | `ui_popupmission_title`      |
| Texture              | `tex_`      | `tex_block`                   |
| 3D Model             | `model_`    | `model_block`                 |
| Material             | `mat_`      | `mat_block`                   |
| Sound Effect (SFX)   | `sfx_`      | `sfx_win`                     |
| Background Music     | `bgm_`      | `bgm_gameplay`                |
| Animation Clip       | `anim_`     | `anim_jump`                   |
| Animator Controller  | `ac_`       | `ac_player`                   |
| Prefab               | `prefab_`   | `prefab_block`                |
| ScriptableObject     | `so_`       | `so_config_game`              |
| Shader               | `shader_`   | `shader_liquid`               |
| Font                 | `font_`     | `font_time_new_romans`       |
| Timeline Asset       | `tl_`       | `tl_action`                   |


# Các hệ thống trong projects
- Dotween Pro
- Quick Outline

# Design Partten
- Singleton



# **SoundManager**

### **1. Chuẩn bị**

1. Tạo **SoundData** (ScriptableObject) và thêm danh sách `entries` gồm:
    - `key`: tên định danh âm thanh.
    - `clip`: file `AudioClip`.
    - `type`: loại âm thanh (`Music`, `Effect`, `UI`, `Voice`, `Ambient`).
2. Thêm `SoundManager` vào scene. (sample: SampleFolders/Prefabs/Manager/prefab_manager_sound)
3. Gán các trường serialized trong **Inspector** (Đã gán sẵn trong prefab):
    - **soundData** – dữ liệu âm thanh. (SampleFolders/SO/Sound/so_data_sound)
    - **mixer** – `AudioMixer` chính. (SampleFolders/SO/Sound/_mixer)
    - **musicSource** – `AudioSource` riêng cho nhạc nền.
    - **mixerMusicGroup**, **mixerFXGroup**, **mixerUIGroup**, **mixerVoiceGroup**, **mixerAmbientGroup** – gán đúng
      group tương ứng trong `AudioMixer`.
4. UI group sample: SampleFolders/Prefabs/UI/prefab_ui_setting_sound

* Các loại sound:
    - **Music** - Nhạc nền
    - **Effect** - Sound hiệu ứng
    - **Ambient** - Sound môi trường
    - **Voice** - Sound giọng nói
    - **UI** - Sound UI

---

### **2. Gọi trong code**

Xem ví dụ tại script: *SoundExample.cs*

| Hành động                    | Cách dùng                                                                      |
|------------------------------|--------------------------------------------------------------------------------|
| Phát âm thanh theo key       | `SoundManager.Instance.Play("button_click");`                                  |
| Phát âm thanh bằng AudioClip | `SoundManager.Instance.Play(myClip);`                                          |
| Dừng âm thanh theo key       | `SoundManager.Instance.Stop("bgm_main");`                                      |
| Dừng âm thanh của AudioClip  | `SoundManager.Instance.Stop(myClip);`                                          |
| Phát nhạc nền                | `SoundManager.Instance.PlayMusic("bgm_main", AudioPlayType.Loop);`             |
| Dừng nhạc nền                | `SoundManager.Instance.StopMusic();`                                           |
| Tạm dừng / Tiếp tục nhạc     | `SoundManager.Instance.PauseMusic();` / `SoundManager.Instance.ResumeMusic();` |
| Phát nhạc ngẫu nhiên         | `SoundManager.Instance.PlayRandomMusic();`                                     |
| Set âm lượng từng loại       | `SoundManager.Instance.SetVolume((float)value, SoundType);`                    |

---

## **Các trường quan trọng**

| Trường                | Loại              | Chức năng                                    |
|-----------------------|-------------------|----------------------------------------------|
| **soundData**         | `SoundData`       | Danh sách các âm thanh có key, clip và type. |
| **musicSource**       | `AudioSource`     | Dùng riêng cho nhạc nền (Music).             |
| **mixer**             | `AudioMixer`      | Bộ trộn âm thanh, chứa các tham số volume.   |
| **mixerMusicGroup**   | `AudioMixerGroup` | Group cho nhạc nền.                          |
| **mixerFXGroup**      | `AudioMixerGroup` | Group cho hiệu ứng.                          |
| **mixerUIGroup**      | `AudioMixerGroup` | Group cho âm thanh UI.                       |
| **mixerVoiceGroup**   | `AudioMixerGroup` | Group cho giọng nói.                         |
| **mixerAmbientGroup** | `AudioMixerGroup` | Group cho âm thanh môi trường.               |

---

### **Ghi chú**

- Volume range: `0` → `1f`. (Đã tự động lerp(-80f,0f) trong SoundManager)
- Khi `soundData` không có key, hàm sẽ return ngay mà ko gây lỗi.
- Nếu sound dùng đơn lẻ có thể gọi play bằng AudioClip mà ko cần thêm key vào SoundData


* Hồ sơ bệnh nhân: Patient Recorder Manager
- khởi tạo prefab patient_recorder
- API:
    + Instance
    + Get - PartientRecorder
        + Init
        + SetName
        + SetAge
- Select Option từ list để chọn loại bệnh