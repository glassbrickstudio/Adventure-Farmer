using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{

    public GameObject connectedDoor;
    private GameObject player;
    private float inputY;

   

    public Key keyscript;
    

    Inputactions  inputactions;

   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        keyscript = keyscript.GetComponent<Key>();

        
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Player") && keyscript.playerHasKey)
        {
            Debug.Log("player entered");
            if (inputY > 0.5f)
            {
                keyscript.playerHasKey = false;
                Destroy(keyscript.gameObject);
                Debug.Log("up pressed");
                player.transform.position = connectedDoor.transform.position;

                
            }

        }




    }

    public void GetInputMove(InputAction.CallbackContext context)
    {
        //read the movement value
        inputY = context.ReadValue<Vector2>().y;
        Debug.Log(inputY);


    }


}
