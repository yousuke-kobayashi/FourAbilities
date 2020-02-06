using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusManager : MonoBehaviour {
    public GameObject[] playerPrefab;
    public GameObject[] ballPrefabs;
    public Text statusText;
    public Text bpText;
    public Text finishText;

    GameObject player;
    PlayerStatus playerStatus;

    float timeLag = 0;

    void Awake() {
        //能力に応じてPlayerを生成
        if (MenuManager.Num() == 0) {
            player = Instantiate(playerPrefab[0]) as GameObject;
        }
        else if (MenuManager.Num() == 1) {
            player = Instantiate(playerPrefab[1]) as GameObject;
        }
        else if (MenuManager.Num() == 2) {
            player = Instantiate(playerPrefab[2]) as GameObject;
        }
        else if (MenuManager.Num() == 3) {
            player = Instantiate(playerPrefab[3]) as GameObject;
        }
    }

    void Start () {
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    void Update () {
        //テキスト更新
        statusText.text =
            "LV" + playerStatus.Level +
            " HP" + Mathf.Clamp(playerStatus.HelthPoint, 0, playerStatus.MaxHelth + 1) +
            "/" + playerStatus.MaxHelth + " ATK" + playerStatus.AttackValue +
            " DEF" + playerStatus.DeffenceValue + " MAG" + playerStatus.MagicValue +
            " SP" + playerStatus.SpeedValue;
        bpText.text = "BP" + playerStatus.BonusPoint;

        timeLag -= Time.deltaTime;

        if (timeLag <= 0 && playerStatus.BonusPoint > 0) {
            timeLag = 0.5f;

            //ランダムな数値に応じて、ballPrefabsを生成
            int num = Random.Range(0, ballPrefabs.Length);
            GameObject ball = Instantiate(ballPrefabs[num]) as GameObject;
            ball.transform.position = new Vector3(Random.Range(-13.0f, 13.0f), 20, 0);

            playerStatus.BonusPoint--;
        }

        if (playerStatus.BonusPoint <= 0) {
            StartCoroutine("BackMenuScene");
        }
    }

    IEnumerator BackMenuScene() {
        yield return new WaitForSeconds(3);
        finishText.text = "FINISH!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MenuScene");
    }

    public void GetRightButtonDown() {
        RightClick = true;
    }
    public void GetRightButtonUp() {
        RightClick = false;
    }

    public void GetLeftButtonDown() {
        LeftClick = true;
    }
    public void GetLeftButtonUp() {
        LeftClick = false;
    }

    public bool RightClick { get; set; }
    public bool LeftClick { get; set; }
}
