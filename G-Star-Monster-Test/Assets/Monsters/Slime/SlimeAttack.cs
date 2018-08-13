using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour {

    public int collisionDamage;

    public float moveSpeed;
    public float moveSpeedForAtk;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 colStart = new Vector2();
        Vector2 colSize = new Vector2();

        GameObject col;
        if (Physics2D.OverlapBox(colStart, colSize, 0) != null)
        {
            col = Physics2D.OverlapBox(colStart, colSize, 0).gameObject;
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerInfo>().hp -= collisionDamage;
            }
        }

        if (GetComponent<MonsterInfo>().GetPlayerRecognized())
        {
            GetComponent<HorizontalMonsterMove>().moveSpeed = moveSpeedForAtk;
        }
        else
        {
            GetComponent<HorizontalMonsterMove>().moveSpeed = moveSpeed;
        }

	}

    
}
