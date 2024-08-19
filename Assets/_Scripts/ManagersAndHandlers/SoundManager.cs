using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SoundManager Instance {  get; private set; }
    public AudioSource _audioSource { get; private set; }
    [SerializeField] private AudioSource _audioObject;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _FireSFX;
    [SerializeField] private AudioClip _HitSFX;
    [SerializeField] private AudioClip _ButtonSFX;
    [SerializeField] private List<AudioClip> _pianoNotes;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Create instance of prefab and assign parameters.
        AudioSource audioSource = Instantiate(_audioObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    [ContextMenu("Random note")]
    public void PlayRandomPianoNote()
    {
        int rand = Random.Range(0, _pianoNotes.Count);
        PlaySFXClip(_pianoNotes[rand], transform, 1f);
    }


    /// <summary>
    /// I tried lol
    /// </summary>
    
    //private void LoadPiano()
    //{
    //    _pianoNotes = Resources.LoadAll<AudioClip>("Piano").ToList();
    //    _pianoNotes.OrderBy(n => n.frequency);
    //}
    //private int CustomSort(AudioClip clip)
    //{
    //    int i = int.Parse(clip.name.Split("_")[0]);
    //    print("clip.name: " + clip.name + ", i: " + i);
    //    return i;
    //}
}
