using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioFetcher _audioFetcher = null;

    public void Update()
    {
        if (_audioFetcher != null)
        {
            if (!_audioFetcher.AudioSorce.isPlaying) { _audioFetcher.LoadNextAudio(); }
        }
    }
}
