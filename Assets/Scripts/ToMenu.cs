using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ToMenu : MonoBehaviour
{
    public static ToMenu instance;

    [SerializeField]
    private GameObject SetActiveSlots;
    
    [SerializeField]private GameController gameController;
    private int currentSceneIndex;

    [SerializeField]
    private Button BackFromFightScreenButton;
    private SettingsController _settings;

    private void Awake()
    {
        _settings = SettingsController.Instance;
        instance = this;
        BackFromFightScreenButton.onClick.AddListener(() => { LoadMainMenu(); });
    }

    public void LoadMainMenu()
    {
        
        SetActiveSlots.SetActive(false);
        var slots = gameController.slots;
        
        _settings._userSettingsSO.UserSettings.itemId = new List<int>();
        _settings._userSettingsSO.UserSettings.slotId = new List<int>();
        _settings._userSettingsSO.UserSettings.totalGold = UIManager.current.Gold;
        
        foreach (var slot in slots)
        {
            if (slot.currentItem == null)
            {
                continue;
            }
            
            var itemId = slot.currentItem.id;
            var slotId = slot.id;
            _settings._userSettingsSO.UserSettings.itemId.Add(itemId);
            _settings._userSettingsSO.UserSettings.slotId.Add(slotId);

            if (itemId%2 == 0)
            {
                _settings._userSettingsSO.UserSettings.archerCounter = gameController.ArcherCost;
            }
            else
            {
                _settings._userSettingsSO.UserSettings.meleeCounter = gameController.MeleeCost;
            }
        }
        
        _settings.SaveSettings();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        _settings._userSettingsSO.UserSettings.totalGold = UIManager.current.Gold;
        _settings.SaveSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
