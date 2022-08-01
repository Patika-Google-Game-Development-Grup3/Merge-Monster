using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject ArcherPrefab;
    public GameObject MeleePrefab;
    
    public GameObject[] ArcherSpawnPoints;
    public GameObject[] MeleeSpawnPoints;
    
    public Button ArcherButton;
    public Button MeleeButton;
    
    private Vector3 offset;
    private Transform spawnPoint;
    

    private void Start()
    {
        ArcherButton.onClick.AddListener(() => { SpawnArcher(); });
        MeleeButton.onClick.AddListener(() => { SpawnMelee(); });
    }
    

    public void SpawnArcher()
    {
        
        int index = Random.Range(0, ArcherSpawnPoints.Length);
        offset = new Vector3(0, 0.5f, 0);

        foreach (var point in ArcherSpawnPoints)
        {
            if (point.GetComponent<CollisionDetection>().IsPointEmpty)
            {
                spawnPoint = point.transform;
            }
            else
            {
                Instantiate(ArcherPrefab, point.transform.position + offset, Quaternion.identity);
                break;
            }
        }
    }
    
    public void SpawnMelee()
    {
        int index = Random.Range(0, MeleeSpawnPoints.Length);
        offset = new Vector3(0, 0.5f, 0);
        
        foreach (var point in MeleeSpawnPoints)
        {
            if (point.GetComponent<CollisionDetection>().IsPointEmpty)
            {
                spawnPoint = point.transform;
            }
            else
            {
                Instantiate(MeleePrefab, point.transform.position + offset, Quaternion.identity);
                break;
            }
        }
    }
    
    
}
