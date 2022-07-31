using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollisionDetection : MonoBehaviour
{
    public static CollisionDetection Instance;
    public bool IsPointEmpty;

    private void Start()
    {
        Instance = this;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Archer") || other.gameObject.CompareTag("Melee"))
        {
            IsPointEmpty = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IsPointEmpty = false;
    }
}
