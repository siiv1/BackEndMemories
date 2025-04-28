using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private Vector3 inc;
    private int tick;
    private static float knockbackForce = 15f;
    bool damaged;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(0f, 0f, 1f);
        inc = new Vector3(.08f, .08f, 0);
        tick = 0;
        damaged = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.localScale += inc;
        GetComponent<CircleCollider2D>().radius += .1f;
        transform.position = new Vector2(transform.position.x, transform.position.y + .1f);
        if (++tick > 40)
        {
            Destroy(gameObject);
        }
        else if (tick % 20 == 0)
        {
            inc = new Vector3(inc.x / 2, inc.y / 2, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        if (collision.tag == "Player" && !damaged) {
            go.GetComponent<PlayerValues>().health--;
            go.GetComponent<Rigidbody2D>().velocity = (collision.transform.position - transform.position) * knockbackForce;
            damaged = true;
        }
    }
}
