using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour {
    
    public int hp;
    [HideInInspector] public bool basicMoving;
    private bool playerIsInView;
    private bool playerRecognized;

    public float loseRecogOfPlayerTime;

    public bool landMonster;

    // Use this for initialization
    void Start () {

        if (playerIsInView)
            playerRecognized = true;

        basicMoving = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (hp <= 0)
        {
            hp = 0;
            gameObject.SetActive(false);
        }
	}

    public bool GetPlayerIsInView()
    {
        return playerIsInView;
    }

    public bool GetPlayerRecognized()
    {
        return playerRecognized;
    }

    public void SeePlayer()
    {
        playerIsInView = true;
        playerRecognized = true;
    }

    public void MissPlayer()
    {
        playerIsInView = false;
        StartCoroutine(LoseRecogOfPlayer());
    }

    IEnumerator LoseRecogOfPlayer()
    {
        yield return new WaitForSeconds(loseRecogOfPlayerTime);

        playerRecognized = false;
    }
}
