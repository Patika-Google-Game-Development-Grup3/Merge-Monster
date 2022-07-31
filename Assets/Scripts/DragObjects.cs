using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private Vector3 mOffset;

    float mZCoord;

    public static bool mouseButtonReleased;

    void OnMouseDown()
    {
        mouseButtonReleased = false;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        mouseButtonReleased = true;
    }

    private Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
   void OnMouseDrag()
   {
      transform.position = GetMouseWorldPos() + mOffset;
   }

   // Merge two object together when they collide with each other. 
    void OnTriggerStay(Collider collision)
    {
        string thisGameObject;
        string collisinGameObject;
    
        thisGameObject = gameObject.name.Substring(0, name.IndexOf("_"));
        collisinGameObject = collision.gameObject.name.Substring(0, name.IndexOf("_"));        
    
        if (mouseButtonReleased && thisGameObject == "Cube" && thisGameObject == collisinGameObject)
        {
            
            Instantiate(Resources.Load("Prefabs/Big_Cube"), transform.position, Quaternion.identity);
            mouseButtonReleased = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }else if (mouseButtonReleased && thisGameObject == "Big" && thisGameObject == collisinGameObject)
        {
            Instantiate(Resources.Load("Prefabs/Sphere_Object"), transform.position, Quaternion.identity);
            mouseButtonReleased = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
