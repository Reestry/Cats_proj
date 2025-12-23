using System;
using DefaultNamespace;
using DG.Tweening;
using Player;
using UnityEngine;

public abstract class PhysicButton : MonoBehaviour, IInteractableItem<object>, IButton
{
    public event Action OnButtonClick;
    
    public virtual object Interact()
    {
        transform.DOLocalMoveZ(-0.08f, 0.3f).SetEase(Ease.InOutCirc).OnComplete(() =>
            transform.DOLocalMoveZ(0, 0.3f).SetEase(Ease.InOutCirc)).SetAutoKill(true);
        OnButtonClick?.Invoke();
        return null;
    }

}
