using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : Singleton<InputManager>, IPointerDownHandler
{
    public static event Action<PointerEventData> onPointerDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(eventData);
    }
    
    

}
