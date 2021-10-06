using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    private SpringJoint2D spring;
    public bool playerHasKey;
    // Start is called before the first frame update
    void Start()
    {
        playerHasKey = false;

        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false;
        GameObject backpack = GameObject.FindGameObjectWithTag("Backpack");
        spring.connectedBody = backpack.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHasKey = true;
            spring.enabled = true;
           

            
        }
        
    }
}
