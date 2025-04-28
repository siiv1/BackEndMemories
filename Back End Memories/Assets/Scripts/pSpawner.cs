using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class pSpawner : MonoBehaviour
{
    public GameObject movingPlatform;
    public GameObject battery;
    public static int maxPlatforms = 20;
    private GameObject lastPlatform;

    // Start is called before the first frame update
    void Start()
    {
        int platformCount = 0;
        Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/movingPlatform.prefab", typeof(GameObject)));
        while (/*(PlatformScript.GetBottom() - PlatformScript.yOffset) > -40f &&*/ ++platformCount < maxPlatforms) {
            lastPlatform = Instantiate(movingPlatform);
            float batteryRand = (UnityEngine.Random.Range(0f, 1f) - .5f) / .25f;
           if (batteryRand >= -1f) {
                Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BatteryPrefab.prefab", typeof(GameObject)), new Vector2(lastPlatform.transform.position.x + 0f*batteryRand, lastPlatform.transform.position.y + (PlatformScript.yOffset)/2f), Quaternion.identity);
           }
        }

    }
}
