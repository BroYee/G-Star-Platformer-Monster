using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour {

    private bool falling;
    private float prevHeight;

    public int durability;

    public int fallingDamage;

    private void Start()
    {
        falling = true;
    }


    // Update is called once per frame
    void Update () {

        if (durability <= 0)
        {
            Destroy(gameObject);
        }



        if (transform.position.y == prevHeight)
        {
            falling = false;
        }

        prevHeight = transform.position.y;
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Block collided");
        if (falling)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<PlayerInfo>().hp -= fallingDamage;
            }
            if (col.gameObject.CompareTag("Monster"))
            {
                if (col.gameObject.name != "WildBoar")
                col.gameObject.GetComponent<MonsterInfo>().hp -= fallingDamage;
            }
        }
    }
}
