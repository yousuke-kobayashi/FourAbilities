using System.Collections;
using UnityEngine;

public class MenuKnighController : MonoBehaviour {
    Transform cameraPos;

	void Start () {
        cameraPos = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Update () {
        transform.LookAt(cameraPos);
	}
}
