using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
//Upward movespeed 
    private float upSpeed = 3f;
    private Rigidbody2D platformRB;
//x position platform starts at
    private float xStart;
    private float yStart;

    //Array of possible positions on x axis
    private int[] xLocation = { -4, 0, 4 };

// Start is called before the first frame update
    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        xStart = platformRB.position.x;
        yStart = platformRB.position.y;
    }

// Update is called once per frame
    void Update()
    {
        platformRB.velocity = new Vector2(0, upSpeed); 
        SpawnPlatform();
     
    }
// Selecting a random respawn location for the platform from the array and rate of the platform spawning
    private void SpawnPlatform()
    {
        int randomXPos = Random.Range(0, xLocation.Length);
    //When yPos exceeds the bounds; move it & generate new platform from array of possible  positions
        if (transform.position.y > yStart + 5)
        {
            int xPos = xLocation[randomXPos];
            int yPos = (int)yStart;

            transform.position = new Vector2(xPos, yPos);
        }
    }
}
