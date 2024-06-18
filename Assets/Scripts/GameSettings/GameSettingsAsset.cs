using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettingsAsset : ScriptableObject
{
    [SerializeField] private Vector3 m_poolOrigin;
    [SerializeField] private Vector2 m_poolBounds;
    [SerializeField] private int m_maxFishCount;

    public Vector3 poolOrigin { get { return m_poolOrigin; } }
    public Vector2 poolBounds { get { return m_poolBounds; } }
    public int maxFishCount { get { return m_maxFishCount; } }
}
