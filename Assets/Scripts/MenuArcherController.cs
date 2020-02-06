using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArcherController : MonoBehaviour {
    Transform cameraPos;

    void Start () {
        cameraPos = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
	
	void Update () {
        transform.LookAt(cameraPos);
    }
}
