using System.Collections;
using UnityEngine;

public class BattleMageController : MonoBehaviour {
    public GameObject iceBombPrefab;
    public GameObject staff;
    public GameObject flameBurstPrefab;
    public GameObject skillPos;
    public AudioClip skill;

    PlayerStatus playerStatus;
    BattleManager battleManager;
    Animator animator;
    AudioSource audioSource;
    Transform pos;

    public float moveSpeed;  //前進速度
    float angleSpeed = 100.0f;　//回転速度

    void Start() {
        playerStatus = GetComponent<PlayerStatus>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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
        if (battleManager.GoClick) {
            animator.SetBool("MageRun", true);
            animator.SetBool("MageBack", false);
            transform.position += transform.forward * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 0.02f);
        }
        if (!battleManager.GoClick) {
            animator.SetBool("MageRun", false);
        }
        //後退
      
          if (battleManager.BackClick){
            animator.SetBool("MageBack", true);
            animator.SetBool("MageRun", false);
            transform.position -= transform.forward * 0.7f * Time.deltaTime * (moveSpeed + playerStatus.SpeedValue * 0.02f);
        }
        if (!battleManager.BackClick) {
            animator.SetBool("MageBack", false);
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
            animator.SetTrigger("MageAttackTrigger");
        }
        //スキル
        if (battleManager.MP >= 10 &&
            battleManager.SkillClick &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("MageSkillTrigger");
            audioSource.PlayOneShot(skill);
            battleManager.ConsumeMP();
        }
    }

    public void Hit() {  //AnimationEvent
        //氷の玉を発射する
        GameObject iceBomb = Instantiate(iceBombPrefab) as GameObject;
        iceBomb.transform.position = pos.transform.position;
        iceBomb.transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
    }

    public void SkillHit() {
        GameObject flameBurst = Instantiate(flameBurstPrefab) as GameObject;
        flameBurst.transform.position = skillPos.transform.position;
    }

    public void FootR() { }  //AnimationEvent
    public void FootL() { }  //AnimationEvent
}
