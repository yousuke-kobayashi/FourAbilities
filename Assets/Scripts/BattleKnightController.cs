using System.Collections;
using UnityEngine;

public class BattleKnightController : MonoBehaviour {
    PlayerStatus playerStatus;
    Animator animator;
    GameObject attackArea;
    BattleManager battleManager;

    float z;
    float moveSpeed = 5.0f;  //前進速度
    float angleSpeed = 100.0f;　//回転速度

	void Start () {
        playerStatus = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        attackArea = GameObject.Find("AttackArea");
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        attackArea.SetActive(false);
    }
	
	void Update () {
        if (battleManager.EndJudge()) {
            //動きを止める
            z = 0;
            animator.SetBool("RunBool", false);
            animator.SetBool("BackBool", false);
            return;
        }

        //上下キーの入力値を読み取る
        z = Input.GetAxis("Vertical") * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);

        //前進と後退と停止
        if (z > 0) {
            animator.SetBool("RunBool", true);
            animator.SetBool("BackBool", false);
            transform.position += transform.forward * z;
        } else if (z < 0) {
            animator.SetBool("BackBool", true);
            animator.SetBool("RunBool", false);
            transform.position += transform.forward * z * 0.3f;
        } else if (z == 0) {
            animator.SetBool("RunBool", false);
            animator.SetBool("BackBool", false);
        }

        //回転
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * angleSpeed;
        transform.Rotate(Vector3.up * x);

        //攻撃
        if (Input.GetKeyDown(KeyCode.Space) && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("AttackTrigger");
        }
    }

    public void Hit() {  //AnimationEvent
        attackArea.SetActive(true);
    }

    public void HitOut() {  //AnimationEvent
        attackArea.SetActive(false);
    }


    public void FootR() { }  //AnimationEvent
    public void FootL() { }  //AnimationEvent
}
