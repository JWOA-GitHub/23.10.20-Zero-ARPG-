using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerVolumeController : MonoBehaviour
{
    public AudioMixer audioMixer; // 从Inspector中分配

    public Slider musicVolumeSlider; // 从Inspector中分配
    public Slider sfxVolumeSlider; // 从Inspector中分配

    bool isMusicToZeroClik = false; // 用于切换音乐静音的标志
    bool isSFXToZeroClik = false;   // 用于切换音效静音的标志

    public float MusicmappedVolume;  // 映射后的音乐音量
    public float SFXmappedVolume;    // 映射后的音效音量

    private void Start()
    {
        // 初始化滑动条的值为当前Audio Mixer的音量值
        float musicVolume;
        float sfxVolume;

        // 获取音量值并映射到滑动条范围
        audioMixer.GetFloat("MusicVolume", out musicVolume);
        audioMixer.GetFloat("SFXVolume", out sfxVolume);
        musicVolumeSlider.value = RemapValue(musicVolume, -80f, 20f, 0f, 1f);
        sfxVolumeSlider.value = RemapValue(sfxVolume, -80f, 20f, 0f, 1f);
    }

    // 映射值的函数
    private float RemapValue(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    // 通过滑动条来调整音乐音量
    public void SetMusicVolume(float volume)
    {
        // 映射滑动条的值到音量范围，然后设置音乐音量
        MusicmappedVolume = RemapValue(volume, 0f, 1f, -80f, 20f);
        audioMixer.SetFloat("MusicVolume", MusicmappedVolume);
    }

    // 通过滑动条来调整音效音量
    public void SetSFXVolume(float volume)
    {
        print(volume);
        // 映射滑动条的值到音量范围，然后设置音效音量
        SFXmappedVolume = RemapValue(volume, 0f, 1f, -80f, 20f);
        audioMixer.SetFloat("SFXVolume", SFXmappedVolume);
    }

    // 直接将音乐音量静音或恢复
    public void SetMusicToZero()
    {
        // 切换静音状态
        isMusicToZeroClik = !isMusicToZeroClik;
        if (isMusicToZeroClik)
        {
            audioMixer.SetFloat("MusicVolume", -80); // 音量静音（-80 是最低值）
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", MusicmappedVolume); // 恢复音量
        }
    }

    // 直接将音效音量静音或恢复
    public void SetSFXToZero()
    {
        // 切换静音状态
        isSFXToZeroClik = !isSFXToZeroClik;
        if (isSFXToZeroClik)
        {
            audioMixer.SetFloat("SFXVolume", -80); // 音量静音（-80 是最低值）
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", SFXmappedVolume); // 恢复音量
        }
    }
}
