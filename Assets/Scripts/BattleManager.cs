using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    public GameObject[] playerPrefab;
    public Text statusText;
    public Text bpText;
    public Text timeText;
    public Text finishText;
    public float limitTime;  //制限時間

    PlayerStatus playerStatus;
    GameObject player;

    float time;  //残り時間
    bool end = false;
    bool boss = false;

    void Awake() {
        if (MenuManager.Num() == 0) {
            player = Instantiate(playerPrefab[0]) as GameObject;
        } else if (MenuManager.Num() == 1) {
            player = Instantiate(playerPrefab[1]) as GameObject;
        } else if (MenuManager.Num() == 2) {
            player = Instantiate(playerPrefab[2]) as GameObject;
        } else if (MenuManager.Num() == 3) {
            player = Instantiate(playerPrefab[3]) as GameObject;
        }
    }

    void Start() {
        playerStatus = player.GetComponent<PlayerStatus>();

        if (MenuManager.FirstBossAlive) {
            boss = true;
            return;
        }

        timeText.text = limitTime.ToString("F2");
        time = limitTime;
    }

    void Update() {
        statusText.text =
            "LV" + playerStatus.Level +
            " HP" + Mathf.Clamp(playerStatus.HelthPoint, 0, playerStatus.MaxHelth) +
            "/" + playerStatus.MaxHelth + " ATK" + playerStatus.AttackValue +
            " DEF" + playerStatus.DeffenceValue + " MAG" + playerStatus.MagicValue +
            " SP" + playerStatus.SpeedValue;
        bpText.text = "BP" + playerStatus.BonusPoint;

        if (end) return;

        //ボス撃破でMenuSceneに遷移
        if (boss) {
            if (!MenuManager.FirstBossAlive) {
                end = true;
                boss = false;
                finishText.text = "VICTORY!";
                StartCoroutine("BackMenuScene");
                return;
            }
        }

        //体力0でMenuSceneに遷移
        if (playerStatus.HelthPoint <= 0) {
            end = true;
            finishText.text = "DEFEAT";
            StartCoroutine("BackMenuScene");
        }

        if (boss) return;　//ボス戦は時間制限なし

        //残り時間0でMenuSceneに遷移
        if (time <= 0) {
            end = true;
            finishText.text = "FINISH!";
            StartCoroutine("BackMenuScene");
        }

        time -= Time.deltaTime;　//時間経過
        timeText.text = Mathf.Clamp(time, 0, limitTime + 1).ToString("F2");
    }

    IEnumerator BackMenuScene() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MenuScene");
    }

    //ボタンを押した時の判定
    //上ボタン
    public void GetGoButtonDown() {
        GoClick = true;
    }
    public void GetGoButtonUp() {
        GoClick = false;
    }
    //下ボタン
    public void GetBackButtonDown() {
        BackClick = true;
    }
    public void GetBackButtonUp() {
        BackClick = false;
    }
    //左ボタン
    public void GetLeftButtonDown() {
        LeftClick = true;
    }
    public void GetLeftButtonUp() {
        LeftClick = false;
    }
    //右ボタン
    public void GetRightButtonDown() {
        RightClick = true;
    }
    public void GetRightButtonUp() {
        RightClick = false;
    }
    //攻撃ボタン
    public void GetAttackButtonDown() {
        AttackClick = true;
    }
    public void GetAttackButtonUp() {
        AttackClick = false;
    }
    //スキルボタン
    public void GetSkillButtonDown() {
        SkillClick = true;
    }
    public void GetSkillButtonUp() {
        SkillClick = false;
    }

    public static bool GoClick { get; set; }
    public static bool BackClick { get; set; }
    public static bool LeftClick { get; set; }
    public static bool RightClick { get; set; }
    public static bool AttackClick { get; set; }
    public static bool SkillClick { get; set; }

    public bool EndJudge() {
        if (end) return true;
        else return false;
    }

    public bool Boss {
        get { return boss; }
        set { boss = value; }
    }
}
