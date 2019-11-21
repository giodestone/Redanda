using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreGUI : MonoBehaviour {

    public Text countText;
    public int count;

    void Start ()
    {
        SetCountText ();
    }

    void FixedUpdate ()
    {
       // count = count + 1;
        SetCountText();
    }


    void SetCountText ()
    {
        countText.text = count.ToString ();
    }
}
