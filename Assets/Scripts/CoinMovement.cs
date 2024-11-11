using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    Rigidbody2D coinRB;
    [SerializeField] float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        coinRB = GetComponent<Rigidbody2D>();
        coinRB.velocity = speed*Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (coinRB.velocity.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
