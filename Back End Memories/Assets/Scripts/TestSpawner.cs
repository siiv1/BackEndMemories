using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject explosion;
    int ticks;

    // Start is called before the first frame update
    void Start()
    {
        ticks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (++ticks == 300)
        {
            Instantiate(explosion, GetComponent<Rigidbody2D>().position, Quaternion.identity);
            ticks = 0;
        }
    }
}
