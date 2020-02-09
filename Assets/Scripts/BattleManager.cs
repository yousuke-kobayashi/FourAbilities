using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    public GameObject[] playerPrefab;
    public AudioClip bgm;
    public AudioClip bossBgm;
    public Text statusText;
    public Text bpText;
    public Text timeText;
    public Text finishText;
    public Text mpText;
    public Slider slider;
    public Button skillButton;
    public Color color1;
    public Color color2;

    public float limitTime;  //制限時間

    PlayerStatus playerStatus;
    GameObject player;
    AudioSource audioSource;
    Image button;

    int mp = 0;
    int maxMp = 10;
    float mpCountTime;
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
        //player = Instantiate(playerPrefab[0]) as GameObject;
    }

    void Start() {
        playerStatus = player.GetComponent<PlayerStatus>();
        audioSource = GetComponent<AudioSource>();
        button = skillButton.GetComponentInChildren<Image>();

        //ボス戦の場合
        if (MenuManager.FirstBossAlive) {
            boss = true;
            audioSource.PlayOneShot(bossBgm);
            return;
        }

        audioSource.PlayOneShot(bgm);

        time = limitTime + playerStatus.Level;
        timeText.text = time.ToString("F2");
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
                //boss = false;
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

        //最大MPになるまで2.0秒間隔でMPを増やす
        if (mpCountTime <= 0 && mp < maxMp) {
            mp++;
            mpCountTime = 2.0f;
        }
        else {
            mpCountTime -= Time.deltaTime;
        }

        //MPゲージの更新
        if (mp == 10) {
            slider.value = 10;

            skillButton.interactable = true;
            button.color = color2;
        } else {
            slider.value = mp;

            skillButton.interactable = false;
            button.color = color1;
        }

        mpText.text = "MP" + mp;

        if (boss) return;　//ボス戦は時間制限なし

        //残り時間0でMenuSceneに遷移
        if (time <= 0) {
            end = true;
            finishText.text = "FINISH!";
            StartCoroutine("BackMenuScene");
        }

        time -= Time.deltaTime;　//時間経過
        timeText.text = Mathf.Clamp(time, 0, limitTime + playerStatus.Level).ToString("F2");
    }

    public void ConsumeMP() {
        mp = 0;
        mpCountTime = 2.0f;
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

    public bool GoClick { get; set; }
    public bool BackClick { get; set; }
    public bool LeftClick { get; set; }
    public bool RightClick { get; set; }
    public bool AttackClick { get; set; }
    public bool SkillClick { get; set; }

    public int MP {
        get { return mp; }
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
