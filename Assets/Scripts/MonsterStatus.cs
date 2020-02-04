using System.Collections;
using UnityEngine;

public class MonsterStatus : MonoBehaviour {
    MonsterModel monsterModel;
    PlayerStatus playerStatus;
    GameObject[] player;
    Transform target;
    
    void Start () {
        player = GameObject.FindGameObjectsWithTag("Player");
        playerStatus = player[0].GetComponent<PlayerStatus>();
        target = player[0].GetComponent<Transform>();
        Distance = Vector3.Distance(target.position, transform.position);
    }

    void Update () {
        //プレイヤーの方を向く
        transform.LookAt(target);

        Distance = Vector3.Distance(target.position, transform.position);
    }
    
    public void MonsterInstantiate() {
        monsterModel = new MonsterModel(20, 5, 1, 1);
    }

    public void BossInstantiate(int hp, int atk, int exp,int bp) {
        monsterModel = new MonsterModel(hp, atk, exp, bp);
    }

    public void MonsterHelthDecrease() {
        monsterModel.hp -= playerStatus.AttackValue;
    }

    public void PlayerHelthDecrease() {
        playerStatus.HelthPoint -= Mathf.Clamp(monsterModel.atk - playerStatus.DeffenceValue * 2 / 10, 1, 999);
    }

    //撃破時、経験値とBPを加算
    public void Defeat() {
        playerStatus.ExperiencePoint += monsterModel.exp;
        playerStatus.BonusPoint += monsterModel.bp;
    }

    public bool HelthZero() {
        if (monsterModel.hp <= 0) {
            return true;
        } else {
            return false;
        }
    }

    public int HP {
        get { return monsterModel.hp; }
    }

    public int AttackValue {
        get { return monsterModel.atk; }
    }

    public bool BossDead { get; private set; }

    public float Distance { get; private set; }
}
