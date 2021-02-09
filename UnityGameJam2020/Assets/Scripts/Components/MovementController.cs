using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class MovementController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float speed;
    public float jumpForce;
    public float timeElapsed;
    public Rigidbody2D rb;
    public  Animator animator;

    public AudioController audioController;
    public Animator legsAnimator;
    public CollisionController collisionController;

    public GameObject Torch;
    public Torch torch;
    public GameObject Legs;
    public GameObject particle;
    public Transform groundPoint;
    public Sprite legs_right;
    public Sprite legs_left;
    public Sprite sandeep;
    public Sprite sandeep_left;
    public Sprite sandeep_arm;
    public Sprite sandeep_arm_left;
    public Transform LightPoint;
    public bool isJumping = false;
    public  bool stunned = false;
    public  float maxSpeed;

    public bool createParticle = false;

    float particleTimer = 0;
    private bool right = true;
    private bool isFacingRight;

    private bool isOnStairs = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        collisionController = GetComponent<CollisionController>();
        torch = Torch.GetComponent<Torch>();
    }
    
    void Update()
    {
        //audioController.PlayWalk();
        if(!stunned){
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                isJumping = true;
            }

            if(mouseScreenPosition.x  < gameObject.transform.position.x)
            {
                flipLeft();
                if(Input.GetAxisRaw("Horizontal") == 0)
                {
                animator.SetInteger("direction", 3);
                }
                else
                {
                animator.SetInteger("direction", 0);
                }
                right = false;

            }
            else if(mouseScreenPosition.x  > gameObject.transform.position.x)
            {
                flipRight();
                if(Input.GetAxisRaw("Horizontal") == 0)
                {
                animator.SetInteger("direction", 2);
                }
                else
                {
                animator.SetInteger("direction", 1);
                }
                right = true;
            } 
            else
            {
                Torch.GetComponent<Torch>().rotate = false;
            }


            if(Input.GetAxisRaw("Horizontal") > 0){
                legsAnimator.SetInteger("direction",1);
            }

            if(Input.GetAxisRaw("Horizontal") < 0){
                legsAnimator.SetInteger("direction",0);
            }
        }

        if(!IsGrounded() && right)
        {
            legsAnimator.SetInteger("direction", 6);
            animator.SetInteger("direction", 4);
        }

        if(!IsGrounded() && !right)
        {
            legsAnimator.SetInteger("direction", 7);
            animator.SetInteger("direction", 5);
        }

        if(stunned)
        {
            particleTimer += Time.deltaTime;
            if(particleTimer > 0.1f && timeElapsed <= 0.75f){
                Instantiate(particle,this.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-1.8f, 0.5f), 0), Quaternion.identity);
                particleTimer = 0;
            }
            torch.stunned = true;
        
        }

        if(stunned && right)
        {
            animator.SetInteger("direction", 7);
            legsAnimator.SetInteger("direction", 3);
            timeElapsed += Time.deltaTime;
        }

        if(stunned && !right)
        {
            animator.SetInteger("direction", 8);
            legsAnimator.SetInteger("direction", 2);
            timeElapsed += Time.deltaTime;
        }

        if(timeElapsed > 1.2f)
        {
            stunned = false; 
            torch.stunned = false;
            timeElapsed = 0;
        }

        if(IsGrounded() && Input.GetAxisRaw("Horizontal") != 0)
        {
            audioController.PlayWalk();
        }

    }
    void FixedUpdate()
    {
        if(!stunned){
            if(Input.GetAxisRaw("Horizontal") > 0){
                Legs.GetComponent<SpriteRenderer>().sprite = legs_right;
            }

            if(Input.GetAxisRaw("Horizontal") < 0){
                Legs.GetComponent<SpriteRenderer>().sprite = legs_left;
            }
            
            if(!CheckStairs())
            {
                transform.Translate((transform.right*Input.GetAxisRaw("Horizontal")).normalized *speed*Time.deltaTime);        
            }


            if(isJumping )
            {
                audioController.PlayJump();
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        } 
    }
    
    bool  CheckStairs()
    {
       RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.95f, groundLayer);
       if(hit.collider != null)
       {
          
           double dirVal = Input.GetAxisRaw("Horizontal");
           if(hit.collider.gameObject.tag == "StairRight")
           {
              rb.velocity = new Vector2(0,0);
              rb.gravityScale = 0;
              dirVal *= 0.05f;
              transform.Translate(new Vector2(2f * (float)dirVal, 2f * (float)dirVal));
              return true;
           }
           else if(hit.collider.gameObject.tag == "StairLeft")
           {
              rb.velocity = new Vector2(0,0);
              rb.gravityScale = 0;
              dirVal *= 0.05f;
              transform.Translate(new Vector2(2f * (float)dirVal, -2f * (float)dirVal));
              return true;
           }
           else
           {
              rb.gravityScale = 2.5f;
           }
       } 
       else
       {
            rb.gravityScale = 2.5f;     
       }

       return false;
    
    }
    bool IsGrounded()
    {
        Vector2 position = groundPoint.position;
        Vector2 direction = Vector2.down;
        float distance = 0.5f; 

        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if(hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public void flipLeft()
    {
        isFacingRight = !isFacingRight;
        gameObject.GetComponent<SpriteRenderer>().sprite = sandeep_left;
        Torch.GetComponent<SpriteRenderer>().sprite = sandeep_arm_left;
        Torch.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Torch.GetComponent<SpriteRenderer>().flipX = true;
        Torch.transform.localScale = new Vector3(1,-1,1);
        Torch.GetComponent<Torch>().rotate = true;
        Torch.transform.localPosition = new Vector2(0.25f, -0.02f);
        torch.isRight = false;
        collisionController.flip = false;
        if(Input.GetAxisRaw("Horizontal") == 0){
            legsAnimator.SetInteger("direction",2);
        }
    }

    public void flipRight()
    {
        isFacingRight = !isFacingRight;
        gameObject.GetComponent<SpriteRenderer>().sprite = sandeep;
        Torch.GetComponent<SpriteRenderer>().sprite = sandeep_arm;
        Torch.GetComponent<SpriteRenderer>().sortingOrder = -1;
        Torch.GetComponent<SpriteRenderer>().flipX = false;
        Torch.transform.localScale = new Vector3(1,1,1);
        Torch.transform.localPosition = new Vector2(0.193f, -0.09f);
        torch.isRight = true;
        Torch.GetComponent<Torch>().rotate = true;
        collisionController.flip = true;
        if(Input.GetAxisRaw("Horizontal") == 0){
            legsAnimator.SetInteger("direction",3);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
           Platform currentPlatform = collision.gameObject.GetComponentInParent<Platform>();
           currentPlatform.isActive = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
           Platform currentPlatform = collision.gameObject.GetComponentInParent<Platform>();
           currentPlatform.isActive = false;
        }
    }
    


}