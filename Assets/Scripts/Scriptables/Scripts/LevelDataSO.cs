using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level Data/Level ", order = 1)]
public class LevelDataSO : ScriptableObject
{
   public LevelData LevelDatas;
   public PlayerData PlayerDatas;
   public UIData UIDatas;
   public GridData GridDatas;
   public List<CharacterPropertiesSO> CharacterDatas;
   
   [System.Serializable]
   public class LevelData
   {
      public Material LevelBackground;
      public GameObject LevelParentPrefab;
   }
   
   [System.Serializable]
   public class PlayerData
   {
      public float PlayerTotalMoney;
   }
   
   [System.Serializable]
   public class UIData
   {
      public Canvas ButtonCanvas;
      public Button ArcherButton;
      public Button MeleeButton;
   }

   [System.Serializable]
   public class GridData
   { 
      public GameObject GridPrefab;
      public GameObject GridParentObject;
      public GameObject[,] GridArray;
      public int GridColumns;
      public int GridRows;
   } 
   
   [System.Serializable]
   public class CharacterData
   {
      public CharacterPropertiesSO characterData;
   }
}
