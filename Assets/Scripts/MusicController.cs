using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    private AudioSource _audioSource;
    private IEnumerable<IButton> _buttons;

    private bool _isPlaying = false;

    private int _currentIndex;
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _buttons = GetComponentsInChildren<IButton>();
        
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
            _audioSource.Play();
        else
            _audioSource.Stop();
        Debug.Log("Play");
    }

    private void SetMusic(int index)
    {
        try
        { 
            if (_audioSource.clip == null)
                _audioSource.clip = _audioClips[index];
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
