using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameResources : ScriptableObject 
{
    public List<Sprite> items = new List<Sprite>();

    public List<GameObject> meshes = new List<GameObject>();
}