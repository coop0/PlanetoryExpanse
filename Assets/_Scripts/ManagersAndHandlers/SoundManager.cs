using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource _audioSource { get; private set; }
    [SerializeField] private AudioSource _audioObject;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _FireSFX;
    [SerializeField] private AudioClip _HitSFX;
    [SerializeField] private AudioClip _ButtonSFX;
    [SerializeField] private List<AudioClip> _pianoNotes;
    [SerializeField] private Queue<AudioClip> _clipQueue;

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        _clipQueue = new();
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

    int lastNote;
    public void PlayPercentage(float p)
    {
        int note = Mathf.RoundToInt(_pianoNotes.Count * p); // Offset reduces murky sounds
        // Adding a random element increases interest when jiggling around the borders.
        if (note == lastNote) note += Random.Range(-1, 1);
        if (note >= _pianoNotes.Count) note = _pianoNotes.Count - Random.Range(1, 2);
        if(note < 0) note = Random.Range(0, 2);

        if(_clipQueue.Count < 1) // This makes jazz. The theory is complicated.
        {
            _clipQueue.Enqueue(_pianoNotes[note]);
            lastNote = note;
        }
        else
        {
            print("spamProofing " + note);
        }
    }

    private void FixedUpdate()
    {
        if(_clipQueue.Count > 0) StartCoroutine(QueuePause());
    }

    WaitForSeconds shortPause;
    bool paused = false;
    IEnumerator QueuePause()
    {
        if(shortPause == null)
        {
            shortPause = new WaitForSeconds(0.15f);
        }

        if (!paused)
        {
            paused = true;
            PlaySFXClip(_clipQueue.Dequeue(), transform, 1f);
            yield return shortPause;
            paused = false;
        }
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
