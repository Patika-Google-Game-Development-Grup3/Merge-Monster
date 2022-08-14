using UnityEngine;

public class ItemInfo : MonoBehaviour 
{
    public int slotId;
    public int itemId;

    public SpriteRenderer visualRenderer;

    public MeshFilter meshFilter;

    public void InitDummy(int slotId, int itemId) 
    {
        this.slotId = slotId;
        this.itemId = itemId;
        visualRenderer.sprite = Utils.GetItemVisualById(itemId);
    }
    
    public void InitDummy3D(int slotId, int itemId) 
    {
        this.slotId = slotId;
        this.itemId = itemId;
        meshFilter.mesh = Utils.GetItemMeshById(itemId);
    }
}