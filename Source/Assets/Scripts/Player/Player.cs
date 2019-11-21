using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public PlayerStat playerStat;

    //doughnut
    [SerializeField]
    GameObject doughtnut;
    float timeLastFired = 0.0f;
    float timeLastFiredNoAmmo = 0.0f;
    [SerializeField]
    float doughnutFireInterval = 0.3f;

    //sound
    [SerializeField]
    AudioSource fireSound;
    [SerializeField]
    AudioSource emptyClip;
    [SerializeField]
    AudioSource fliedSound;

    //sprites
    SpriteRenderer spriteRenderer;

    //[SerializeField]
    //Sprite facingForwards;
    //[SerializeField]
    //Sprite facingBackwards;
    //[SerializeField]
    //Sprite facingLeft;
    //[SerializeField]
    //Sprite facingRight;

    [SerializeField]
    TypingScript typingScript;

    //Ammo text
    [SerializeField]
    Text ammoText;

    [SerializeField]
	float movementSpeed = 5.0f;
	[SerializeField]
	float slidiness = 2.0f;
	
	bool wasKeyDown = false;

    Vector2 dir = Vector2.zero;
	Vector2 oldDir;
	
	float slide;
    bool sliding;
    float timeSliding;

    public int health = 100;
    Color colourOfdeath = new Color(1, 1, 1);

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStat.Ammunition = playerStat.MaxAmmuntion;
        playerStat.IsAlive = true;
        playerStat.IsTyping = false;
        GetComponent<Animator>().SetBool("Alive", true);
        spriteRenderer.color = colourOfdeath;

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (playerStat.IsAlive)
        {
            if (Input.GetAxis("Reload") > 0.0f && !playerStat.IsTyping)
            {
                typingScript.Reload();
            }
            else if (!playerStat.IsTyping)
            {
                HandleInput();
            }
            else if (playerStat.IsTyping)
            {
                dir = Vector2.zero;
            }
        }

        //update text
        ammoText.text = playerStat.Ammunition.ToString() + " / " + playerStat.MaxAmmuntion.ToString();

        if (playerStat.Ammunition < 5)
            ammoText.color = new Color(1.0f, 0, 0);
        else
            ammoText.color = new Color(1.0f, 1.0f, 1.0f);

        if (health <= 0)
            Flied();
    }

    void FixedUpdate()
    {
        if (playerStat.IsAlive)
        {
            MovePosition();
            UpdateSprite();
        }

        timeLastFired += Time.deltaTime; //needs to be here because this is where time.deltaTime is accurate
        timeLastFiredNoAmmo += Time.deltaTime;
    }

    public void Flied()
    {
        //spriteRenderer.sprite = facingForwards;
        fliedSound.Play();
        GetComponent<Animator>().SetInteger("walkState", 0);
        GetComponent<Animator>().SetBool("Alive", false);
        playerStat.IsAlive = false;
    }

    public void AnimationFinished() 
    {
        GetComponent<Animator>().speed = 0;
        SceneManager.LoadScene("Scenes/GameOver");
    }

    private void HandleInput()
    {
        HandleMovement();
        HandleDoughnuts();
    }

    private void HandleMovement()
    {
        wasKeyDown = false;

        if (Input.GetAxis("MoveX") > 0.0f || Input.GetAxis("MoveX") < 0.0f)
        {
            dir.x = Input.GetAxis("MoveX");
            wasKeyDown = true;
            sliding = true;
        }
        else
        {
            oldDir.x = dir.x;
            dir.x = 0.0f;
        }

        if (Input.GetAxis("MoveY") > 0.0f || Input.GetAxis("MoveY") < 0.0f)
        {
            dir.y = Input.GetAxis("MoveY");
            wasKeyDown = true;
            sliding = true;
        }
        else
        {
            oldDir.y = dir.y;
            dir.y = 0.0f;
        }
    }

    private void HandleDoughnuts()
    {
        if (Input.GetButton("Fire1") && playerStat.Ammunition > 0 && 
            timeLastFired >= doughnutFireInterval && Time.timeScale == 1)
        {
            Instantiate(doughtnut).GetComponent<Mover>().SetUpMover(this.transform.position, GetMouseDir());

            playerStat.Ammunition--;
            fireSound.Play();
            Debug.Log("Fired a doughnut.");
            timeLastFired = 0.0f;
        }
        else if (Input.GetButton("Fire1") && playerStat.Ammunition <= 0 && timeLastFiredNoAmmo >= doughnutFireInterval)
        {
            emptyClip.Play();
            timeLastFiredNoAmmo = 0.0f;
            Debug.Log("Not enough ammo.");
        }
    }

    private void MovePosition()
    {
        Vector2 calculatedDir; //sliding mechanics
        if (dir != Vector2.zero) //if button was clicked
        {
            calculatedDir = dir * movementSpeed * Time.deltaTime; //move according to speed
            oldDir = dir; //setup slidiness
            slide = movementSpeed;
        }
        else //slide if no buttons are clicked
        {
            calculatedDir = oldDir * slide * Time.deltaTime;
            slide = Mathf.Lerp(slide, 0, timeSliding);
            timeSliding += Time.deltaTime;
            slide = Mathf.Clamp(slide, 0.0f, movementSpeed); //make sure that the player doesnt start sliding backwards
            if (slide == 0.0f)
            {
                sliding = false;
            }
        }

        dir = dir.normalized;

        transform.Translate(calculatedDir);
    }

    private void UpdateSprite()
    {
        if (!sliding)
            //spriteRenderer.sprite = facingForwards;
            GetComponent<Animator>().SetInteger("walkState", 0);
        else
        {
            if (dir.x > 0.0f)
            {
                //spriteRenderer.sprite = facingRight;
                GetComponent<Animator>().SetInteger("walkState", 3);
            }
            else if (dir.x < 0.0f)
            {
                //spriteRenderer.sprite = facingLeft;
                GetComponent<Animator>().SetInteger("walkState", 2);
            }
            else if (dir.y > 0.0f)
            {
                //spriteRenderer.sprite = facingBackwards;
                GetComponent<Animator>().SetInteger("walkState", 1);
            }
            else if (dir.y < 0.0f)
            {
                //spriteRenderer.sprite = facingForwards;
                GetComponent<Animator>().SetInteger("walkState", 0);
            }
        }
    }

    private void PointToMouse()
    {
        Vector3 mouse_pos;
        Vector3 object_pos;
        float angle;

        mouse_pos = Input.mousePosition;
        mouse_pos.z = -20;
        object_pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private Vector2 GetMouseDir()
    {
        Vector2 mousePos;
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        mousePos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        mousePos.Normalize();

        return mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "knife")
        {
            colourOfdeath -= new Color(0.04f, 0.04f, 0.04f, 0);
            spriteRenderer.color = colourOfdeath;
            health -= 5;
        }
    }
}
