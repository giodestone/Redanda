using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseFunction : MonoBehaviour {

    private Text pt;
    public bool pause;

	void Start () {
        pt = GameObject.FindGameObjectWithTag("pauseText").GetComponent<Text>();
    }
	
	
	void Update () {

        if(Input.GetButtonUp("pause"))
        {
            if (!pause)
            {
                pt.enabled = true;
                Time.timeScale = 0;
                pause = true;
            }
            else
            {
                pt.enabled = false;
                Time.timeScale = 1;
                pause = false;
            }
        }
	}
}
