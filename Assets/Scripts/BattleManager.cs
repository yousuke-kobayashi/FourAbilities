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
        player = Instantiate(playerPrefab[0]) as GameObject;  //インスタンス化
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
        //テキスト更新
        statusText.text =
            "LV" + playerStatus.Level +
            " HP" + Mathf.Clamp(playerStatus.HelthPoint, 0, playerStatus.MaxHelth) +
            "/" + playerStatus.MaxHelth + " ATK" + playerStatus.AttackValue +
            " DEF" + playerStatus.DeffenceValue + " MAG" + playerStatus.MagicValue +
            " SP" + playerStatus.SpeedValue;
        bpText.text = "BP" + playerStatus.BonusPoint;

        if (end) return;

        //体力0でMenuSceneに遷移
        if (playerStatus.HelthPoint <= 0) {
            end = true;
            finishText.text = "DEFEAT";
            StartCoroutine("BackMenuScene");
        }

        if (boss) return;

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

    public bool EndJudge() {
        if (end) return true;
        else return false;
    }

    public bool Boss {
        get { return boss; }
        set { boss = value; }
    }
}
