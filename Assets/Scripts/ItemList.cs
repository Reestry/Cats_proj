using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemList")]
public class ItemList : ScriptableObject
{
    [SerializeField] private List<Rigidbody> _itemList = new();

    public List<Rigidbody> ItemLists => _itemList;
}
