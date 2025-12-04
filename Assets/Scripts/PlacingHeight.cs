using System;
using UnityEngine;

public class PlacingHeight : MonoBehaviour
{
    private Vector3 _placePos;
    

    public Vector3 DropPlace()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, 4))
        {
            
            _placePos = hit.point;
        }
        return _placePos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1);
    }
}
