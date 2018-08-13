using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour {

    private Rigidbody2D rb;

    public bool flying;

    public float flyHeight;

    [HideInInspector] public bool keepingSameHeight;

    public GameObject colStart;
    public GameObject colEnd;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 1.0f;

        keepingSameHeight = false;

        flying = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (flying)
        {
            Vector2 flyColStart = new Vector2(transform.position.x, transform.position.y - (transform.lossyScale.y / 2));
            Vector2 flyColEnd = new Vector2(flyColStart.x + 0.001f, flyColStart.y - flyHeight);

            //Instantiate(colStart, flyColStart, Quaternion.identity);
            //Instantiate(colEnd, flyColEnd, Quaternion.identity);

            if (Physics2D.OverlapArea(flyColStart, flyColEnd))
            {
                Collider2D[] col = Physics2D.OverlapAreaAll(flyColStart, flyColEnd);
                for (int i = 0; i < col.Length; i++)
                {
                    if (col[i].CompareTag("Ground"))
                    {
                        if (rb.gravityScale != 0.0f) GetComponent<MonsterInfo>().basicMoving = true;

                        rb.velocity = Vector3.zero;
                        rb.gravityScale = 0.0f;
                        keepingSameHeight = true;
                        break;
                    }
                }
            }

            if (rb.gravityScale != 0.0f)
                keepingSameHeight = false;
        }
    }
}
