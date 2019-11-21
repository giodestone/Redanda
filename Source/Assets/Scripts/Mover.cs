using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    [SerializeField]
    float speed = 20.0f;

    [SerializeField]
    float sdfCoord = 1000.0f;

    Rigidbody2D rigidbody2d;

    public Vector2 vel = Vector2.zero;

	// Use this for initialization
	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void SetUpMover(Vector3 pos, Vector2 dir)
    {
        this.transform.position = pos;
        vel = dir * speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(vel.x, vel.y, 0) * Time.deltaTime);
    }

    private void Update()
    {
        CheckIfOutOfBounds();
    }

    private void CheckIfOutOfBounds()
    {
        if (transform.position.x >= sdfCoord ||
                    transform.position.x <= -sdfCoord ||
                    transform.position.y >= sdfCoord ||
                    transform.position.y <= -sdfCoord)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
