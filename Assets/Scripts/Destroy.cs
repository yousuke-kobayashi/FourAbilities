using System.Collections;
using UnityEngine;

public class Destroy : MonoBehaviour {
    public float time;

	void Start () {
        StartCoroutine("Limit");
	}
	
	void Update () {
		
	}

    IEnumerator Limit() {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
