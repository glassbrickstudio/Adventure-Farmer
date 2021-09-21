using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;

    private PlayerActionControls playerActionControls;

    private Rigidbody2D rb;

    private Collider2D col;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private AudioClip deathSound;

    private AudioSource audiosource;

    private bool isAlive;

    float inputX;
    float jumpForce;




    private void Awake()
    {
       
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();

    }






    void Start()
    {
        
        deathSound = Resources.Load<AudioClip>("DeathFail8Bit");
        isAlive = true;
    }


    void Update()
    {

        Move();


    }



    public void GetInputMove(InputAction.CallbackContext context)
    {
        //read the movement value
        inputX = context.ReadValue<Vector2>().x;

        

    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (context.performed && isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            //animation
            animator.SetTrigger("Jump");


        }



    }

    private void Move()
    {
        //move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += inputX * speed * Time.deltaTime;


        //move position
        transform.position = currentPosition;


        //animation
        if (inputX != 0)
        {
            animator.SetBool("Run", true);

        }
        else
        {
            animator.SetBool("Run", false);

        }

        //sprite flip
        if (inputX < -0.1f)
        {
            spriteRenderer.flipX = true;

        }
        else if (inputX > 0.1f)
        {
            spriteRenderer.flipX = false;

        }
    }
   

    private bool isGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x;
        bottomRight.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRight, ground);


    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && isAlive)
        {
            isAlive = false;

            StartCoroutine(PlaySound());

        }

    }




    IEnumerator PlaySound()
    {
        Debug.Log("corountine call");
        audiosource.clip = deathSound;
        audiosource.Play();


        playerActionControls.Land.Disable();

        yield return new WaitUntil(() => audiosource.isPlaying == false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    }







}//end
