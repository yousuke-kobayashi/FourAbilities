using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public GameObject[] playerPrefab;
    public Text lvText;
    public Text hpText;
    public Text atkText;
    public Text defText;
    public Text magText;
    public Text spText;
    public Text bpText;
    public Button bossButton;

    GameObject player;
    PlayerStatus playerStatus;

    static List<int> statusArray = new List<int>();
    static int max;

    void Awake() {
        player = Instantiate(playerPrefab[0]) as GameObject;
        playerStatus = player.GetComponent<PlayerStatus>();

        statusArray.Clear();

        statusArray.Add(playerStatus.AttackValue);
        statusArray.Add(playerStatus.DeffenceValue);
        statusArray.Add(playerStatus.MagicValue);
        statusArray.Add(playerStatus.SpeedValue);
        //ステータスの最大値
        max = statusArray.Max();

        Destroy(player);
    }

    void Start() {

        if (Num() == 0) {
            player = Instantiate(playerPrefab[0]) as GameObject;
        } else if (Num() == 1) {
            player = Instantiate(playerPrefab[1]) as GameObject;
        } else if (Num() == 2) {
            player = Instantiate(playerPrefab[2]) as GameObject;
        } else if (Num() == 3) {
            player = Instantiate(playerPrefab[3]) as GameObject;
        }
        Debug.Log(max);
        Debug.Log(statusArray[0]);
    }

    void Update() {
        playerStatus.HelthReset();  //体力全快

        lvText.text = "LV " + playerStatus.Level + "(" + playerStatus.ExperiencePoint + "/" + playerStatus.NeedEXP + ")";
        hpText.text = "HP " + playerStatus.MaxHelth;
        atkText.text = "ATK " + playerStatus.AttackValue;
        defText.text = "DEF " + playerStatus.DeffenceValue;
        magText.text = "MAG " + playerStatus.MagicValue;
        spText.text = "SP " + playerStatus.SpeedValue;
        bpText.text = "BP " + playerStatus.BonusPoint;

        //全ステータスが一定値を超えていたらBossButtonを有効化
        if (playerStatus.StatusOver(30)) {
            bossButton.interactable = true;
        }
        else {
            //BossButtonを無効化
            bossButton.interactable = false;
        }
    }

    public void OnClickTrainingButton() {
        SceneManager.LoadScene("BattleScene");
    }

    public void OnClickBossButton() {
        FirstBossAlive = true;
        SceneManager.LoadScene("BattleScene");
    }

    public void OnClickBonusButton() {
        SceneManager.LoadScene("BonusScene");
    }

    //どのPlayerを生成するか決める
    public static int Num() {
        if (max == statusArray[0]) {
            return 0;
        } else if (max == statusArray[1]) {
            return 1;
        } else if (max == statusArray[2]) {
            return 2;
        } else if (max == statusArray[3]) {
            return 3;
        }
        else { return 99; }
        
    }

    public static bool FirstBossAlive { get; set; }
}
