using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colourCycleScript : MonoBehaviour {

    public Color[] colourToCycle;
    private Color lerpedColor = Color.white;
    private float colourLerpTimer;
    private int counter;
    private SpriteRenderer sp;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();       
    }

    void Update()
    {
        if (lerpedColor == colourToCycle[counter + 1])
        {
            if (counter < colourToCycle.Length - 2)
            {
                colourLerpTimer = colourLerpTimer - 1;
                counter += 1;
            }
            else
            {
                colourLerpTimer = colourLerpTimer - 1;
                counter = 0;
            }
        }
        else
        {
            colourLerpTimer += Time.deltaTime *0.2f;
            lerpedColor = Color.Lerp(colourToCycle[counter], colourToCycle[counter + 1], colourLerpTimer);
        }
            
        sp.color = lerpedColor;
    }
}
