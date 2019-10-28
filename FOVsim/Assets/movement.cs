using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float MovementSpeed = 3f;
    public Rigidbody2D rb;
    public Vector2 movemented;

    // Update is called once per frame
    void Update()
    {
        movemented.x = Input.GetAxisRaw("Horizontal");
        movemented.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movemented * MovementSpeed * Time.fixedDeltaTime);
    }
}
