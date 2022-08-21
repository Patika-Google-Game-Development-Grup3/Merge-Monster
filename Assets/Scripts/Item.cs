using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public int type;
    public Slot parentSlot;

    public SpriteRenderer visualRenderer;
    public GameObject model;

    public void Init(int id, Slot slot)
    {
        this.id = id;
        this.parentSlot = slot;
        visualRenderer.sprite = Utils.GetItemVisualById(id);
    }
    
    public GameObject Init3D(int id, Slot slot, int type)
    {
        this.type = type;
        this.id = id;
        this.parentSlot = slot;
        model = Utils.GetGameObjectById(id, type);

        return model;
    }
}
