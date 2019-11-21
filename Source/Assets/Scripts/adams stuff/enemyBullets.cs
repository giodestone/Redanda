using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullets : MonoBehaviour {

    public float speed;

    [SerializeField]
    AudioSource throwSound;
    [SerializeField]
    float soundVariation = 0.35f;

    private void Start()
    {
        throwSound.pitch = 1.0f;
        throwSound.pitch += Random.Range(-soundVariation, soundVariation);
        throwSound.Play();
    }
    void Update () {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Destroy(gameObject);
    }
}
