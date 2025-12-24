using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    public event Action<string> OnTextEdited;
    private MusicInterface _musicInterface;
    private AudioSource _audioSource;
    private IEnumerable<IButton> _buttons;

    private bool _isPlaying = false;

    private int _currentIndex;
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _buttons = GetComponentsInChildren<IButton>();
        _musicInterface = GetComponent<MusicInterface>();
        
        foreach (var button in _buttons)
        {
            button.OnButtonClick += () => HandleClick(button);
        }
        
        _currentIndex = 0;
        SetMusic(_currentIndex);
    }

    private void HandleClick(IButton button)
    {
        switch (button)
        {
            case PlayButton:
                Play();
                break;
            case NextButton:
                Next();
                break;
        }
    }
    private void Play()
    {
        _isPlaying = !_isPlaying;

        if (_isPlaying)
        {
            SetMusic(_currentIndex);
            _audioSource.Play();
        }
        else
            _audioSource.Stop();
        Debug.Log("Play");
    }

    private void SetMusic(int index)
    {
        try
        { 
            _audioSource.clip = _audioClips[index];
            
            OnTextEdited?.Invoke(_audioSource.clip.name);
            _musicInterface.SetText(_audioSource.clip.name);
        }
        catch
        {
            Debug.Log("Нет песен");
        }

    }

    private void Next()
    {
        _currentIndex++;

        if (_audioClips.Count - 1 < _currentIndex)
            _currentIndex = 0;

        SetMusic(_currentIndex);


        Debug.Log("Next");
    }
}
