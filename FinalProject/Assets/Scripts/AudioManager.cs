using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    public struct AudioPool
    {
        public string tag;
        public AudioClip clip;
    }
    [SerializeField]
    List<AudioPool> _audioClips;
    private bool muted;

    private AudioSource _as;
    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }
    public void PlayClip(string tag, Vector3 position)
    {
        foreach (var item in _audioClips)
        {
            if (item.tag == tag)
            {
                _as.clip = item.clip;
                _as.gameObject.transform.position = position;
                _as.Play();
                return;
            }
        }
    }

    public void OnSoundButtonDown() 
    {
        if (muted)
        {
            AudioListener.volume = 1.0f;
        }
        else if (!muted)
        {
            AudioListener.volume = 0.0f;
        }
        muted = !muted;
    }
}