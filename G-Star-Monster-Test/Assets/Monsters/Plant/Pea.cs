using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour {

    public int damage;

    public float speed;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerInfo>().hp -= damage;
            Destroy(gameObject);
        }
    }
}
