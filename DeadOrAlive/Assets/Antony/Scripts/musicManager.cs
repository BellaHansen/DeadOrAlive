using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip track1; 
    public AudioClip track2; 
    public AudioClip track3;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        
        PlayMusic(track1);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SwitchToTrack1()
    {
        StartCoroutine(SwitchTrackCoroutine(track1));
    }

    public void SwitchToTrack2()
    {
        StartCoroutine(SwitchTrackCoroutine(track2));
    }

    public void SwitchToTrack3()
    {
        StartCoroutine(SwitchTrackCoroutine(track3));
    }

    private IEnumerator SwitchTrackCoroutine(AudioClip newClip)
    {
        yield return StartCoroutine(FadeOutCoroutine(1f)); 
        PlayMusic(newClip);
        yield return StartCoroutine(FadeInCoroutine(1f)); 
    }

    public void FadeInMusic(float duration)
    {
        StartCoroutine(FadeInCoroutine(duration));
    }

    public void FadeOutMusic(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1f;
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
