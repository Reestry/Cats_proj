using System;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    //
    //[Serializefield]
    private void Update()
    {
        var rayHit = Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity);

        var shadowPos = hit.point;
        

        //if (hit)
    }

}