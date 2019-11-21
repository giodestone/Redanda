using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = "Your Score: " + System.Convert.ToString(GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeperScript>().displayScore);
        Destroy(GameObject.FindGameObjectWithTag("ScoreKeeper"));
    }
	
}
