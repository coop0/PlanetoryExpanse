using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource _audioSource { get; private set; }
    [SerializeField] private AudioSource _sfxObj;
    [SerializeField] private AudioSource _pianoObj;
    [SerializeField] private AudioSource _harpObj;
    [SerializeField] private AudioSource _timpaniObj;
    [SerializeField] private AudioSource _musicObj;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private List<AudioClip> _pianoNotes;
    [SerializeField] private List<AudioClip> _harpNotes;
    [SerializeField] private List<AudioClip> _timpaniNotes;
    [SerializeField] private Queue<AudioClip> _pianoQueue;
    private int lastNote; // Reused for piano and timpani on purpose.

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        _pianoQueue = new();
        StartAmbience();
    }
    private void StartAmbience()
    {
        PlayMusicClip(null);
    }
    public void PlayMusicClip(AudioClip track)
    {
        var obj = Instantiate(_musicObj, transform);
        if(track != null)
        {
            obj.clip = track;
        }
    }
    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, AudioSource sourceObject=null)
    {
        AudioSource audioSource;
        if (sourceObject == null)
        {
            // Create instance of prefab and assign parameters.
             audioSource = Instantiate(_sfxObj, spawnTransform.position, Quaternion.identity);
        }
        else
        {
            audioSource = Instantiate(sourceObject, spawnTransform.position, Quaternion.identity);
        }
        audioSource.clip = audioClip;
        audioSource.volume = 1f;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    [ContextMenu("Random timpani")]
    public void PlayRandomTimpani()
    {
        int rand = lastNote;
        while (rand == lastNote)
        {
            rand = Random.Range(0, _timpaniNotes.Count);
        }
        PlaySFXClip(_timpaniNotes[rand], transform, _timpaniObj);
    }
    [ContextMenu("Random note")]
    public void PlayRandomHarp()
    {
        int rand = lastNote;
        while (rand == lastNote)
        {
            rand = Random.Range(0, _harpNotes.Count);
        }
        PlaySFXClip(_harpNotes[rand], transform, _harpObj);
    }

    public void PlayPercentage(float p)
    {
        int note = Mathf.RoundToInt(_pianoNotes.Count * p);
        // Adding a random element increases interest when jiggling around the borders.
        if (note == lastNote) note += Random.Range(-1, 1);
        if (note >= _pianoNotes.Count) note = _pianoNotes.Count - Random.Range(1, 2);
        if(note < 0) note = Random.Range(0, 2);

        if(_pianoQueue.Count < 1) // This makes jazz. The theory is complicated.
        {
            _pianoQueue.Enqueue(_pianoNotes[note]);
            lastNote = note;
        }
        else
        {
            print("spamProofing " + note);
        }
    }

    private void FixedUpdate()
    {
        if(_pianoQueue.Count > 0) StartCoroutine(QueuePause());
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
            PlaySFXClip(_pianoQueue.Dequeue(), transform, _pianoObj);
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
