using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Playlist")]
    public AudioClip[] playlist;
    private int _currentIndex = 0;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (playlist.Length > 0)
        {
            PlayCurrentTrack();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _audioSource.mute = !_audioSource.mute;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            NextTrack();
        }
    }

    void NextTrack()
    {
        if (playlist.Length == 0) return;

        _currentIndex++;

     
        if (_currentIndex >= playlist.Length)
        {
            _currentIndex = 0;
        }

        PlayCurrentTrack();
    }

    void PlayCurrentTrack()
    {
        _audioSource.clip = playlist[_currentIndex];
        _audioSource.Play();
        Debug.Log("Acum cântă melodia: " + playlist[_currentIndex].name);
    }
}