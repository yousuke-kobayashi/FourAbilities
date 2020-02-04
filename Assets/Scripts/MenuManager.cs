using System.Collections;
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

    void Awake() {
        player = Instantiate(playerPrefab[0]) as GameObject;  //インスタンス化
    }

    void Start() {
        playerStatus = player.GetComponent<PlayerStatus>();
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

    public static bool FirstBossAlive { get; set; }
}
