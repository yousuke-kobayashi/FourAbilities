using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public float speed;

	void Start () {
		
	}
	
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
	}

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
