using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundContainer : MonoBehaviour{
    [SerializeField] private AudioClip sound;

    public AudioClip Sound
    {
        get => sound;
        set => sound = value;
    }
}
