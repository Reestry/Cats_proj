using UnityEngine;

public class TakeableItem : Item
{
    [SerializeField] private ItemObj _item;
    
    private int _cost;
    private int _tag;
    private GameObject _gameObject;

    private void OnEnable()
    {
        SetInfo(_item);
    }
    private void SetInfo(ItemObj item)
    {
        if (_item == null)
            return;
        
        _cost = item.Cost;
        _tag = item.Tag;

        if(item.Mesh != null)
            gameObject.GetComponent<MeshFilter>().mesh = item.Mesh;
        if(item.Material != null)
            gameObject.GetComponent<MeshRenderer>().material = item.Material;

        transform.localScale = item.Scale;
    }

    public override void Interact()
    {
        Debug.Log($"Нашлась коробочка Стоимость{_cost}");
    }
}
