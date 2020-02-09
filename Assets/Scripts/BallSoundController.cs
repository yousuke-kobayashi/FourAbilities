using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundController : MonoBehaviour {
    public AudioClip sound;

    AudioSource audioSource;

    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other) {
        //ボールを取ったら音を出す
        if (other.gameObject.tag == "BallRed" ||
            other.gameObject.tag == "BallYellow" ||
            other.gameObject.tag == "BallBlue" ||
            other.gameObject.tag == "BallGreen")
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
