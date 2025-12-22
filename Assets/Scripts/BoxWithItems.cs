using System;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BoxWithItems : MonoBehaviour, IInteractableItem<Rigidbody>


{

    
    [SerializeField] private ItemList _itemList;

    private List<Rigidbody> _items;

    private void OnEnable()
    {
        _items = new List<Rigidbody>(_itemList.ItemLists);
    }



    public Rigidbody Interact()
    {
        try
        {
            return GetItem();
        }
        catch
        {
            Debug.Log("Ничего нет :((");
            DeleteBox();
            return null;
        }
    }


    private Rigidbody GetItem()
    {
        if (_items.Count == 0)
            return null;
        
        var item = _items[Random.Range(0, _items.Count)];
        _items.Remove(item);
        var obj = Instantiate(item);
        obj.transform.position = transform.position;
        return obj;

    }

    public void DeleteBox()
    {
        var scale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(scale, 0.1f).SetEase(Ease.Flash).OnComplete(() =>
                transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack).OnComplete(() => Destroy(this)))
            .SetAutoKill(true);
    }
}
