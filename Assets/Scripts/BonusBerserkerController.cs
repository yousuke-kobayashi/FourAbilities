using System.Collections;
using UnityEngine;

public class BonusBerserkerController : MonoBehaviour {
    public AudioClip skillSound;
    public GameObject effect;
    public GameObject skillPos;

    PlayerStatus playerStatus;
    BonusManager bonusManager;
    Vector3 angle;
    Animator animator;
    AudioSource audioSource;

    float moveSpeed = 0.15f;

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
                animator.SetBool("BerserkerRun", true);
                transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 0.002f);
            }
            //左矢印を押すと左を向いて進む
            else if (bonusManager.LeftClick) {
                angle.y = -90.0f;
                transform.eulerAngles = angle;
                animator.SetBool("BerserkerRun", true);
                transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 0.002f); ;
            }
            else {
                animator.SetBool("BerserkerRun", false);
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

    public void Hit()
    {
        audioSource.PlayOneShot(skillSound);
        GameObject skill = Instantiate(effect) as GameObject;
        skill.transform.position = skillPos.transform.position;
        skill.transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
    }

    public void HitOut() { }

    public void FootL() { }
    public void FootR() { }
}
