using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleAudio : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public static AudioSource instancia;
    void Awake()
    {
        meuAudioSource = this.GetComponent<AudioSource>();
        instancia = meuAudioSource;
    }
}
