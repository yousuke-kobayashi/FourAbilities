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
    
    public void SlimeInstantiate() {
        monsterModel = new MonsterModel(20, 5, 1, 2);
    }

    public void TurtleInstantiate() {
        monsterModel = new MonsterModel(100, 15, 3, 5);
    }

    public void BossInstantiate(int hp, int atk, int exp,int bp) {
        monsterModel = new MonsterModel(hp, atk, exp, bp);
    }
    
    public void PlayerHelthDecrease() {
        playerStatus.HelthPoint -= Mathf.Clamp(monsterModel.atk - playerStatus.DeffenceValue / 5, 1, 999);
    }

    //ナイトの攻撃＝攻撃力
    //ナイトのスキル＝攻撃力＋魔力＊0.2
    public void KnightAttack() {
        monsterModel.hp -= playerStatus.AttackValue;
    }
    public void KnightSkill() {
        monsterModel.hp -= (playerStatus.AttackValue + playerStatus.MagicValue / 5);
    }
    //バーサーカーの攻撃＝攻撃力＊1.5
    //バーサーカーのスキル＝攻撃力＋防御力/2＋スタン
    public void BerserkerAttack() {
        monsterModel.hp -= playerStatus.AttackValue * 3 / 2;
    }
    public void BerserkerSkill() {
        monsterModel.hp -= playerStatus.AttackValue + playerStatus.DeffenceValue / 2;
    }
    //メイジの攻撃＝魔力＋攻撃力＊0.2
    //メイジのスキル＝魔力＊2
    public void MageAttack() {
        monsterModel.hp -= playerStatus.MagicValue + playerStatus.AttackValue / 5;
    }
    public void MageSkill() {
        monsterModel.hp -= playerStatus.MagicValue * 2;
    }
    //アーチャーの攻撃＝攻撃力/2＋素早さ/2
    //アーチャーのスキル＝素早さ＋魔力0.2＋吹き飛ばし
    public void ArcherAttack() {
        monsterModel.hp -= playerStatus.AttackValue / 2 + playerStatus.SpeedValue / 2;
    }
    public void ArcherSkill() {
        monsterModel.hp -= playerStatus.SpeedValue + playerStatus.MagicValue / 5;
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
