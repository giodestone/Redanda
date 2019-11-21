using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeperScript : MonoBehaviour {
    public int displayScore;
    Text scoretext;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        scoretext = GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        if (scoretext != null)  
            displayScore = System.Convert.ToInt32(scoretext.text);

        if (SceneManager.GetActiveScene().name != "mainGame" && SceneManager.GetActiveScene().name != "GameOver")
            Destroy(this.gameObject);
    }
}
