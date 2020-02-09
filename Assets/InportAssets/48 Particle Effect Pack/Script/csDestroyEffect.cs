using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour {

    void Start() {
        StartCoroutine("Destroy");
    }

    void Update () {
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
