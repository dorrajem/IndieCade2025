using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
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
}
