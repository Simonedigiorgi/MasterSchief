using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        PlayerPrefs.SetInt("controller", controls);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        controls = PlayerPrefs.GetInt("controller");
    }
}
