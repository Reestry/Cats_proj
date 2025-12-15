using System;
using UnityEngine;

public class JoinableItem : MonoBehaviour
{
    private Transform _joinPoint;
    private void OnTriggerEnter(Collider other)
    {

    }

    public Transform ReturnJoinPoint()
    {
        return _joinPoint;
    }
}