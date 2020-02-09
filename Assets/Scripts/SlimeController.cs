﻿using System.Collections;
using UnityEngine;

public class SlimeController : MonoBehaviour {
    public GameObject damageEffect;
    public AudioClip attack;
    public AudioClip slash;
    public AudioClip bang;
    public AudioClip sting;

    BattleManager battleManager;
    MonsterStatus monsterStatus;
    Animator animator;
    AudioSource audioSource;

    float moveSpeed = 5.0f;
    float attackArea = 4.0f;
    float count;

    void Start () {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        monsterStatus = GetComponent<MonsterStatus>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
        if (battleManager.EndJudge()) return;

        //HP0の処理
        if (monsterStatus.HelthZero()) {
            animator.SetTrigger("SlimeDieTrigger");
            return;
        }

        //0.5秒後に吹き飛ばし解除
        if (moveSpeed < 0) {
            count += Time.deltaTime;

            if (count >= 0.5f) {
                moveSpeed = 5.0f;
            }
        }

        //プレイヤーに向かって移動
        if (moveSpeed > 0) {
            animator.SetBool("SlimeRun", true);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
        {
            //ダメージ中、スタン中は止まる
            moveSpeed = 0;
        }
        else if (monsterStatus.Distance <= attackArea) {
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
        if (!monsterStatus.HelthZero()) {
            if (other.gameObject.tag == "KnightAttackArea") {
                SlimeDamageProcess();
                audioSource.PlayOneShot(slash);
                monsterStatus.KnightAttack();
            } else if (other.gameObject.tag == "KnightSkillArea") {
                SlimeDamageProcess();
                audioSource.PlayOneShot(slash);
                monsterStatus.KnightSkill();
            }
            else if (other.gameObject.tag == "BerserkerAttackArea") {
                SlimeDamageProcess();
                audioSource.PlayOneShot(slash);
                monsterStatus.BerserkerAttack();
            } else if (other.gameObject.tag == "BerserkerSkillArea") {
                animator.SetTrigger("SlimeStunTrigger");
                Instantiate(damageEffect, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(bang);
                monsterStatus.BerserkerSkill();
            }
            else if (other.gameObject.tag == "MageAttackArea") {
                SlimeDamageProcess();
                monsterStatus.MageAttack();
            } else if (other.gameObject.tag == "MageSkillArea") {
                SlimeDamageProcess();
                monsterStatus.MageSkill();
            }
            else if (other.gameObject.tag == "ArcherAttackArea") {
                SlimeDamageProcess();
                audioSource.PlayOneShot(sting);
                monsterStatus.ArcherAttack();
            } else if (other.gameObject.tag == "ArcherSkillArea") {
                moveSpeed = -150;
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                SlimeDamageProcess();
                audioSource.PlayOneShot(slash);
                monsterStatus.ArcherSkill();
            }
        }
    }

    //ダメージを受けた時の処理
    public void SlimeDamageProcess() {
        animator.SetTrigger("SlimeDamageTrigger");
        Instantiate(damageEffect, transform.position, Quaternion.identity);
    }

    public void Hit() {  //AnimationEvent
        audioSource.PlayOneShot(attack);
        monsterStatus.PlayerHelthDecrease();
    }

    public void Disappear() {  //AnimationEvent
        monsterStatus.Defeat();
        Destroy(this.gameObject);
    }
}
