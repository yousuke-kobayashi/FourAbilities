using System.Collections;
using UnityEngine;

public class FlameBurstController : MonoBehaviour {

	void Start () {
        StartCoroutine("Explosion");
	}
	
	void Update () {
		
	}

    IEnumerator Explosion() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
