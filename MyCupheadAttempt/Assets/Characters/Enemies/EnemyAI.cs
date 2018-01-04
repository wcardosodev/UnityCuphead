using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    int playerLayer = 9;

    [SerializeField] float activateRange = 5f;
    [SerializeField] float movementSpeed = 4f;

    bool activated = false;
    bool flipDirection = false;

    BoxCollider2D boxCollider;

    GameObject player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        boxCollider = GetComponent<BoxCollider2D>();
	}

    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= activateRange)
        {
            activated = true;
        }

        if (activated)
        {
            Move();
        }
    }

    void Move()
    {
        if (!flipDirection)
        {
            transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
        }
    }

    void FlipDirection()
    {
        flipDirection = !flipDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If colliding with the player
        if(collision.gameObject.layer == playerLayer && collision.gameObject.tag == "Player")
        {
            collision.GetComponent<CharacterHealth>().TakeDamage();
        }

        //If colliding with a "Change direction" object
        if (collision.gameObject.tag == "AIBarrier")
        {
            print("Ai barrier");
            FlipDirection();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activateRange);
    }
}
