using System.Collections;
using UnityEngine;

public class BonusKnightController : MonoBehaviour {
    Vector3 angle;
    Animator animator;

    float moveSpeed = 10.0f;

	void Start () {
        angle = transform.eulerAngles;
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        float z = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //右矢印を押すと右を向いて進む
        if (z > 0) {
            angle.y = 90.0f;
            transform.eulerAngles = angle;
            animator.SetBool("RunBool", true);
            transform.position += transform.forward * z;
        }
        //左矢印を押すと左を向いて進む
        else if (z < 0) {
            angle.y = -90.0f;
            transform.eulerAngles = angle;
            animator.SetBool("RunBool", true);
            transform.position -= transform.forward * z;
        }
        else {
            animator.SetBool("RunBool", false);
        }
    }

    public void FootL() { }
    public void FootR() { }
}
