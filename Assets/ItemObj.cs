using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items")]
public class ItemObj : ScriptableObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private int _cost;
    [SerializeField] private int _tag;

    public GameObject Model => _model;
    public int Cost => _cost;
    public int Tag => _tag;

    public Mesh Mesh
    {
        get
        {
            if (_model != null)
            {
                var mf = _model.GetComponentInChildren<MeshFilter>();
                if (mf != null)
                    return mf.sharedMesh;
            }
            return null;
        }
    }
    
    public Material Material
    {
        get
        {
            if (_model != null)
            {
                var mr = _model.GetComponentInChildren<MeshRenderer>();
                if (mr != null)
                    return mr.sharedMaterial;
            }
            return null;
        }
    }

    public Vector3 Scale
    {
        get
        {
            if (_model == null)
                return new Vector3(1,1,1);

            var scale = _model.transform.localScale;
            return scale;
        }
    }
}
