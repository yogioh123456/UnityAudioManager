using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound {

    public AudioClip clip;
    public AudioSource source;
    public bool loop;

    /// <summary>
    /// 播放进度
    /// </summary>
    public float progress
    {
        get
        {
            if (source == null || clip == null)
                return 0f;
            return (float)source.timeSamples / (float)clip.samples;
        }
    }

    /// <summary>
    /// 判断是否完成播放
    /// </summary>
    public bool finished
    {
        get
        {
            return !loop && progress >= 1f;
        }
    }

    /// <summary>
    /// 播放与暂停
    /// </summary>
    public bool playing
    {
        get
        {
            return source != null && source.isPlaying;
        }
        set
        {
            if (value)
            {
                if (!source.isPlaying)
                {
                    source.UnPause();
                }
            }
            else
            {
                if (source.isPlaying)
                {
                    source.Pause();
                }
            }
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="clip"></param>
    public Sound(AudioClip clip, AudioSource source)
    {
        this.clip = clip;
        this.source = source;
        this.source.clip = clip;
        this.source.Play();
    }

    public void Update()
    {
        if (source != null)
        {
            source.loop = loop;
        }
        if (finished)
        {
            Finish();
        }
    }

    /// <summary>
    /// 播放完成的话
    /// </summary>
    public void Finish()
    {
        Object.Destroy(source);
        source = null;//null的话会被自动清除
    }
}
