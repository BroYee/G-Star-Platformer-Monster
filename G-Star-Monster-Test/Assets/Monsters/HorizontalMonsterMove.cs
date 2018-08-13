using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMonsterMove : MonoBehaviour {

    [HideInInspector] public bool turnBackEnabled;

    public int direction;
    public float moveSpeed;

    public float colDis;

    [HideInInspector] public bool grounded;

    private bool turningBack;

    public float backViewDis;

    public float groundCheckHeight;

    public float turnBackTime;

    public GameObject _fallColStart;
    public GameObject _fallColEnd;

    //public float climbableHeight;
    //public float fallableHeight;

	// Use this for initialization
	void Start () {
        grounded = false;
        turningBack = false;

        turnBackEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<MonsterInfo>().basicMoving)
        {
            if (MoveCheck() && !turningBack)
            {
                transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0);
            }
            else if (!turningBack)
            {
                StartCoroutine(TurnBack(0.0f));
            }
        }

        if (!GetComponent<MonsterInfo>().GetPlayerIsInView() && GetComponent<MonsterInfo>().GetPlayerRecognized() && !turningBack)
        {
            Vector2 backColStart = new Vector2(transform.position.x - (Mathf.Abs(transform.lossyScale.x) / 2 * direction), transform.position.y + transform.lossyScale.y / 2);
            Vector2 backColEnd = new Vector2(backColStart.x - (backViewDis * direction), backColStart.y - transform.lossyScale.y);

            //Instantiate(_fallColStart, backColStart, Quaternion.identity);
            //Instantiate(_fallColEnd, backColEnd, Quaternion.identity);

            if (Physics2D.OverlapArea(backColStart, backColEnd) != null)
            {
                Collider2D[] col;
                col = Physics2D.OverlapAreaAll(backColStart, backColEnd);
                for (int i = 0; i < col.Length; i++)
                {
                    if (col[i].CompareTag("Player"))
                    {
                        StartCoroutine(TurnBack(turnBackTime));
                        break;
                    }
                }
            }

        }
    }

    public bool MoveCheck()
    {
        Vector2 fallColStart = new Vector2(transform.position.x + (Mathf.Abs(transform.lossyScale.x) / 2 * direction - (colDis * direction)), transform.position.y - transform.lossyScale.y / 2);
        Vector2 fallColEnd = new Vector2(fallColStart.x + 0.001f, fallColStart.y - groundCheckHeight);

        //Instantiate(_fallColStart, fallColStart, Quaternion.identity);
        //Instantiate(_fallColEnd, fallColEnd, Quaternion.identity);

        Vector2 wallColStart = new Vector2(fallColStart.x + (0.05f * direction), transform.position.y + (transform.lossyScale.y / 2) - colDis);
        Vector2 wallColSize = new Vector2(0.001f, transform.lossyScale.y);
        
        if (Physics2D.OverlapBox(wallColStart, wallColSize, 0) != null)
        {
            Collider2D[] wallObj;
            wallObj = Physics2D.OverlapBoxAll(wallColStart, wallColSize, 0);
            for (int i = 0; i < wallObj.Length; i++)
            {
                if (wallObj[i].CompareTag("Wall") || wallObj[i].CompareTag("Block"))
                {
                    return false;
                }
            }
        }
        
        //if (Physics2D.OverlapBox(fallColStart, fallColSize, 0) != null)
        if (Physics2D.OverlapArea(fallColStart, fallColEnd) != null)
        {
            Collider2D[] groundObj = Physics2D.OverlapAreaAll(fallColStart, fallColEnd);
            for (int i = 0; i < groundObj.Length; i++)
            {
                if (groundObj[i].CompareTag("Ground"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || GetComponent<MonsterInfo>().landMonster)
        {
            grounded = true;
            GetComponent<MonsterInfo>().basicMoving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || GetComponent<MonsterInfo>().landMonster)
        {
            grounded = false;
        }
    }

    IEnumerator TurnBack(float time)
    {
        if (turnBackEnabled)
        {
            turningBack = true;
            yield return new WaitForSeconds(time);

            turningBack = false;
            direction = -direction;

            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
}
