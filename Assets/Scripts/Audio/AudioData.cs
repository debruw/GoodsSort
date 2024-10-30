using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/Audio data", order = 0)]
    public class AudioData : SerializedScriptableObject
    {
        [DictionaryDrawerSettings(KeyLabel = "AudioID", ValueLabel = "AudioClip")]
        public Dictionary<AudioID, AudioClip> AudioClips = new Dictionary<AudioID, AudioClip>();

        public AudioClip GetAudio(AudioID timesUp)
        {
            return AudioClips[timesUp];
        }
    }
}