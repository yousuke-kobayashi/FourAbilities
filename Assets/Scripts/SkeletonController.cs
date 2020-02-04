using System.Collections;
using UnityEngine;

public class SkeletonController : MonoBehaviour {
    BattleManager battleManager;
    MonsterStatus monsterStatus;
    Animator animator;

    float moveSpeed = 3.0f;
    float attackArea = 8.0f;

    void Start() {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        monsterStatus = GetComponent<MonsterStatus>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (battleManager.EndJudge()) return;

        //HP0の処理
        if (monsterStatus.HelthZero()) {
            animator.SetTrigger("SkeletonDieTrigger");
            MenuManager.FirstBossAlive = false;
            return;
        }

        //プレイヤーに向かって移動
        if (moveSpeed > 0) {
            animator.SetBool("SkeletonWalk", true);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (monsterStatus.Distance <= attackArea) {
            //射程距離に到達すると、止まって攻撃する
            moveSpeed = 0;
            animator.SetBool("SkeletonWalk", false);
            animator.SetTrigger("SkeletonAttackTrigger");
        } else {
            animator.SetBool("SkeletonWalk", true);
            moveSpeed = 3.0f;
        }
    }

    public void OnTriggerEnter(Collider other) {
        //攻撃範囲内で攻撃されるとダメージを受ける
        if (other.gameObject.tag == "AttackArea" && !monsterStatus.HelthZero()) {
            monsterStatus.MonsterHelthDecrease();
        }
    }

    public void Hit() {  //AnimationEvent
        monsterStatus.PlayerHelthDecrease();
    }

    public void Disappear() {  //AnimationEvent
        monsterStatus.Defeat();
        Destroy(this.gameObject);
    }
}
