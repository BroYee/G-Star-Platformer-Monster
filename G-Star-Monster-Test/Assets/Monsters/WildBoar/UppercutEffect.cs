using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutEffect : MonoBehaviour {

    private int damage;

	// Use this for initialization
	void Start () {
        damage = transform.parent.GetComponent<WildBoarAttack>().uppercutDamage;
	}	

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerInfo>().hp -= damage;
        }
        if (col.CompareTag("Block"))
        {
            Destroy(col.gameObject);
        }
    }

}
