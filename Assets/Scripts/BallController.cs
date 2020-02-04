using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour {
    GameObject[] player;
    PlayerStatus playerStatus;

    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player");
        playerStatus = player[0].GetComponent<PlayerStatus>();
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
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

        if (collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}
