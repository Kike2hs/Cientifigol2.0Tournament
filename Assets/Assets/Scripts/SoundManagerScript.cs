using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    /*private static AudioSource audioSource;
    private static AudioClip sonidoGol;

    void Start()  {
        audioSource = GetComponent<AudioSource>();
        sonidoGol = Resources.Load<AudioClip>("gol");
    }

    public static void playGol() {
        audioSource.PlayOneShot(sonidoGol);
    }*/

    public Sound[] sounds;

    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);

        foreach (Sound sonido in sounds) {
            sonido.source = this.gameObject.AddComponent<AudioSource>();
            
            sonido.source.clip   = sonido.audioClip;
            sonido.source.volume = sonido.volume;

            if (sonido.onLoop) {
                sonido.source.loop = sonido.onLoop;
                sonido.source.playOnAwake = sonido.onLoop;
                playSound(sonido.tipoSonido, sonido.volume);
            }
        }
    }

    public void playSound(TipoSonido tipoSonido, float? volumen) {
        var sound = Array.Find(sounds, sound => sound.tipoSonido == tipoSonido);
        if (sound.source.isPlaying) {
            print("sound is playing returning");
            return; 
        }

        sound.source.volume = volumen ?? sound.volume;
        sound.source.Play();
    }
}


[System.Serializable]
public class Sound
{
    public TipoSonido tipoSonido;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public bool onLoop;
}

[System.Serializable]
public enum TipoSonido
{
    GOL, REBOTE_BALON, SALTO, FONDO, RECOGER_OBJETO, BOTON_CLICK
}