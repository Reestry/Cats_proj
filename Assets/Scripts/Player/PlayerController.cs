using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly List<IInputable> _inputables = new();
    
    private void Awake()
    {
        
    }

    private void Update()
    {
        foreach (var input in _inputables)
            input.Run();
    }

    public void SetInput([CanBeNull] IInputable item)
    {
        item?.Init();
        _inputables.Add(item);
        
    }

    public void RemoveInput([CanBeNull] IInputable item)
    {
        _inputables.Remove(item);
    }
    
}
