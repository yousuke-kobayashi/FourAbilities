using System.Collections;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour {
    MonsterStatus monsterStatus;

    public BattleManager battleManager;
    public GameObject slimePrefab;
    public GameObject skeletonPrefab;

    float timeLag;

	void Start () {
        if (MenuManager.FirstBossAlive) {
            GameObject skeleton = Instantiate(skeletonPrefab) as GameObject;
            monsterStatus = skeleton.GetComponent<MonsterStatus>();
            monsterStatus.BossInstantiate(200, 30, 30, 0);
        }
    }
	
	void Update () {
        if (battleManager.EndJudge()) return;

        if (!battleManager.Boss) {
            timeLag -= Time.deltaTime;  //時間経過
            //スライムを生成
            if (timeLag <= 0) {
                timeLag = 4.0f; //ラグリセット
                //前方から生成
                GameObject slime1 = Instantiate(slimePrefab) as GameObject;
                slime1.transform.position = new Vector3(Random.Range(-50, 50), 0, 50);
                monsterStatus = slime1.GetComponent<MonsterStatus>();
                monsterStatus.MonsterInstantiate();
                //後方から生成
                GameObject slime2 = Instantiate(slimePrefab) as GameObject;
                slime2.transform.position = new Vector3(Random.Range(-50, 50), 0, -50);
                monsterStatus = slime2.GetComponent<MonsterStatus>();
                monsterStatus.MonsterInstantiate();
                //右から生成
                GameObject slime3 = Instantiate(slimePrefab) as GameObject;
                slime3.transform.position = new Vector3(50, 0, Random.Range(-50, 50));
                monsterStatus = slime3.GetComponent<MonsterStatus>();
                monsterStatus.MonsterInstantiate();
                //左から生成
                GameObject slime4 = Instantiate(slimePrefab) as GameObject;
                slime4.transform.position = new Vector3(-50, 0, Random.Range(-50, 50));
                monsterStatus = slime4.GetComponent<MonsterStatus>();
                monsterStatus.MonsterInstantiate();
            }
        }
    }
}
