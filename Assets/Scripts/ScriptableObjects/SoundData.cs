using Studio.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Studio/SoundData", order = 2)]
    public class SoundData : ScriptableObject
    {
        [SerializeField]
        public List<SoundInfo> sounds;

        [Serializable]
        public class SoundInfo
        {
            public Sounds soundType;
            public AudioClip audioClip;

            [Range(0, 1f)]
            public float volume = 1f;

            public bool isLoop;
            public bool isSfx;
        }
    }
}