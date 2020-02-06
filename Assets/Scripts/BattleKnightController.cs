using System.Collections;
using UnityEngine;

public class BattleKnightController : MonoBehaviour {
    PlayerStatus playerStatus;
    BattleManager battleManager;
    Animator animator;
    GameObject attackArea;
    GameObject skillArea;


    float moveSpeed = 1.0f;  //前進速度
    float angleSpeed = 100.0f;　//回転速度

    void Start () {
        playerStatus = GetComponent<PlayerStatus>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        animator = GetComponent<Animator>();
        attackArea = GameObject.Find("AttackArea");
        skillArea = GameObject.Find("SkillArea");

        attackArea.SetActive(false);
        skillArea.SetActive(false);
    }
	
	void Update () {
        if (battleManager.EndJudge()) {
            //動きを止める
            animator.SetBool("KnightRun", false);
            animator.SetBool("KnightBack", false);
            return;
        }
        
        //前進
        if (BattleManager.GoClick) {
            animator.SetBool("KnightRun", true);
            animator.SetBool("KnightBack", false);
            transform.position += transform.forward * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.GoClick) {
            animator.SetBool("KnightRun", false);
        }
        //後退
        if (BattleManager.BackClick) {
            animator.SetBool("KnightBack", true);
            animator.SetBool("KnightRun", false);
            transform.position -= transform.forward * 0.7f * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.BackClick) {
            animator.SetBool("KnightBack", false);
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
        if (BattleManager.AttackClick &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("KnightAttackTrigger");
        }
        //スキル
        if (BattleManager.SkillClick &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("KnightSkillTrigger");
        }
    }

    public void Hit() {  //AnimationEvent
        attackArea.SetActive(true);
    }
    public void HitOut() {  //AnimationEvent
        attackArea.SetActive(false);
    }

    public void SkillHit() {
        skillArea.SetActive(true);
    }
    public void SkillHitOut() {
        skillArea.SetActive(false);
    }

    public void FootR() { }  //AnimationEvent
    public void FootL() { }  //AnimationEvent
}
