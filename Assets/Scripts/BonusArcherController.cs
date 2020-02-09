using System.Collections;
using UnityEngine;

public class BonusArcherController : MonoBehaviour {
    public GameObject skillPrefab;

    PlayerStatus playerStatus;
    BonusManager bonusManager;
    Vector3 angle;
    Animator animator;
    AudioSource audioSource;

    float moveSpeed = 0.2f;

    void Start() {
        playerStatus = GetComponent<PlayerStatus>();
        bonusManager = GameObject.Find("BonusManager").GetComponent<BonusManager>();
        angle = transform.eulerAngles;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BonusSkill")) {
            //右矢印を押すと右を向いて進む
            if (bonusManager.RightClick) {
                angle.y = 90.0f;
                transform.eulerAngles = angle;
                animator.SetBool("ArcherRun", true);
                transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 0.003f);
            }
            //左矢印を押すと左を向いて進む
            else if (bonusManager.LeftClick) {
                angle.y = -90.0f;
                transform.eulerAngles = angle;
                animator.SetBool("ArcherRun", true);
                transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 0.003f); ;
            }
            else {
                animator.SetBool("ArcherRun", false);
            }
        }

        //スキル発動
        if (bonusManager.SkillClick &&
            bonusManager.MP == 10 &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("BonusSkill"))
        {
            animator.SetTrigger("BonusSkillTrigger");
            bonusManager.ConsumeMP();
        }
    }

    public void Hit() {
        audioSource.Play();
        Physics.gravity = new Vector3(0, -4, 0);
        GameObject wind = Instantiate(skillPrefab) as GameObject;
        StartCoroutine("ResetGravity");
    }

    IEnumerator ResetGravity() {
        yield return new WaitForSeconds(3);
        audioSource.Stop();
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    public void FootL() { }
    public void FootR() { }
}
