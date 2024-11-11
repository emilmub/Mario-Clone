using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WeakPoint"))
        {
            Destroy(collision.gameObject);
            GetComponentInParent<Rigidbody2D>().velocity += 10f * Time.deltaTime * Vector2.up;
        }
    }
}
