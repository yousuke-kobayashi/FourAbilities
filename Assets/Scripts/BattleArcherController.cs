using System.Collections;
using UnityEngine;

public class BattleArcherController : MonoBehaviour {
    public GameObject arrowPrefab;
    public GameObject bow;
    public GameObject windArrowPrefab;

    PlayerStatus playerStatus;
    BattleManager battleManager;
    Animator animator;
    Transform pos;

    float moveSpeed = 0.9f;  //前進速度
    float angleSpeed = 100.0f;　//回転速度

    void Start() {
        playerStatus = GetComponent<PlayerStatus>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        animator = GetComponent<Animator>();

        pos = bow.GetComponent<Transform>();
    }

    void Update() {
        if (battleManager.EndJudge()) {
            //動きを止める
            animator.SetBool("ArcherRun", false);
            animator.SetBool("ArcherBack", false);
            return;
        }

        //前進
        if (BattleManager.GoClick) {
            animator.SetBool("ArcherRun", true);
            animator.SetBool("ArcherBack", false);
            transform.position += transform.forward * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.GoClick) {
            animator.SetBool("ArcherRun", false);
        }
        //後退
        if (BattleManager.BackClick) {
            animator.SetBool("ArcherBack", true);
            animator.SetBool("ArcherRun", false);
            transform.position -= transform.forward * 0.7f * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.BackClick) {
            animator.SetBool("ArcherBack", false);
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
            animator.SetTrigger("ArcherAttackTrigger");
        }
        //スキル
        if (BattleManager.SkillClick &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("ArcherSkillTrigger");
        }
    }

    public void Hit() {  //AnimationEvent
        //弓矢を発射する
        GameObject arrow = Instantiate(arrowPrefab) as GameObject;
        arrow.transform.position = new Vector3(pos.transform.position.x, 1.5f, pos.transform.position.z);
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
    }

    public void SkillHit() {
        GameObject windArrow = Instantiate(windArrowPrefab) as GameObject;
        windArrow.transform.position = new Vector3(transform.position.x, 1, transform.position.z + 10);
    }

    public void FootR() { }  //AnimationEvent
    public void FootL() { }  //AnimationEvent
}
