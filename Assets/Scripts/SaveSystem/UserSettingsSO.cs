using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings/User Settings", order = 1)]
public class UserSettingsSO : ScriptableObject
{
    public UserSettings UserSettings;
}

[System.Serializable]
public class UserSettings
{
    public List<int> itemId;
    public List<int> slotId;
}
