using System.Collections;
using UnityEngine;

public class BattleKnightController : MonoBehaviour {
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

    void Start () {
        playerStatus = GetComponent<PlayerStatus>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        if (battleManager.GoClick) {
            animator.SetBool("KnightRun", true);
            animator.SetBool("KnightBack", false);
            transform.position += transform.forward * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 0.02f);
        }
        if (!battleManager.GoClick) {
            animator.SetBool("KnightRun", false);
        }
        //後退
        if (battleManager.BackClick) {
            animator.SetBool("KnightBack", true);
            animator.SetBool("KnightRun", false);
            transform.position -= transform.forward * 0.7f * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 0.02f);
        }
        if (!battleManager.BackClick) {
            animator.SetBool("KnightBack", false);
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
        if (battleManager.AttackClick &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("KnightAttackTrigger");
        }
        //スキル
        if (battleManager.MP >= 10 &&
            battleManager.SkillClick &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("KnightSkillTrigger");
            battleManager.ConsumeMP();
        }
    }

    public void Sound() {
        audioSource.PlayOneShot(attack);
    }

    public void Hit() {  //AnimationEvent
        attackArea.SetActive(true);
    }
    public void HitOut() {  //AnimationEvent
        attackArea.SetActive(false);
    }

    public void SkillHit() {
        audioSource.PlayOneShot(skill);
        skillArea.SetActive(true);
        GameObject effect = Instantiate(skillEffect) as GameObject;
        effect.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
    }
    public void SkillHitOut() {
        skillArea.SetActive(false);
    }

    public void FootR() { }  //AnimationEvent
    public void FootL() { }  //AnimationEvent
}
