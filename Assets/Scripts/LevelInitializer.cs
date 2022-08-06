using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : Singleton<LevelInitializer>
{
    public static event Action SpawnArena;
    public static event Action SpawnButtons; 

    private void Start()
    {
        SpawnGrid();
        SpawnUIElements();
    }

    public void SpawnGrid()
    {
        SpawnArena?.Invoke();
    }
    
    public void SpawnUIElements()
    {
        SpawnButtons?.Invoke();
    }
}
