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

    public Animator legsAnimator;
    public CollisionController collisionController;

    public GameObject Torch;
    public Torch torch;
    public GameObject Legs;
    public GameObject particle;
    public Sprite legs_right;
    public Sprite legs_left;
    public Sprite sandeep;
    public Sprite sandeep_left;
    public Sprite sandeep_arm;
    public Sprite sandeep_arm_left;
    public Transform LightPoint;
    public bool isJumping = false;
    public  bool stunned = false;

    public bool createParticle = false;

    float particleTimer = 0;
    private bool right = true;
    private bool isFacingRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        collisionController = GetComponent<CollisionController>();
        torch = Torch.GetComponent<Torch>();
    }
    
    void Update()
    {
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
                Instantiate(particle, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-1.8f, 0.5f), 0), Quaternion.identity);
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
        
            transform.Translate((transform.right*Input.GetAxisRaw("Horizontal")).normalized *speed*Time.deltaTime);        
            if(isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
    }
    
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.6f; 
        
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
}