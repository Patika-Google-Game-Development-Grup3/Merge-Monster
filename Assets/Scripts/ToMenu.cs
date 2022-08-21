using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
    public static ToMenu instance;
    [SerializeField]private GameController gameController;
    private int currentSceneIndex;

    private void Awake()
    {
        instance = this;
    }

    public void LoadMainMenu()
    {
        var slots = gameController.slots;
        var settings = SettingsController.Instance;
        settings._userSettingsSO.UserSettings.itemId = new List<int>();
        settings._userSettingsSO.UserSettings.slotId = new List<int>();

        foreach (var slot in slots)
        {
            if (slot.currentItem == null)
            {
                continue;
            }
            var itemId = slot.currentItem.id;
            var slotId = slot.id;
            settings._userSettingsSO.UserSettings.itemId.Add(itemId);
            settings._userSettingsSO.UserSettings.slotId.Add(slotId);
        }
        settings.SaveSettings();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
