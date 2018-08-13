using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private float prevHeight;

    public float moveSpeed = 4.0f;
    public float jumpSpeed = 3.0f;

    private Rigidbody2D rb;

    // 현재 플레이어가 점프 중인지 아닌지를 판단
    private bool jumped;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        jumped = false;
    }

    void Update()
    {
        float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        // 움직임에 따라 이미지 좌우반전
        if (distanceX < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (distanceX > 0) transform.localScale = new Vector3(1, 1, 1);

        // 인자 값 만큼 이동
        transform.Translate(distanceX, 0, 0);

        if (prevHeight == transform.position.y)
        {
            Jump();
        }

        prevHeight = transform.position.y;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        // 플렛폼과 충돌되어 있다면
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Block"))
        {
            // 현재 점프하고 있지 않을때만
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // 플렛폼과 처음 충돌 시
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Block"))
        {
            jumped = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumped)
        {
            rb.AddForce(Vector2.up * jumpSpeed * Time.deltaTime, ForceMode2D.Impulse);
            jumped = true;
        }
    }

}