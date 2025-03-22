using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    public float moveSpeed = 5f;
    private static float playerMvmtMult = 0.1f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Moving the character according to speed
    void Update()
    {
        rb.velocity = new Vector2(playerMvmtMult * horizontalMovement * moveSpeed, rb.velocity.y);   
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }
}
