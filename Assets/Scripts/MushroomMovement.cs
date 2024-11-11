using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    Vector3 startingPosition;
    Rigidbody2D mushroomRB;
    bool outOfBox = false;
    [SerializeField] float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        mushroomRB = GetComponent<Rigidbody2D>();
        mushroomRB.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < startingPosition.y + 1 && !outOfBox)
        {
            transform.position += (speed * Time.deltaTime) * Vector3.up;
        }
        else
        {
            if (mushroomRB.gravityScale == 0)
            {
                outOfBox = true;
                mushroomRB.gravityScale = 1;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            transform.position += (speed * Time.deltaTime) * Vector3.right;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string[] notWalls = { "Floor", "Player" };
        if (!notWalls.Contains<string>(collision.gameObject.tag))
        {
            List<ContactPoint2D> collisionContacts = new List<ContactPoint2D>();
            collision.GetContacts(collisionContacts);
            for (int i = 0; i < collisionContacts.Count; i++)
            {
                if (collisionContacts[i].normal.x != 0)
                {
                    speed *= -1f;
                    break;
                }
            }
        }
    }
}
