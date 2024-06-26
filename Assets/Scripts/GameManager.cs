using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishingGame;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettingsAsset m_settingsAsset;
    
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
        if (m_settingsAsset != null)
        {
            GameSettings.LoadSettingsFromAsset(m_settingsAsset);
            Debug.Log("GameManager successfully loaded GameSettingsAsset!");
        }
        else
        {
            throw new System.NullReferenceException("There isn't a GameSettingsAsset attached to the GameManager.");
        }
    }
}
