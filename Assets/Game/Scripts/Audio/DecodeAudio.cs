using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DecodeAudio : MonoBehaviour
{
    [SerializeField] private AudioData _audioData = null;

    private void Awake()
    {
        foreach (AudioClip clip in _audioData.AudioClips)
        {
            //string temp = Application.dataPath + "/Temp/1.wav";
            //using (FileStream stream = File.OpenWrite(temp))
            //{

            //}

            clip.UnloadAudioData();
        }
    }
}
