using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAttack : MonoBehaviour {

    private int atkDir;
    
    private bool peaShooting;

    public float peaShootRate;

    public GameObject pea;

	// Use this for initialization
	void Start () {
        peaShooting = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<HorizontalMonsterMove>().grounded)
        {
            if (GetComponent<MonsterInfo>().GetPlayerIsInView() && !peaShooting)
            {
                GetComponent<MonsterInfo>().basicMoving = false;
                StartCoroutine(PeaShoot());
            }
            if (GetComponent<MonsterInfo>().GetPlayerIsInView())
                GetComponent<MonsterInfo>().basicMoving = false;
            else GetComponent<MonsterInfo>().basicMoving = true;
        }

    }

    IEnumerator PeaShoot()
    {
        peaShooting = true;
        yield return new WaitForSeconds(peaShootRate);

        Vector3 tempPos = new Vector3(transform.position.x, transform.position.y, 0);
        
        GameObject tempArrow = Instantiate(pea, tempPos, Quaternion.identity);

        float z = Mathf.Atan2((transform.GetChild(0).GetComponent<MonsterView>().playerPos.y - tempArrow.transform.position.y),
            (transform.GetChild(0).GetComponent<MonsterView>().playerPos.x - tempArrow.transform.position.x)) * Mathf.Rad2Deg - 90;

        tempArrow.transform.eulerAngles = new Vector3(0, 0, z);

        peaShooting = false;
    }
}
