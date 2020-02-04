using System.Collections;
using UnityEngine;

public class PlayerModel {
    static PlayerModel Instance = null;

    public static PlayerModel GetInstance() {
        if (Instance == null) {
            Instance = new PlayerModel();
        }
        return Instance;
    }

    public int lv, exp, maxHp, hp, atk, def, mag, sp, bp;

    public PlayerModel() {
        lv = 1;
        exp = 0;
        maxHp = 100;
        hp = maxHp;
        atk = 30;
        def = 30;
        mag = 30;
        sp = 30;
        bp = 0;
    }

    /*public PlayerModel(int lv, int exp, int maxHp, int atk, int def, int mag, int sp, int bp) {
        this.lv = lv;
        this.exp = exp;
        this.maxHp = maxHp;
        this.hp = maxHp;
        this.atk = atk;
        this.def = def;
        this.mag = mag;
        this.sp = sp;
        this.bp = bp;
    }*/
}
