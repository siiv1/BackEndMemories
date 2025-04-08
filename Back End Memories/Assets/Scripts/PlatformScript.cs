using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    //Upward movespeed
    private int platformId = 0;
    private float speed = 3f;
    private Rigidbody2D platformRB;
//x position platform starts at
    private float xStart;
    private float yStart;
    public float yDirection = 1;
    private float xDirection = 0;
    private Rigidbody2D player;

    //Array of possible positions on x axis
    private static float[] xLocations = { -3.5f, 0f, 3.5f };
    private static int lastX = 0;
    private static int platforms = 0;
    private static int maxPlatforms = 5;
    private static float initialY;
    private static float yOffset = -6f;

// Start is called before the first frame update
    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        platformId = platforms++;
        if (platformId == 0) yStart = initialY = platformRB.position.y;
        else yStart = initialY + platformId * yOffset;

        int nextX = UnityEngine.Random.Range(0, xLocations.Length);
        while (nextX == lastX) nextX = UnityEngine.Random.Range(0,xLocations.Length);
        lastX = nextX;
        transform.position = new Vector2(-.5f + xLocations[lastX],yStart);

        if (platformId < maxPlatforms) Instantiate(this);
    }

// Update is called once per frame
    void Update()
    {
        platformRB.velocity = new Vector2(speed * xDirection, speed * yDirection);
        MovePlatform();
    }

    void OnCollisionEnter2D(Collision2D collider) {
        player = collider.rigidbody;
    }
    void OnCollisionExit2D(Collision2D collider)
    {
        player = null;
    }
    // Selecting a random respawn location for the platform from the array and rate of the platform spawning
    private void MovePlatform()
    {
        float randomXPos = Random.Range(0, xLocations.Length);
        //When yPos exceeds the bounds; record position and reverse motion
        if (transform.position.y > yStart + 5)
        {
            yDirection = -1;
            if (player != null) player.velocity = new Vector2(player.velocity.x, speed * -1);
        }
        else if (transform.position.y < yStart) yDirection = 1;
    }
}
