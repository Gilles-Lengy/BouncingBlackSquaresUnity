using UnityEngine;
using System.Collections;

public class RandomVelocity : MonoBehaviour {

    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            rb.velocity = new Vector2(15,15);

    }
}
