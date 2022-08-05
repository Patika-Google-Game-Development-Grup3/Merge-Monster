using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeObjects : MonoBehaviour
{
    
    // Merge two object together when they collide with each other. 
    void OnTriggerStay(Collider collision)
    {
        string thisGameObject;
        string collisinGameObject;
    
        thisGameObject = gameObject.name.Substring(0, name.IndexOf("_"));        
        collisinGameObject = collision.gameObject.name.Substring(0, name.IndexOf("_"));        
        
    
        if (DragObjects.mouseButtonReleased && thisGameObject == "Archer" && thisGameObject == collisinGameObject)
        {
            
            Instantiate(Resources.Load("Prefabs/Second_Archer"), transform.position, Quaternion.identity);
            DragObjects.mouseButtonReleased = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }else if (DragObjects.mouseButtonReleased && thisGameObject == "Second" && thisGameObject == collisinGameObject)
        {
            Instantiate(Resources.Load("Prefabs/Third_Archer"), transform.position, Quaternion.identity);
            DragObjects.mouseButtonReleased = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }else {
            if(DragObjects.mouseButtonReleased)
            {
                gameObject.transform.position = DragObjects.pos;
            }
        }
    }

    
}
