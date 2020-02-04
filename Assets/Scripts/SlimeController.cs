using System.Collections;
using UnityEngine;

public class SlimeController : MonoBehaviour {
    BattleManager battleManager;
    MonsterStatus monsterStatus;
    Animator animator;

    float moveSpeed = 5.0f;
    float attackArea = 4.0f;

    void Start () {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        monsterStatus = GetComponent<MonsterStatus>();
        animator = GetComponent<Animator>();
    }
	
	void Update () {
        if (battleManager.EndJudge()) return;

        //HP0の処理
        if (monsterStatus.HelthZero()) {
            animator.SetTrigger("SlimeDieTrigger");
            return;
        }

        //プレイヤーに向かって移動
        if (moveSpeed > 0) {
            animator.SetBool("SlimeRun", true);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (monsterStatus.Distance <= attackArea) {
            //射程距離に到達すると、止まって攻撃する
            moveSpeed = 0;
            animator.SetBool("SlimeRun", false);
            animator.SetTrigger("SlimeAttackTrigger");
        } else {
            //離れると再び追いかける
            moveSpeed = 5.0f;
        }
    }

    public void OnTriggerEnter(Collider other) {
        //攻撃範囲内で攻撃されるとダメージを受ける
        if (other.gameObject.tag == "AttackArea" && !monsterStatus.HelthZero()) {
            animator.SetTrigger("SlimeDamageTrigger");
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
