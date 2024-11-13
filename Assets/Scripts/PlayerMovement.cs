using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float gravityMultFalling = 1.5f;
    [SerializeField] float gravityMultShortJump = 1f;

    Rigidbody2D playerRB;
    BoxCollider2D playerCollider;
    SpriteRenderer playerSR;
    bool inAir = false;
    bool poweredUp = false;
    bool isInvincible = false;
    bool running = false;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if ((Input.GetKeyDown(KeyCode.LeftShift) ||  Input.GetKeyDown(KeyCode.RightShift)) && !running)
        {
            speed *= 2;
            running = true;
        }

        if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) && running)
        {
            speed /= 2;
            running = false;
        }

        Jump();

    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += speed * horizontalInput * Time.deltaTime * Vector3.right;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        List<ContactPoint2D> collisionContacts = new List<ContactPoint2D>();
        collision.GetContacts(collisionContacts);

        // When colliding with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyCollision(collision, collisionContacts[0]);
            
        }
        else if (collision.gameObject.CompareTag("Mushroom"))
        {
            Destroy(collision.gameObject);
            if (!poweredUp)
            {
                poweredUp = true;
                transform.localScale = new Vector3(1,1.5f);
            }
        }
        else
        {
            for (int i = 0; i < collisionContacts.Count; i++)
            {
                if (collisionContacts[i].normal == new Vector2(0,1))
                {
                    inAir = false;
                    break;
                }

                if (collision.gameObject.CompareTag("Brick") && collisionContacts[i].normal == new Vector2(0, -1) && poweredUp)
                {
                    Destroy(collision.gameObject);
                    break;
                }
            }

            
        }
    }

    void EnemyCollision(Collision2D collision, ContactPoint2D contactPoint)
    {
        // If colliding with top edge
        if (contactPoint.normal == new Vector2(0, 1))
        {
            Destroy(collision.gameObject);
            playerRB.velocity += jumpSpeed * Vector2.up;
        }
        else
        {
            // If getting hit and powered up
            if (poweredUp)
            {
                transform.localScale = new Vector3(1, 1);
                poweredUp = false;
                StartCoroutine("BecomeInvincible");
            }
            // If getting hit outside of invincibility frames
            else if (!isInvincible)
            {
                Destroy(gameObject);
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            playerRB.velocity += jumpSpeed * Vector2.up;
            inAir = true;
        }

        // When the player is falling, increase the gravity
        if (playerRB.velocity.y < 0f)
        {
            playerRB.velocity += Vector2.up * (gravityMultFalling * Physics2D.gravity.y * Time.deltaTime);
        }
        // When the player is going up but the jump button is not being pressed, increase gravity
        else if (playerRB.velocity.y > 0f && !Input.GetKey(KeyCode.Space))
        {
            playerRB.velocity += Vector2.up * (gravityMultShortJump * Physics2D.gravity.y * Time.deltaTime);

        }
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        float a = -0.5f;
        //playerCollider.isTrigger = true;
        for (float i = 0; i < 1.5; i += 0.1f)
        {
            playerSR.color += new Color(0, 0, 0, a);
            a *= -1;
            yield return new WaitForSeconds(0.1f);
        }
        if (playerSR.color.a < 1)
        {
            playerSR.color += new Color(0, 0, 0, 0.5f);
        }
        isInvincible = false;
        //playerCollider.isTrigger = false;
    }
}
