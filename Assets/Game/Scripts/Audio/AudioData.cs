using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Data", menuName = "Game Data/Audio")]
public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip[] _audioClips = null;

    public AudioClip[] AudioClips { get { return _audioClips; } }
}
