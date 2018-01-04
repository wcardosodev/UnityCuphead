using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;

    SpriteRenderer spriteRenderer;
    Animator animator;

    BoxCollider2D groundCheck;
    public bool grounded;

    public bool facingLeft;

    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float jumpStrength = 20f;

    [SerializeField] float dashRange = 4f;

    [SerializeField] float xMinMapBoundary, xMaxMapBoundary, yMinMapBoundary, yMaxMapBoundary;

    bool canMove = true;

    void Start () {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        groundCheck = GetComponentInChildren<BoxCollider2D>();
	}
	
	void Update () {

        if (canMove)
        {
            animator.SetBool("Grounded", grounded);

            float x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
            //Clamp the map position
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMinMapBoundary, xMaxMapBoundary), transform.position.y);

            if (x != 0)
            {
                animator.SetBool("Moving", true);
            }

            PlayerRotation(x);

            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                Dash();
            }

            GravityOnFall();
        }

	}

    void PlayerRotation(float xDir)
    {
        if (xDir > 0)
        {
            facingLeft = false;
            transform.rotation = Quaternion.Euler(new Vector2(0, 0));
            transform.Translate(xDir, 0, 0);
        }
        else if(xDir < 0)
        {
            facingLeft = true;
            transform.rotation = Quaternion.Euler(new Vector2(0, 180));
            transform.Translate(-xDir, 0, 0);
        }
    }

    void GravityOnFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2 - 1) * Time.deltaTime;
        }
    }

    private void Jump()
    {
        grounded = false;

        rb.velocity = Vector2.up * jumpStrength;
    }

    void Dash()
    {
        //play dash animation
        transform.Translate(Vector2.right * dashRange);
    }

    public bool FacingLeft
    {
        get { return facingLeft; }
    }

    public bool CanMove
    {
        set { canMove = value; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatforms>())
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatforms>())
        {
            transform.parent = null;
        }
    }
}
