using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }
    public AudioClip[] audioSources;
    private AudioSource audioSourceEffect;

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSourceEffect = GetComponent<AudioSource>();
    }
    //播放音效
    public void PlayAudioEffect(int order)
    {
        audioSourceEffect.clip = audioSources[order];
        audioSourceEffect.Play();
    }
}
