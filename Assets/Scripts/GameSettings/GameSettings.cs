using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static Vector3 POOL_ORIGIN;
    public static Vector2 POOL_BOUNDS;
    public static int MAX_FISH_COUNT;

    public static float POOL_HEIGHT { get { return POOL_ORIGIN.y; } }

    public static void LoadSettingsFromAsset(GameSettingsAsset asset)
    {
        POOL_ORIGIN = asset.poolOrigin;
        POOL_BOUNDS = asset.poolBounds;
        MAX_FISH_COUNT = asset.maxFishCount;
    }
}
