using UnityEngine;

public class ItemInfo : MonoBehaviour 
{
    public int slotId;
    public int itemId;

    public int parentSlotId;

    public int itemType;

    public SpriteRenderer visualRenderer;

    public GameObject meshFilter;
    public GameObject model;

    public void InitDummy(int slotId, int itemId) 
    {
        this.slotId = slotId;
        this.itemId = itemId;
        visualRenderer.sprite = Utils.GetItemVisualById(itemId);
    }

    public GameObject InitDummy3D(int id, int slotId, int type)
    {
        this.itemType = type;
        this.itemId = id;
        parentSlotId = slotId;
        model = Utils.GetGameObjectById(id, type);

        return model;
    }

}