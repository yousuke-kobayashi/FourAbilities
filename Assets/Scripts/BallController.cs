using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour {
    GameObject[] player;
    PlayerStatus playerStatus;

    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player");
        playerStatus = player[0].GetComponent<PlayerStatus>();
    }

    //ボールを取ったら各能力が上昇
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {

            if (tag == "BallRed") {
                playerStatus.AttackValue++;
            }
            else if (tag == "BallYellow") {
                playerStatus.DeffenceValue++;
            }
            else if (tag == "BallBlue") {
                playerStatus.MagicValue++;
            }
            else if (tag == "BallGreen") {
                playerStatus.SpeedValue++;
            }
        }

        if (other.gameObject.tag == "Player" ||
            other.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}
