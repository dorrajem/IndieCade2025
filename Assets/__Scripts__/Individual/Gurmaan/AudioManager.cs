using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    //Caleb's Edit
    //public static AudioManager Instance;


    public AudioSource audioSource;
    
    public List<AudioClip> CardSelect;
    public List<AudioClip> CardPlace;
    public List<AudioClip> CardAttack;
    public List<AudioClip> CardDie;
    public List<AudioClip> CardSacrifice;
    
    public void PlayCardSelect()
    {
        PlayRandomClip(CardSelect);
    }

    public void PlayCardPlace()
    {
        PlayRandomClip(CardPlace);
    }

    public void PlayCardAttack()
    {
        PlayRandomClip(CardAttack);
    }

    public void PlayCardDie()
    {
        PlayRandomClip(CardDie);
    }

    public void PlayCardSacrifice()
    {
        PlayRandomClip(CardSacrifice);
    }
    
    private void PlayRandomClip(List<AudioClip> clips)
    {
        int index = Random.Range(0, clips.Count);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clips[index]);
        audioSource.pitch = 1f;
    }


    //Caleb's Edit
    /*
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this across scenes
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
    */
}
