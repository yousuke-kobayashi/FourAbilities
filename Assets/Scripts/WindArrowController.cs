using System.Collections;
using UnityEngine;

public class WindArrowController : MonoBehaviour {

	void Start () {
        StartCoroutine("WindBurst");
	}
	
	void Update () {
		
	}

    IEnumerator WindBurst() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
