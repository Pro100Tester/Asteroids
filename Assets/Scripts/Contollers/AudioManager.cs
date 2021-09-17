using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{

    public static AudioManager audioManager { get; private set; }
    [SerializeField, Header("Your AudioClips")]
    private List<AudioClip> sounds;

    [SerializeField, Header("Your AudioSource")]
    private List<AudioSource> audioSource;

    private List<AudioSource> ListOFAudioSurces = new List<AudioSource>();

    private void Awake()
    {
        audioManager = this;
        //ListOFAudioSurces.Clear();
        #region Convert AudioClip to AudioSource
        foreach (AudioClip s in sounds)
        {
            AudioSource A = gameObject.AddComponent<AudioSource>();
            A.clip = s;
            ListOFAudioSurces.Add(A);

        }
        #endregion

        //добовляет в лист уже существующие  AudioSource
        ListOFAudioSurces.AddRange(audioSource);

    }


    private AudioSource FindByName(string name)
    {
        AudioSource audio = ListOFAudioSurces.Find(n => n.clip.name == name);

        if (audio == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            //throw new System.Exception("Sound not found");
        }
        return audio;
    }

    public void PlayOneShot(in string name)
    {
        AudioSource audio = FindByName(name);
        audio.PlayOneShot(audio.clip);
    }
    public void Play(in string name) => FindByName(name).Play();

}
