using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObjects : MonoBehaviour
{
    void Start()
    {
        var settings = SettingsController.Instance;
        settings.LoadSettings();
        var itemIds = settings._userSettingsSO.UserSettings.itemId;
        var slotIds = settings._userSettingsSO.UserSettings.slotId;

        var cnt = 0;
        for (int i = 0; i < 64; i++)
        {
            if (slotIds.Contains(i))
            {
                var desiredSlot = slotIds[cnt];
                var desiredItem = itemIds[cnt];
                cnt++;
                Debug.Log($"desired position {desiredSlot}, desired object {desiredItem}");
                
            }
        }

    }

}
