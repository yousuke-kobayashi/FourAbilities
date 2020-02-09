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
    public Text mpText;
    public Slider slider;
    public Button skillButton;
    public Color color1;
    public Color color2;

    GameObject player;
    PlayerStatus playerStatus;
    Image button;

    int mp = 0;
    int maxMp = 10;
    float mpCountTime;
    float timeLag = 0;

    void Awake() {
        //能力に応じてPlayerを生成
        /*if (MenuManager.Num() == 0) {
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
        }*/
        player = Instantiate(playerPrefab[0]) as GameObject;
    }

    void Start () {
        playerStatus = player.GetComponent<PlayerStatus>();
        button = skillButton.GetComponentInChildren<Image>();
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

        //BPがなくなったら、MenuSceneへ遷移
        if (playerStatus.BonusPoint <= 0) {
            StartCoroutine("BackMenuScene");
        }

        //最大MPになるまで0.5秒間隔でMPを増やす
        if (mpCountTime <= 0 && mp < maxMp) {
            mp++;
            mpCountTime = 0.5f;
        }
        else {
            mpCountTime -= Time.deltaTime;
        }

        //MPゲージの更新
        if (mp == 10) {
            slider.value = 10;

            skillButton.interactable = true;
            button.color = color2;
        }
        else {
            slider.value = mp;

            skillButton.interactable = false;
            button.color = color1;
        }

        mpText.text = "MP" + mp;
    }

    public void ConsumeMP() {
        mp = 0;
        mpCountTime = 0.5f;
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

    public void GetSkillButtonDown() {
        SkillClick = true;
    }
    public void GetSkillButtonUp() {
        SkillClick = false;
    }

    public bool RightClick { get; set; }
    public bool LeftClick { get; set; }
    public bool SkillClick { get; set; }

    public int MP {
        get { return mp; }
    }
}
