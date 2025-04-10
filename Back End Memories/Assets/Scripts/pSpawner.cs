using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pSpawner : MonoBehaviour
{
    public GameObject movingPlatform;
    public static int maxPlatforms = 6;

    // Start is called before the first frame update
    void Start()
    {
        int platformCount = 0;
        Instantiate(movingPlatform, GetComponent<Rigidbody2D>().position,Quaternion.identity);
        while (++platformCount < maxPlatforms) Instantiate(movingPlatform);

    }
}
