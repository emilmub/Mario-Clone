using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBoxOpen : MonoBehaviour
{
    [SerializeField] GameObject collectible;

    SpriteRenderer boxSR;
    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        boxSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !opened)
        {
            List<ContactPoint2D> collisionContacts = new List<ContactPoint2D>();
            collision.GetContacts(collisionContacts);
            for (int i = 0; i < collisionContacts.Count; i++) 
            { 
                if (collisionContacts[i].normal == new Vector2(0,1))
                {
                    boxSR.color = new Color(0.7f, 0.35f, 0.06f);
                    opened = true;
                    Instantiate(collectible);
                    break;
                }
            }
        }
    }
}
