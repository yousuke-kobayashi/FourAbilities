using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosionController : MonoBehaviour {

	void Start () {
        StartCoroutine("Explosion");
	}
	
	void Update () {
		
	}

    IEnumerator Explosion() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
