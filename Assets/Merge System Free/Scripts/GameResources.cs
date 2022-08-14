using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameResources : ScriptableObject 
{
    public List<Sprite> items = new List<Sprite>();

    public List<Mesh> meshes = new List<Mesh>();
}