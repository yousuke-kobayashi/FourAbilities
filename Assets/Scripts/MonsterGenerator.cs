using System.Collections;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {
    MonsterStatus monsterStatus;

    public BattleManager battleManager;
    public GameObject slimePrefab;
    public GameObject turtlePrefab;
    public GameObject skeletonPrefab;

    public float generateSpeed;

    GameObject[] player;
    PlayerStatus playerStatus;

    float timeLag;

	void Start () {
        player = GameObject.FindGameObjectsWithTag("Player");
        playerStatus = player[0].GetComponent<PlayerStatus>();

        if (MenuManager.FirstBossAlive) {
            GameObject skeleton = Instantiate(skeletonPrefab) as GameObject;
            monsterStatus = skeleton.GetComponent<MonsterStatus>();
            monsterStatus.BossInstantiate(200, 30, 30, 0);
        }
    }
	
	void Update () {
        if (battleManager.EndJudge()) return;

        //レベルに応じて生成速度を上げる
        float count = Mathf.Clamp(generateSpeed - playerStatus.Level * 0.01f, 2, 4);

        if (!battleManager.Boss)
        {
            timeLag -= Time.deltaTime;  //時間経過

            if (timeLag <= 0)
            {
                timeLag = count; //ラグリセット

                if (!MenuManager.FirstBossDead)
                {
                    //スライムを生成

                    //前方から生成
                    GameObject slime1 = Instantiate(slimePrefab) as GameObject;
                    slime1.transform.position = new Vector3(Random.Range(-50, 50), 0, 50);
                    monsterStatus = slime1.GetComponent<MonsterStatus>();
                    monsterStatus.SlimeInstantiate();
                    //後方から生成
                    GameObject slime2 = Instantiate(slimePrefab) as GameObject;
                    slime2.transform.position = new Vector3(Random.Range(-50, 50), 0, -50);
                    monsterStatus = slime2.GetComponent<MonsterStatus>();
                    monsterStatus.SlimeInstantiate();
                    //右から生成
                    GameObject slime3 = Instantiate(slimePrefab) as GameObject;
                    slime3.transform.position = new Vector3(50, 0, Random.Range(-50, 50));
                    monsterStatus = slime3.GetComponent<MonsterStatus>();
                    monsterStatus.SlimeInstantiate();
                    //左から生成
                    GameObject slime4 = Instantiate(slimePrefab) as GameObject;
                    slime4.transform.position = new Vector3(-50, 0, Random.Range(-50, 50));
                    monsterStatus = slime4.GetComponent<MonsterStatus>();
                    monsterStatus.SlimeInstantiate();
                }
                else {
                    //トゲカメを生成

                    //前方から生成
                    GameObject turtle1 = Instantiate(turtlePrefab) as GameObject;
                    turtle1.transform.position = new Vector3(Random.Range(-50, 50), 0, 50);
                    monsterStatus = turtle1.GetComponent<MonsterStatus>();
                    monsterStatus.TurtleInstantiate();
                    //後方から生成
                    GameObject turtle2 = Instantiate(turtlePrefab) as GameObject;
                    turtle2.transform.position = new Vector3(Random.Range(-50, 50), 0, -50);
                    monsterStatus = turtle2.GetComponent<MonsterStatus>();
                    monsterStatus.TurtleInstantiate();
                    //右から生成
                    GameObject turtle3 = Instantiate(turtlePrefab) as GameObject;
                    turtle3.transform.position = new Vector3(50, 0, Random.Range(-50, 50));
                    monsterStatus = turtle3.GetComponent<MonsterStatus>();
                    monsterStatus.TurtleInstantiate();
                    //左から生成
                    GameObject turtle4 = Instantiate(turtlePrefab) as GameObject;
                    turtle4.transform.position = new Vector3(-50, 0, Random.Range(-50, 50));
                    monsterStatus = turtle4.GetComponent<MonsterStatus>();
                    monsterStatus.TurtleInstantiate();

                }
            }
        }
    }
}
