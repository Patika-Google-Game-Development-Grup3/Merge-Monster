using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnSystem : Singleton<SpawnSystem>
{
   private GameObject[,] gridArray;
   private Vector3 SpawnPosition;
   private void Awake()
   {
      LevelInitializer.SpawnArena += SpawnArena;
      LevelInitializer.SpawnButtons += SpawnButtons;
      InputManager.onPointerDown += OnDown;
   }
   
   private void OnDestroy()
   {
      InputManager.onPointerDown -= OnDown;
      LevelInitializer.SpawnButtons -= SpawnButtons;
      LevelInitializer.SpawnArena -= SpawnArena;
   }

   public void SpawnArena()
   {
      var gridColumns = DataHandler.Instance.LevelData.GridDatas.GridColumns;
      var gridRows = DataHandler.Instance.LevelData.GridDatas.GridRows;
      gridArray = new GameObject[gridColumns, gridRows];
      
      var x_Space = 1.5f;
      var z_space = 1.5f;
      var leftBottomLocation = Vector3.zero;

      var gridPrefab = DataHandler.Instance.LevelData.GridDatas.GridPrefab;
      var gridParent = Instantiate(DataHandler.Instance.LevelData.GridDatas.GridParentObject);
      gridParent.transform.SetParent(GameObject.FindWithTag("Level").transform);

      for (int i = 0; i < gridColumns; i++)
      {
         for (int j = 0; j < gridRows; j++)
         {
            GameObject obj = Instantiate(gridPrefab, new Vector3(x_Space * (leftBottomLocation.x + i), 0f, z_space * (leftBottomLocation.z + j)), Quaternion.identity);
            obj.transform.SetParent(gridParent.transform);
            
            gridArray[i, j] = obj;
         }
      }

   }
   
   public void SpawnButtons()
   {
      var buttonCanvas = Instantiate(DataHandler.Instance.LevelData.UIDatas.ButtonCanvas);
      buttonCanvas.transform.SetParent(GameObject.FindWithTag("Level").transform);
      
      var archerButton = Instantiate(DataHandler.Instance.LevelData.UIDatas.ArcherButton);
      archerButton.transform.SetParent(buttonCanvas.transform);
      archerButton.onClick.AddListener(() => {SpawnArcher();});
      
      var meleeButton = Instantiate(DataHandler.Instance.LevelData.UIDatas.MeleeButton);
      meleeButton.transform.SetParent(buttonCanvas.transform);
      meleeButton.onClick.AddListener(() => {SpawnMelee();});
   }

   public void OnDown(PointerEventData eventData)
   {
      if (eventData.lastPress == gameObject.CompareTag("Archer"))
      {
         SpawnArcher();
      }
   
      if (eventData.lastPress == gameObject.CompareTag("Melee"))
      {
         SpawnMelee();
      }
   }
   
   public void SpawnArcher()
   {
      SpawnPosition = GetRandomGridPosition();
      var offset = new Vector3(0f, 0.5f, 0f);
      
      
      foreach (var grid in gridArray)
      {
         if (grid.GetComponent<CollisionDetection>().IsPointEmpty)
         {
            SpawnPosition = GetRandomGridPosition();
         }
         else
         {
            Instantiate(DataHandler.Instance.LevelData.CharacterDatas[0].CharacterPrefab, SpawnPosition + offset, Quaternion.identity);
            break;
         }
      }
      
      

   }
   public void SpawnMelee()
   {
      //Instantiate(DataHandler.Instance.LevelData.CharacterDatas[1].CharacterPrefab, spawnPosition + offset , Quaternion.identity);
   }

   public Vector3 GetRandomGridPosition()
   {
      var randomColumn = Random.Range(0, DataHandler.Instance.LevelData.GridDatas.GridColumns);
      var randomRow = Random.Range(0, DataHandler.Instance.LevelData.GridDatas.GridRows);
      
      return SpawnPosition = gridArray[randomColumn, randomRow].transform.position;
   }
   
  
}
