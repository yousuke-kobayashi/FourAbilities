using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        transform.position += transform.forward * Time.deltaTime * 25;
	}

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
