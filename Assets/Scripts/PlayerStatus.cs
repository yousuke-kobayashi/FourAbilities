using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    PlayerModel playerModel;

    void Awake () {
        playerModel = PlayerModel.GetInstance();  //インスタンス化
        //次のレベルまでに必要な経験値＝レベル＊10＋レベル2乗
        NeedEXP = (playerModel.lv * 10 + (playerModel.lv * playerModel.lv)) / 2;
    }

    void Update()　{
        if (playerModel.exp >= NeedEXP)　{
            playerModel.exp -= NeedEXP;  //経験値を繰り越す
            LevelUp();

            //次のレベルまでに必要な経験値＝レベル＊10＋レベル2乗
            NeedEXP = (playerModel.lv * 10 + (playerModel.lv * playerModel.lv)) / 2;
        }
    }

    public bool StatusOver(int under) {
        if (playerModel.atk >= under &&
            playerModel.def >= under &&
            playerModel.mag >= under &&
            playerModel.sp >= under)
        {
            return true;
        }
        else return false;
    }

    public void LevelUp() {
        playerModel.lv++;
        playerModel.maxHp += 10;
        playerModel.atk　+= (playerModel.lv / 10) + 1;
        playerModel.def += (playerModel.lv / 10) + 1;
        playerModel.mag += (playerModel.lv / 10) + 1;
        playerModel.sp += (playerModel.lv / 10) + 1;
    }

    public void HelthReset() {
        playerModel.hp = playerModel.maxHp;
    }

    public int Level {
        get { return playerModel.lv; }
    }

    public int MaxHelth {
        get { return playerModel.maxHp; }
    }

    public int HelthPoint {
        get { return playerModel.hp; }
        set { playerModel.hp = value; }
    }

    public int AttackValue {
        get { return playerModel.atk; }
        set { playerModel.atk = value; }
    }

    public int DeffenceValue {
        get { return playerModel.def; }
        set { playerModel.def = value; }
    }

    public int MagicValue {
        get { return playerModel.mag; }
        set { playerModel.mag = value; }
    }

    public int SpeedValue {
        get { return playerModel.sp; }
        set { playerModel.sp = value; }
    }

    public int ExperiencePoint {
        get { return playerModel.exp; }
        set { playerModel.exp = value; }
    }

    public int BonusPoint {
        get { return playerModel.bp; }
        set { playerModel.bp = value; }
    }

    public int NeedEXP { get; private set; }  

}
