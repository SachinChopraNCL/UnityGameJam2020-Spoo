using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class MovementController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public  Animator animator;
    public CollisionController collisionController;

    public GameObject Torch;
    public GameObject Legs;

    public Sprite legs_right;
    public Sprite legs_left;
    public Sprite sandeep;
    public Sprite sandeep_left;
    public Sprite sandeep_arm;
    public Sprite sandeep_arm_left;

    public Transform LightPoint;
    public bool isJumping = false;
    private bool isFacingRight;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        collisionController = GetComponent<CollisionController>();
    }
    
    void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
         {
             isJumping = true;
         }

        if(mouseScreenPosition.x  < gameObject.transform.position.x)
        {
            flipLeft();
        }
        else if(mouseScreenPosition.x  > gameObject.transform.position.x)
        {
            flipRight();
        } 
        else
        {
            Torch.GetComponent<Torch>().rotate = false;
        }
    }
    void FixedUpdate()
    {
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
    
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.2f; 
        
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
        collisionController.flip = false;
    }

    public void flipRight()
    {
        isFacingRight = !isFacingRight;
        gameObject.GetComponent<SpriteRenderer>().sprite = sandeep;
        Torch.GetComponent<SpriteRenderer>().sprite = sandeep_arm;
        Torch.GetComponent<SpriteRenderer>().sortingOrder = -1;
        Torch.GetComponent<SpriteRenderer>().flipX = false;
        Torch.transform.localScale = new Vector3(1,1,1);
        Torch.GetComponent<Torch>().rotate = true;
        collisionController.flip = true;
    }
}