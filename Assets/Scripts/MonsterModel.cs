using System.Collections;
using UnityEngine;

public class MonsterModel {
    public int maxHp, hp, atk, exp, bp;

    public MonsterModel(int maxHp, int atk, int exp, int bp) {
        this.maxHp = maxHp;
        this.hp = maxHp;
        this.atk = atk;
        this.exp = exp;
        this.bp = bp;
    }

    public int HitPoint {
        get { return hp; }
        set { hp = value; }
    }
}
