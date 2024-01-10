using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region 单例模式
    public static AudioManager Instance; // 静态实例

    private void Awake()
    {
        if (Instance == null) // 如果实例为空，表示是第一次创建该类的实例
        {
            // DontDestroyOnLoad(gameObject); // 在场景切换时不销毁该对象
            Instance = this; // 将当前实例赋值给静态实例
        }
        else if (Instance != null) // 如果已经存在实例
        {
            Destroy(gameObject); // 销毁当前对象，保持只有一个实例
        }

        foreach (AudioType type in AudioTypes)
        {
            type.Source = gameObject.AddComponent<AudioSource>();

            type.Source.clip = type.Clip;
            type.Source.name = type.Name;
            type.Source.volume = type.Volume;
            type.Source.pitch = type.Pitch;
            type.Source.loop = type.Loop;

            if (type.Group != null)
            {
                type.Source.outputAudioMixerGroup = type.Group;
            }
        }
    }
    #endregion


    public AudioType[] AudioTypes;

    private void Start()
    {

    }

    //播放音乐
    public void Play(string name)
    {
        foreach (AudioType type in AudioTypes)
        {
            if (type.Name == name)
            {
                type.Source.Play();
                return;
            }
        }

        Debug.LogWarning("没有找到" + name + "音频");
    }
    //暂停音乐
    public void Pause(string name)
    {
        foreach (AudioType type in AudioTypes)
        {
            if (type.Name == name)
            {
                type.Source.Pause();
                return;
            }
        }

        Debug.LogWarning("没有找到" + name + "音频");
    }

    //停止播放
    public void Stop(string name)
    {
        foreach (AudioType type in AudioTypes)
        {
            if (type.Name == name)
            {
                type.Source.Stop();
                return;
            }
        }

        Debug.LogWarning("没有找到" + name + "音频");
    }
}
