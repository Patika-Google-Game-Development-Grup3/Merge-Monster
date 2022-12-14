using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Properties", menuName = "Scriptable Objects/Characters/Properties", order = 2)]
public class CharacterPropertiesSO : ScriptableObject
{
    public GameObject CharacterPrefab;
    public float CharacterHealth;
    public float CharacterAttackPower;
    public float CharacterAttackRange;
    public float CharacterMoveSpeed;
    public float CharacterAttackRate;
    public float ArcherCost = 1;
    public float MeleeCost = 1;
    
}
