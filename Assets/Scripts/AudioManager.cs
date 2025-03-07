using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    // AudioSource để phát nhạc nền
    public AudioSource backgroundMusicSource;

    // AudioClip mặc định cho nhạc nền (tùy chọn)
    public AudioClip defaultBackgroundMusic;
    public AudioClip themeMap1;
    public AudioClip themeMap2;
    public AudioClip themeMap3;

    public float backGroundVolume = 0.8f;
    public float mapThemeVolume = 0.3f;

    void Awake()
    {
        // Kiểm tra nếu đã có một instance khác
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); // Hủy object này nếu đã có một instance khác
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject); // Giữ object này khi chuyển scene

        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.loop = true; // Lặp lại nhạc
        }

        // Phát nhạc nền mặc định nếu có
        if (defaultBackgroundMusic != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.clip = defaultBackgroundMusic;
            backgroundMusicSource.volume = backGroundVolume;
            backgroundMusicSource.Play();
        }
    }

    public void PlayTheme1BackgroundMusic()
    {
        if (backgroundMusicSource.clip == themeMap1) {
            backgroundMusicSource.time = 0;
            backgroundMusicSource.Play();
        }

        backgroundMusicSource.clip = themeMap1;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = mapThemeVolume;
        backgroundMusicSource.Play();
    }

    public void PlayTheme2BackgroundMusic()
    {
        if (backgroundMusicSource.clip == themeMap2) {
            backgroundMusicSource.time = 0;
            backgroundMusicSource.Play();
        }

        backgroundMusicSource.clip = themeMap2;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = mapThemeVolume;
        backgroundMusicSource.Play();
    }

    public void PlayTheme3BackgroundMusic()
    {
        if (backgroundMusicSource.clip == themeMap3)
        {
            backgroundMusicSource.time = 0;
            backgroundMusicSource.Play();
        }

        backgroundMusicSource.clip = themeMap3;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = mapThemeVolume;
        backgroundMusicSource.Play();
    }

    // Hàm phát nhạc nền
    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource.clip == defaultBackgroundMusic)
            return;

        backgroundMusicSource.clip = defaultBackgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = backGroundVolume;
        backgroundMusicSource.Play();
    }

    // Hàm dừng nhạc nền
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    // Hàm điều chỉnh âm lượng nhạc nền
    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicSource.volume = Mathf.Clamp01(volume); // Đảm bảo âm lượng từ 0 đến 1
    }

    // Các hàm khác để phát âm thanh hiệu ứng (SFX)
    public void PlaySoundEffect(AudioClip clip)
    {
        // Tạo một GameObject tạm thời để phát SFX và tự hủy sau khi phát xong
        GameObject sfxObject = new GameObject("SFX");
        AudioSource sfxSource = sfxObject.AddComponent<AudioSource>();
        sfxSource.clip = clip;
        sfxSource.Play();
        Destroy(sfxObject, clip.length); // Hủy sau khi clip kết thúc
    }
}