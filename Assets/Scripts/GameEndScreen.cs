
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{
    public GameObject wonGame;
    public GameObject loseGame;
    private bool gameOver;
    private GameObject[] _enemy;

    private GameObject[] _ally;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;

    }

    private void Update()
    {
        _enemy = GameObject.FindGameObjectsWithTag("Enemy");
        _ally = GameObject.FindGameObjectsWithTag("Ally");
        
        
        if (_enemy.Length <= 0 && _ally.Length > 0)
        {
            gameOver = false;
            wonGame.SetActive(true);
            
        }

        if (_enemy.Length > 0 && _ally.Length <= 0)
        {
            gameOver = true;
            loseGame.SetActive(true);
        }
    }
}
