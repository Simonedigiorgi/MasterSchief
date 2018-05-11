using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour {

    public int controls;

    public void Controller()
    {
        controls = 0;
        SaveData();
    }

    public void Mouse()
    {
        controls = 1;
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("controls", controls);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        controls = PlayerPrefs.GetInt("controls");
    }
}
