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
        bp = 10;
    }
}
