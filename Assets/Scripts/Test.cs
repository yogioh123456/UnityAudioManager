using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    

    // Use this for initialization
    void Start () {
        //am.PlayMusic(Resources.Load<AudioClip>("Dungeon"));
        //AudioSource source = gameObject.AddComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    int a;
    Sound s;
    public void PlayMusic01()
    {
        ++a;
        s = AudioManager.Instance.PlayMusic(a.ToString(), Resources.Load<AudioClip>("Lose Yourself"), false);
    }

    public void PauseMusic01()
    {
        if (s == null)
        {
            Debug.LogWarning("没有设置背景音乐");
            return;
        }
        s.playing = !s.playing;
    }

    public void PlayBGM01()
    {
        AudioManager.Instance.PlayBGM(Resources.Load<AudioClip>("Not Afraid"), true);
    }

    public void PauseBGM01()
    {
        AudioManager.Instance.PauseBGM();
    }
}
