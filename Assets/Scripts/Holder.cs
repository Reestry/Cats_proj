using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField] private Transform _holderPosition;

    private Item _holdItem;


    public void SetItem(Item item)
    {
        _holdItem = item;
        _holdItem.GetComponent<Rigidbody>().isKinematic = true;
        _holdItem.GetComponent<Collider>().enabled = false;
        
        _holdItem.transform.DOMove(_holderPosition.position, 0.2f)
            .SetEase(Ease.Flash)
            .SetAutoKill(true);
    }

    private void Update()
    {
        if (_holdItem == null)
            return;

        _holdItem.transform.position = _holderPosition.position;
        _holdItem.transform.rotation = _holderPosition.rotation;
    }

    public Item UnsetItem()
    {
        if (_holdItem == null)
            return null;

        _holdItem.GetComponent<Collider>().enabled = true;
        _holdItem.GetComponent<Rigidbody>().isKinematic = false;
        var item = _holdItem;
        _holdItem = null;

        return item;
    }

    public bool HasItem()
    {
        return _holdItem != null;
    }
    
}
