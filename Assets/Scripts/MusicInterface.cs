using System;
using TMPro;
using UnityEngine;

public class MusicInterface : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private MusicController _musicController;

    private void Start()
    {
        _musicController = GetComponent<MusicController>();

        _musicController.OnTextEdited += SetText;
        
        _text.text = "";
    }

    public void SetText(string title)
    {
        _text.text = title;
        
        Debug.Log(title);
    }
}
