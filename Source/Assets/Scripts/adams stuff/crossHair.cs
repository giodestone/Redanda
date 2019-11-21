using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossHair : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()   
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));    //Get the cursor position on the screen
    }
}
