﻿using System.Collections;
using UnityEngine;

public class BonusArcherController : MonoBehaviour {
    PlayerStatus playerStatus;
    BonusManager bonusManager;
    Vector3 angle;
    Animator animator;

    float moveSpeed = 0.2f;

    void Start() {
        playerStatus = GetComponent<PlayerStatus>();
        bonusManager = GameObject.Find("BonusManager").GetComponent<BonusManager>();
        angle = transform.eulerAngles;
        animator = GetComponent<Animator>();
    }

    void Update() {
        //右矢印を押すと右を向いて進む
        if (bonusManager.RightClick) {
            angle.y = 90.0f;
            transform.eulerAngles = angle;
            animator.SetBool("ArcherRun", true);
            transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 5 / 100);
        }
        //左矢印を押すと左を向いて進む
        else if (bonusManager.LeftClick) {
            angle.y = -90.0f;
            transform.eulerAngles = angle;
            animator.SetBool("ArcherRun", true);
            transform.position += transform.forward * (moveSpeed + playerStatus.SpeedValue * 5 / 100); ;
        }
        else {
            animator.SetBool("ArcherRun", false);
        }
    }

    public void FootL() { }
    public void FootR() { }
}
