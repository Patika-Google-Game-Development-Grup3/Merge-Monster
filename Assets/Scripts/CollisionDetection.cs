using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollisionDetection : MonoBehaviour
{
    public bool IsPointEmpty;
    

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
    private void OnTriggerStay(Collider other)
    {
        gameObject.transform.position = DragObjects.pos;
    }
}
