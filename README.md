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