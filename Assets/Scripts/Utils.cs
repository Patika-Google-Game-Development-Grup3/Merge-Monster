using UnityEngine;

public static class Utils
{

    public static GameResources gameResources;

    public static GameResources InitResources()
    {
        Debug.Log("Resources initialized.");
        return gameResources = Resources.Load<GameResources>("GameResources");
    }

    public static Sprite GetItemVisualById(int itemId)
    {
        return gameResources.items[itemId];
    }
    
    public static GameObject GetItemMeshById(int itemId)
    {
        return gameResources.meshes[itemId];
    }

    public static GameObject GetGameObjectById(int itemId, int type)
    {
        switch (type)
        {
            case 0:
                return gameResources.itemGOs[itemId];
            case 1:
                return gameResources.itemGOs[itemId];
            default:
                return null;
        }
    }
}