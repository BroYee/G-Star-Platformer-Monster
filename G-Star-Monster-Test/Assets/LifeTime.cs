using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour {

    public bool destroyOrDisable;
    public float lifeTime;

	// Use this for initialization
	void Start () {
        StartCoroutine(Live());
	}

    IEnumerator Live()
    {
        yield return new WaitForSeconds(lifeTime);
        {
            if (destroyOrDisable) Destroy(gameObject);
            else gameObject.SetActive(false);
        }
    }
}
