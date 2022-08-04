using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private Vector3 mOffset;

    float mZCoord;

    public bool mouseButtonReleased;

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

    

    
}
