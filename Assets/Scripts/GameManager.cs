using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishingGame;

public class GameManager : MonoBehaviour
{
    public GameSettingsAsset settingsAsset;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Load Settings")]
    public void LoadSettings()
    {
        if (settingsAsset != null)
        {
            GameSettings.LoadSettingsFromAsset(settingsAsset);
            Debug.Log("GameManager successfully loaded GameSettingsAsset!");
        }
        else
        {
            throw new System.NullReferenceException("There isn't a GameSettingsAsset attached to the GaneManager.");
        }
    }
}
