using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarAttack : MonoBehaviour {

    public GameObject block;

    public List<GameObject> monsters;

    private bool attacking;
    private bool approaching;

    private bool hugeBlockExist;
    private bool normalBlockExist;

    private bool kickBlockEnabled;
    private bool kickingBlock;
    public float kickBlockSpeed;

    public float moveSpeed;

    public int chargeDamage;
    public float chargeSpeed;
    private bool charging;

    public int uppercutDamage;
    private bool readyForUppercut;

    private Vector2 playerPos;
    private float xDis;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {

        attacking = false;
        approaching = false;

        readyForUppercut = false;
        
        kickBlockEnabled = false;
        kickingBlock = false;

        rb = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {

        playerPos = transform.GetChild(0).gameObject.GetComponent<MonsterView>().playerPos;
        xDis = playerPos.x - transform.position.x;

        //Debug.Log("playerXPos : " + playerPos.x.ToString());

        if (xDis < 2.0f && approaching)
        {
            approaching = false;
            readyForUppercut = true;
        }

        if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !attacking && !approaching)
        {
            attacking = true;
            GetComponent<MonsterInfo>().basicMoving = false;

            int hp = GetComponent<MonsterInfo>().hp;
            if (hp > 500)
                EarlyAttack();
            else if (hp > 300)
                MiddleAttack();
            else
                LastAttack();
        }

	}
    
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Block"))
        {
            if (!kickingBlock)
            {
                if (kickBlockEnabled && !charging && !attacking)
                {
                    attacking = true;
                    StartCoroutine(KickBlock(col.transform));

                }
                else
                {
                    Destroy(col.gameObject);
                }
            }
        }
    }

    void EarlyAttack()
    {       

        int rand = Random.Range(0, 10);

        if (rand > 2 || readyForUppercut)
        {
            // Uppercut

            if (Mathf.Abs(xDis) < 2.0f)
            {
                StartCoroutine(Uppercut());
            }
            else
            {
                GetComponent<MonsterInfo>().basicMoving = true;
                attacking = false;
                approaching = true;
            }
        }
        else if (rand < 3)
        {
            // Charges
            StartCoroutine(Charge());

        }


    }

    void MiddleAttack()
    {
        kickBlockEnabled = true;

        int rand = Random.Range(0, 10);

        if (rand < 6 || readyForUppercut)
        {
            // Uppercut

            if (Mathf.Abs(xDis) < 2.0f)
            {
                StartCoroutine(Uppercut());
            }
            else
            {
                GetComponent<MonsterInfo>().basicMoving = true;
                attacking = false;
                approaching = true;
            }
        }
        else if (rand < 9 && !charging)
        {
            // Charges
            StartCoroutine(Charge());
        }
        else
        {
            StartCoroutine(Stamp());
        }



    }

    void LastAttack()
    {

    }


    void DropBlocks()
    {
        int _rand = Random.Range(3, 5);

        for (int i = 0; i < _rand; i++)
        {
            Instantiate(block, new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), Quaternion.identity);
        }
    }

    void DropMonsters()
    {
        int _rand = Random.Range(2, 4);

        for (int i = 0; i < _rand; i++)
        {
            Instantiate(monsters[Random.Range(0, 4)], new Vector3(Random.Range(-4.0f, 4.0f), 6, 0), Quaternion.identity);
        }
    }

    IEnumerator Charge()
    {
        Debug.Log("Charge");

        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        int dir = GetComponent<HorizontalMonsterMove>().direction;
        charging = true;

        yield return new WaitForSeconds(2.0f);

        transform.GetChild(1).gameObject.SetActive(true);

        while (true)
        {
            dir = GetComponent<HorizontalMonsterMove>().direction;

            if (GetComponent<HorizontalMonsterMove>().MoveCheck())
            {
                yield return new WaitForSeconds(0.1f);
                rb.AddForce(new Vector2(dir * chargeSpeed * Time.deltaTime, 0));
            }
            else
            {
                break;
            }

        }

        transform.GetChild(1).gameObject.SetActive(false);
        charging = false;

        DropBlocks();

        yield return new WaitForSeconds(3.0f);

        GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        GetComponent<MonsterInfo>().basicMoving = true;
        attacking = false;
    }

    IEnumerator Uppercut()
    {
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        yield return new WaitForSeconds(1);
        
        transform.GetChild(2).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        transform.GetChild(2).gameObject.SetActive(false);
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        GetComponent<MonsterInfo>().basicMoving = true;
        attacking = false;
        readyForUppercut = false;

    }

    IEnumerator Stamp()
    {
        attacking = true;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<MonsterInfo>().basicMoving = false;

        yield return new WaitForSeconds(1.0f);

        DropMonsters();

        yield return new WaitForSeconds(1.0f);

        attacking = false;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        GetComponent<MonsterInfo>().basicMoving = true;

    }

    IEnumerator KickBlock(Transform blockTrans)
    {
        attacking = true;
        kickingBlock = true;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = false;
        GetComponent<MonsterInfo>().basicMoving = false;

        yield return new WaitForSeconds(1.0f);

        blockTrans.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<HorizontalMonsterMove>().direction * kickBlockSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        blockTrans.GetComponent<BlockInfo>().durability -= 40;

        yield return new WaitForSeconds(0.5f);

        attacking = false;
        kickingBlock = false;
        GetComponent<HorizontalMonsterMove>().turnBackEnabled = true;
        GetComponent<MonsterInfo>().basicMoving = true;
    }

    IEnumerator PushBlock(Transform blockTrans)
    {

        yield return new WaitForSeconds(0.1f);

    }

    
}
