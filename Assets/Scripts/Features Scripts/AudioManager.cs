using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour{
    

    public AudioSound[] sounds;
    public static AudioManager instance;

    void Awake(){

        if (instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach(AudioSound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start(){
        Play("Theme");
    }
    public void Play (string name){
        AudioSound s = Array.Find(sounds, AudioSound => AudioSound.name == name);
        if(s == null){
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Play();
    }   
 }

[System.Serializable]
public class AudioSound
{


    public string name;
    public AudioClip clip;


    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

}



