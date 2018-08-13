using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJump : MonoBehaviour {

    public float jumpSpeed;

    private bool jumping;

    private MonsterInfo slime;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        slime = GetComponent<MonsterInfo>();
        rb = GetComponent<Rigidbody2D>();

        jumping = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (jumping)
        {
            slime.basicMoving = true;
        }
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Block"))
        {
            Jump();
        }
    }

    void Jump()
    {

        rb.AddForce(Vector2.up * jumpSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
