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
        player = Instantiate(playerPrefab[0]) as GameObject;  //インスタンス化
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
}
