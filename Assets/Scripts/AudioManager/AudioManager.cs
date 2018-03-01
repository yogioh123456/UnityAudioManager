using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 音频管理器
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    /// <summary>
    /// 单例模式
    /// </summary>
    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if ((_instance = FindObjectOfType<AudioManager>()) == null)
                {
                    GameObject go = new GameObject(typeof(AudioManager).ToString());
                    _instance = go.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 音乐大小
    /// </summary>
    public float musicVol = 0.5f;

    /// <summary>
    /// 播放中的音频字典
    /// </summary>
    public Dictionary<string, Sound> soundDic = new Dictionary<string, Sound>();

    /// <summary>
    /// 播放新音乐
    /// </summary>
    /// <param name="musicName"></param>
    /// <param name="clip"></param>
    /// <param name="isLoop"></param>
    /// <returns></returns>
    public Sound PlayMusic(string musicName, AudioClip clip, bool isLoop)
    {
        AudioSource source = this.gameObject.AddComponent<AudioSource>();
        source.loop = isLoop;
        source.volume = musicVol;
        Sound sound = new Sound(clip, source);
        soundDic.Add(musicName, sound);
        return sound;
    }

    /// <summary>
    /// 音乐暂停/恢复
    /// </summary>
    /// <param name="musicName"></param>
    /// <returns></returns>
    public Sound PauseMusic(string musicName, bool b)
    {
        Sound _sound;
        if (soundDic.TryGetValue(musicName, out _sound))
        {
            _sound.playing = b;
        }
        return _sound;
    }

    /// <summary>
    /// 音乐停止
    /// </summary>
    /// <param name="musicName"></param>
    public void StopMusic(string musicName)
    {
        Sound _sound;
        if (soundDic.TryGetValue(musicName, out _sound))
        {
            _sound.Finish();
        }
    }

    void Update()
    {
        for (int i = 0; i < soundDic.Count; i++)
        {
            Sound _sound = soundDic.Values.ElementAt(i);
            string _key = soundDic.Keys.ElementAt(i);
            _sound.Update();
            if (_sound.source == null)
            {
                soundDic.Remove(_key);
            }
        }
    }

    /// <summary>
    /// 背景音乐(只能存在一个，并且是循环的，播放新的会自动替换旧的)
    /// </summary>
    private AudioSource bgmSource;

    /// <summary>
    /// BGM大小
    /// </summary>
    public float bgmVol = 0.5f;

    void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="isLoop"></param>
    public void PlayBGM(AudioClip clip, bool isLoop)
    {
        bgmSource.clip = clip;
        bgmSource.loop = isLoop;
        bgmSource.volume = bgmVol;
        bgmSource.Play();
    }

    /// <summary>
    /// 暂停/恢复背景音乐
    /// </summary>
    public void PauseBGM()
    {
        if (bgmSource.clip == null)
        {
            Debug.LogWarning("没有设置背景音乐");
            return;
        }
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
        else
        {
            bgmSource.UnPause();
        }
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
