using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioFetcher : MonoBehaviour
{
    [SerializeField] private AudioData _audioData = null;

    private string _audioPath = "";
    private AudioSource _audioSource = null;
    private AudioClip[] _clips = null;
    private int _clipIndex = -1;

    public AudioSource AudioSorce { get { return _audioSource; } }

    private void Awake()
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(_audioPath, AudioType.WAV);
        www.Dispose();
        _audioSource = this.GetComponent<AudioSource>();

    }

    public void LoadNextAudio()
    {
        _clipIndex = Random.Range(0, _audioData.AudioClips.Length);
        _audioSource.clip = GetClip(_clipIndex);
        _audioSource.Play();
    }

    private AudioClip GetClip(int index)
    {
        if (_audioData.AudioClips.Length <= 0) { return null; }

        if (index > _audioData.AudioClips.Length - 1) { index = 0; }

        _ = (index < 0) ? 0 : index;

        return _audioData.AudioClips[index];
    }
}
