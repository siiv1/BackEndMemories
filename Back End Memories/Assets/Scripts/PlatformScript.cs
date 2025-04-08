using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
//Upward movespeed 
    private float speed = 3f;
    private Rigidbody2D platformRB;
//x position platform starts at
    private float xStart;
    private float yStart;
    public float yDirection = 1;
    private float xDirection = 0;

    //Array of possible positions on x axis
    private static float[] xLocations = { -3.5f, 0f, 3.5f };

// Start is called before the first frame update
    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        yStart = platformRB.position.y;

        transform.position = new Vector2(-.5f + xLocations[UnityEngine.Random.Range(0, xLocations.Length)],yStart);
    }

// Update is called once per frame
    void Update()
    {
        platformRB.velocity = new Vector2(speed * xDirection, speed * yDirection);
        MovePlatform();
    }

    void OnCollisionEnter2D(Collision2D collider) {
        Debug.Log("Set collider parent to transform");
        collider.transform.SetParent(transform);
    }
    void OnCollisionExit2D(Collision2D collider)
    {
        Debug.Log("Set collider parent to null");
        collider.transform.SetParent(null);
    }
    // Selecting a random respawn location for the platform from the array and rate of the platform spawning
    private void MovePlatform()
    {
        float randomXPos = Random.Range(0, xLocations.Length);
        //When yPos exceeds the bounds; record position and reverse motion
        if (transform.position.y > yStart + 5) yDirection = -1;
        else if (transform.position.y < yStart) yDirection = 1;
    }
}
