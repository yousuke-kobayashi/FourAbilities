using System.Collections;
using UnityEngine;

public class BonusKnightController : MonoBehaviour {
    public GameObject effect;
    public AudioClip skillSound;

    PlayerStatus playerStatus;
    BonusManager bonusManager;
    Vector3 angle;
    Animator animator;
    AudioSource audioSource;

    float moveSpeed = 0.2f;

	void Start () {
        playerStatus = GetComponent<PlayerStatus>();
        bonusManager = GameObject.Find("BonusManager").GetComponent<BonusManager>();
        angle = transform.eulerAngles;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BonusSkill")) {
            //右矢印を押すと右を向いて進む
            if (bonusManager.RightClick) {
                angle.y = 90.0f;
                transform.eulerAngles = angle;
                animator.SetBool("KnightRun", true);
                transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 0.002f);
            }
            //左矢印を押すと左を向いて進む
            else if (bonusManager.LeftClick) {
                angle.y = -90.0f;
                transform.eulerAngles = angle;
                animator.SetBool("KnightRun", true);
                transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 0.002f); ;
            }
            else {
                animator.SetBool("KnightRun", false);
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
        GameObject skill = Instantiate(effect) as GameObject;
        skill.transform.position = transform.position;
        //skill.transform.rotation = Quaternion.Euler(new Vector3(0, transform.localEulerAngles.y, 0));
    }

    public void Sound() {
        audioSource.PlayOneShot(skillSound);
    }

    public void HitOut() { }
    public void FootL() { }
    public void FootR() { }
}
