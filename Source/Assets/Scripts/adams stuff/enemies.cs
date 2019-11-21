using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemies : MonoBehaviour {


    private GameObject player;
    public GameObject bullet;
    public float speed;
    public int health;
    public float range;
    public float shotSpeed;
    float shotTimer;
    public bool controller;

    public bool red;
    public Animator anim;
    public float deathTimer;

    private Text score;
    public int scoreAmount = 1;

    public SpriteRenderer sr;
    public bool damaged;

    [SerializeField]
    AudioSource fliedawaySound;
    bool playedSound = false;
    [SerializeField]
    float soundVariation = 0.2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>();
    }

    void Update () {

        if (controller)
        {
            transform.right = player.transform.position - transform.position;
            if (Vector3.Distance(player.transform.position, transform.position) > range)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else
            {

                shotTimer -= Time.deltaTime;

                if (shotTimer < 0)
                {
                    Instantiate(bullet, transform.position, transform.rotation);
                    shotTimer = shotSpeed;
                }
            }
        }
        else
        {
            if(red)
            {
                anim.SetBool("red", true);
            }

            if (transform.parent.gameObject.GetComponent<enemies>().damaged == true)
            {
                sr.color = Color.gray;
            }

            if (transform.parent.gameObject.GetComponent<enemies>().health <= 0)
            {
                anim.SetBool("dead", true);
                deathTimer -= Time.deltaTime;

                if (!playedSound)
                {
                    fliedawaySound.pitch = 1.0f;
                    fliedawaySound.pitch += Random.Range(-soundVariation, soundVariation);
                    fliedawaySound.Play();
                    playedSound = true;
                }
            }

            if (deathTimer <= 0)
            {
                score.text = System.Convert.ToString(System.Convert.ToInt32(score.text) + scoreAmount);
                Destroy(transform.parent.gameObject);
            }

            transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "doughnut")
        {
            damaged = true;
            health -= 25;
        }
    }
}
