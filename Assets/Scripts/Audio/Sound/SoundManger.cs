using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class SoundManger : MonoBehaviour
    {
        private static SoundManger instance;
        public static SoundManger Instance
        {
            get => instance;
        }

        private AudioSource audioSource;
        private Dictionary<string, AudioClip> dictAudio;

        // public Sound[] musicSounds, sfxSounds;
        // public AudioSource musicSource, sfxSource;

        private void Awake()
        {
            if (Instance == null) // 如果实例为空，表示是第一次创建该类的实例
            {
                // DontDestroyOnLoad(gameObject); // 在场景切换时不销毁该对象
                instance = this; // 将当前实例赋值给静态实例
            }
            else if (Instance != null) // 如果已经存在实例
            {
                Destroy(gameObject); // 销毁当前对象，保持只有一个实例
            }

            audioSource = GetComponent<AudioSource>();
            dictAudio = new Dictionary<string, AudioClip>();
        }

        /// <summary> 辅助函数：加载音频，需要确保音频文件的路径在Resources文件夹下
        /// </summary>
        /// <param name="path">音频所在路径</param>
        /// <returns></returns>
        public AudioClip LoadAudio(string path)
        {
            return (AudioClip)Resources.Load(path);
        }

        /// <summary> 辅助函数：获取音频，并且将其缓存在dictAudio中，避免重复加载
        /// </summary>
        /// <param name="path">音频所在路径</param>
        /// <returns></returns>
        private AudioClip GetAudio(string path)
        {
            if (!dictAudio.ContainsKey(path))
            {
                dictAudio[path] = LoadAudio(name);
            }
            else
            {
                Debug.Log($"{path} 路径下 找不到 {name} 音频");
            }

            return dictAudio[path];
        }


        public void PlayBGM(string name, float volume = 1.0f)
        {
            audioSource.Stop();
            audioSource.clip = GetAudio(name);
            audioSource.Play();
        }

        public void StopBGM()
        {
            audioSource.Stop();
        }

        /// <summary> 播放音效
        /// </summary>
        /// <param name="path">音频路径</param>
        /// <param name="volume">音量，默认为1</param>
        public void PlayAudio(string path, float volume = 1.0f)
        {
            // PlayOneShot支持多个音频文件在同一时刻播放，并且不会相互覆盖
            audioSource.PlayOneShot(LoadAudio(path), volume);
            // audioSource.volume = volume;
        }

        /// <summary> 某些需要使用到3d或者根据距离有关的音效播放，可以使用挂载在物体身上的AudioSource播放，以达到音效远近的效果
        /// </summary>
        /// <param name="audioSource">物体身上的音频源</param>
        /// <param name="path">音频路径</param>
        /// <param name="volume">音量，默认为1</param>
        public void PlayAudio(AudioSource audioSource, string path, float volume = 1.0f)
        {
            // PlayOneShot支持多个音频文件在同一时刻播放，并且不会相互覆盖
            audioSource.PlayOneShot(LoadAudio(path), volume);
            audioSource.volume = volume;
        }
    }
}
