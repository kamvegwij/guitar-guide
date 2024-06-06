using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip cardFlipSound;
    [SerializeField] private AudioClip cardMatchSound;
    [SerializeField] private AudioClip cardIncorrectSound;
    [SerializeField] private AudioClip gameOverSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip)
    {
        if (!audioSource.isPlaying && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
    public void PlayFlipSound()
    {
        PlaySound(cardFlipSound);
    }
    public void PlayMatchSound()
    {
        PlaySound(cardMatchSound);
    }
    public void PlayIncorrectSound()
    {
        PlaySound(cardIncorrectSound);
    }
    public void PlayGameOverSound()
    {
        PlaySound(gameOverSound);
    }
}
