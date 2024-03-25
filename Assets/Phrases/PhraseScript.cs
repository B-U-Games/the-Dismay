using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhraseScript : MonoBehaviour
{
    public TextMeshProUGUI Subtitles;
    private string text = "";

    public AudioClip WayBackPhrase;
    private bool _wayBackPlayed = false;
    public AudioClip BloodSeenPhrase;
    private bool _bloodSeenPlayed = false;
    public AudioClip FirstEncounterPhrase;
    private bool _firstEncounterPlayed = false;
    public AudioClip NoKeyPhrase;
    private bool _noKeyPlayed = false;
    public AudioClip SecretPhrase;
    private bool _secretPlayed = false;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWayBack()
    {
        if (!_wayBackPlayed)
        {
            StopCoroutine(SubtitlePrint("", 0f));
            SubtitleClear();
            audioSource.Stop();
            audioSource.PlayOneShot(WayBackPhrase);
            _wayBackPlayed = true;
            StartCoroutine(SubtitlePrint("It's too late to go back now", 0.065f));
            Invoke("SubtitleClear", 6f);
        }
    }

    public void PlayBloodSeen()
    {
        if (!_bloodSeenPlayed)
        {
            StopCoroutine(SubtitlePrint("", 0f));
            SubtitleClear();
            audioSource.Stop();
            audioSource.PlayOneShot(BloodSeenPhrase);
            _bloodSeenPlayed = true;
            StartCoroutine(SubtitlePrint("I need to find where all that blood comes from", 0.045f));
            Invoke("SubtitleClear", 6f);
        }
    }

    public void PlayFirstEncounter()
    {
        if (!_firstEncounterPlayed)
        {
            StopCoroutine(SubtitlePrint("", 0f));
            SubtitleClear();
            audioSource.Stop();
            audioSource.PlayOneShot(FirstEncounterPhrase);
            _firstEncounterPlayed = true;
            StartCoroutine(SubtitlePrint("What was that?", 0.065f));
            Invoke("SubtitleClear", 6f);
        }
    }

    public void PlayNoKey()
    {
        if (!_noKeyPlayed)
        {
            StopCoroutine(SubtitlePrint("", 0f));
            SubtitleClear();
            audioSource.Stop();
            audioSource.PlayOneShot(NoKeyPhrase);
            _noKeyPlayed = true;
            StartCoroutine(SubtitlePrint("I need a key, it must be somewhere near this fabric", 0.065f));
            Invoke("SubtitleClear", 6f);
        }
    }

    public void PlaySecret()
    {
        if (!_secretPlayed)
        {
            StopCoroutine(SubtitlePrint("", 0f));
            SubtitleClear();
            audioSource.Stop();
            audioSource.PlayOneShot(SecretPhrase);
            _secretPlayed = true;
            StartCoroutine(SubtitlePrint("Looks like Harrison did make it now", 0.065f));
            Invoke("SubtitleClear", 6f);
        }
    }

    private IEnumerator SubtitlePrint(string str, float delay)
    {
        foreach (var sym in str)
        {
            Subtitles.text += sym;
            yield return new WaitForSeconds(delay);
        }
    }

    private void SubtitleClear()
    {
        Subtitles.text = string.Empty;
    }
}
