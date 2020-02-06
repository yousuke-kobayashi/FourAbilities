using System.Collections;
using UnityEngine;

public class BattleBerserkerController : MonoBehaviour {
    PlayerStatus playerStatus;
    Animator animator;
    GameObject attackArea;
    BattleManager battleManager;

    float moveSpeed = 1.0f;  //前進速度
    float angleSpeed = 100.0f;　//回転速度

    void Start() {
        playerStatus = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        attackArea = GameObject.Find("AttackArea");
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        attackArea.SetActive(false);
    }

    void Update() {
        if (battleManager.EndJudge()) {
            //動きを止める
            animator.SetBool("BerserkerRun", false);
            animator.SetBool("BerserkerBack", false);
            return;
        }

        //前進
        if (BattleManager.GoClick) {
            animator.SetBool("BerserkerRun", true);
            animator.SetBool("BerserkerBack", false);
            transform.position += transform.forward * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.GoClick) {
            animator.SetBool("BerserkerRun", false);
        }
        //後退
        if (BattleManager.BackClick) {
            animator.SetBool("BerserkerBack", true);
            animator.SetBool("BerserkerRun", false);
            transform.position -= transform.forward * 0.7f * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.BackClick) {
            animator.SetBool("BerserkerBack", false);
        }
        //左回転
        if (BattleManager.LeftClick) {
            transform.Rotate(Vector3.up * -angleSpeed * Time.deltaTime);
        }
        //右回転
        if (BattleManager.RightClick) {
            transform.Rotate(Vector3.up * angleSpeed * Time.deltaTime);
        }
        //攻撃
        if (BattleManager.AttackClick && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            animator.SetTrigger("BerserkerAttackTrigger");
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