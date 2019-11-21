using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    

    public float spawnTimer = 15f;
    public GameObject[] ObjectsToSpawn;

    private float Timer;
    int waveNumber = 1;
    int i = 0;
    void Start()
    {

    }

    void Update()
    {
        

        Timer -= Time.deltaTime;

        if (Timer < 0)
        {
            while (i < waveNumber * 5)
            {
                Vector3 rndPosWithin;
                rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
                Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length)], rndPosWithin, transform.rotation);
                i += 1;
            }

            Timer = 30;
            i = 0;
            waveNumber += 1;
        }

    }
}
