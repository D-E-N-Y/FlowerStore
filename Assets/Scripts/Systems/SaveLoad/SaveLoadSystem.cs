using UnityEngine;

public class SaveLoadSystem
{
    public static SaveLoadSystem current;

    public void Initialize()
    {
        current = this;
    }

    public void SaveData(string id, string parameter, float value)
    {
        PlayerPrefs.SetFloat($"{id}_{parameter}", value);
    }

    public float LoadData(string id, string parameter)
    {
        return PlayerPrefs.GetFloat($"{id}_{parameter}");
    }
}