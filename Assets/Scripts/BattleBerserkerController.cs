using System.Collections;
using UnityEngine;

public class BattleBerserkerController : MonoBehaviour {
    public AudioClip attack;
    public AudioClip skill;
    public GameObject skillEffect;

    PlayerStatus playerStatus;
    BattleManager battleManager;
    Animator animator;
    AudioSource audioSource;
    GameObject attackArea;
    GameObject skillArea;

    public float moveSpeed;  //前進速度
    float angleSpeed = 100.0f;　//回転速度

    void Start() {
        playerStatus = GetComponent<PlayerStatus>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        attackArea = GameObject.Find("AttackArea");
        skillArea = GameObject.Find("SkillArea");

        attackArea.SetActive(false);
        skillArea.SetActive(false);
    }

    void Update() {
        if (battleManager.EndJudge()) {
            //動きを止める
            animator.SetBool("BerserkerRun", false);
            animator.SetBool("BerserkerBack", false);
            return;
        }

        //前進
        if (battleManager.GoClick) {
            animator.SetBool("BerserkerRun", true);
            animator.SetBool("BerserkerBack", false);
            transform.position += transform.forward * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 0.01f);
        }
        if (!battleManager.GoClick) {
            animator.SetBool("BerserkerRun", false);
        }
        //後退
        if (battleManager.BackClick) {
            animator.SetBool("BerserkerBack", true);
            animator.SetBool("BerserkerRun", false);
            transform.position -= transform.forward * 0.7f * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 0.01f);
        }
        if (!battleManager.BackClick) {
            animator.SetBool("BerserkerBack", false);
        }
        //左回転
        if (battleManager.LeftClick) {
            transform.Rotate(Vector3.up * -angleSpeed * Time.deltaTime);
        }
        //右回転
        if (battleManager.RightClick) {
            transform.Rotate(Vector3.up * angleSpeed * Time.deltaTime);
        }
        //攻撃
        if (battleManager.AttackClick && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            animator.SetTrigger("BerserkerAttackTrigger");
        }
        //スキル
        if (battleManager.MP >= 10 &&
            battleManager.SkillClick &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("BerserkerSkillTrigger");
            battleManager.ConsumeMP();
        }
    }

    public void Hit() {  //AnimationEvent
        audioSource.PlayOneShot(attack);
        attackArea.SetActive(true);
    }
    public void HitOut() {  //AnimationEvent
        attackArea.SetActive(false);
    }

    public void SkillHit() {
        audioSource.PlayOneShot(skill);
        skillArea.SetActive(true);
        GameObject effect = Instantiate(skillEffect) as GameObject;
        effect.transform.position = skillArea.transform.position;
        effect.transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
    }
    public void SkillHitOut() {
        skillArea.SetActive(false);
    }

    public void FootR() { }  //AnimationEvent
    public void FootL() { }  //AnimationEvent
}