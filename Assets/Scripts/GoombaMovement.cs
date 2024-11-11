using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = -1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (horizontalSpeed * Time.deltaTime) * Vector3.right;
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
                    horizontalSpeed *= -1f;
                    break;
                }
            }
        }
    }
}
