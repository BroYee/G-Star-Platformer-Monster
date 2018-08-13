using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFollower : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        transform.position = transform.parent.position;

	}
}
