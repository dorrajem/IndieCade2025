using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource themeSource;
    
    public List<AudioClip> CardSelect;
    public List<AudioClip> CardPlace;
    public List<AudioClip> CardAttack;
    public List<AudioClip> CardDie;
    public List<AudioClip> CardSacrifice;
    public AudioClip Theme;
    public AudioClip BossTheme;
    public AudioClip OfficeTheme;
    
    public static AudioManager AudioInstance;

    private void Awake()
    {
        // Singleton setup
        if (AudioInstance == null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(gameObject); 
            
            themeSource.clip = Theme;
            themeSource.loop = true;  
            themeSource.Play(); 
            RebindAudioSource();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RebindAudioSource();
    }

    private void RebindAudioSource()
    {
        var cam = GameObject.FindWithTag("MainCamera");
        if (cam != null)
        {
            audioSource = cam.GetComponent<AudioSource>();
        }
    }

    private void OnDestroy()
    {
        if (AudioInstance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    
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
        if (audioSource == null)
        {
            RebindAudioSource();
            if (audioSource == null) return; 
        }
        int index = Random.Range(0, clips.Count);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clips[index]);
        audioSource.pitch = 1f;
    }
}
