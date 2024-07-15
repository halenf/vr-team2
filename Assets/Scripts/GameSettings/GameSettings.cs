using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public static class GameSettings
    {
        public static Vector3 POOL_ORIGIN;
        public static float POOL_RADIUS;
        public static int MAX_FISH_COUNT;

        public static float POOL_HEIGHT { get { return POOL_ORIGIN.y; } }
        
        public static Vector3 get_random_position_in_pool()
        {
            float radius = Random.Range(0, POOL_RADIUS);
            float angle = Random.Range(0, 2f * Mathf.PI);

            Vector3 position = new Vector3(POOL_ORIGIN.x + radius * Mathf.Cos(angle),
                                           POOL_HEIGHT,
                                           POOL_ORIGIN.z + radius * Mathf.Sin(angle));

            return position;
        }

        public static void LoadSettingsFromAsset(GameSettingsAsset asset)
        {
            POOL_ORIGIN = asset.poolOrigin;
            POOL_RADIUS = asset.poolRadius;
            MAX_FISH_COUNT = asset.maxFishCount;
        }
    }
}
