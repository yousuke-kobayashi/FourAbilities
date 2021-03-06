﻿using System.Collections;
using UnityEngine;

public class IceBombController : MonoBehaviour {
    public  GameObject iceExplosionPrefab;

	void Start () {
    }
	
	void Update () {
        transform.position += transform.forward * Time.deltaTime * 15;
	}

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Monster") {
            GameObject iceExplosion = Instantiate(iceExplosionPrefab) as GameObject;
            iceExplosion.transform.position = transform.position;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
