using System.Collections;
using UnityEngine;

public class BattleMageController : MonoBehaviour {
    public GameObject iceBombPrefab;
    public GameObject staff;

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

        pos = staff.GetComponent<Transform>();
    }

    void Update() {
        if (battleManager.EndJudge()) {
            //動きを止める
            animator.SetBool("MageRun", false);
            animator.SetBool("MageBack", false);
            return;
        }

        //前進
        if (BattleManager.GoClick) {
            animator.SetBool("MageRun", true);
            animator.SetBool("MageBack", false);
            transform.position += transform.forward * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.GoClick) {
            animator.SetBool("MageRun", false);
        }
        //後退
      
          if (BattleManager.BackClick){
            animator.SetBool("MageBack", true);
            animator.SetBool("MageRun", false);
            transform.position -= transform.forward * 0.7f * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        if (!BattleManager.BackClick) {
            animator.SetBool("MageBack", false);
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
            animator.SetTrigger("MageAttackTrigger");
        }
    }

    public void Hit() {  //AnimationEvent
        //氷の玉を発射する
        GameObject iceBomb = Instantiate(iceBombPrefab) as GameObject;
        iceBomb.transform.position = pos.transform.position;
        iceBomb.transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
    }

    public void FootR() { }  //AnimationEvent
    public void FootL() { }  //AnimationEvent
}
