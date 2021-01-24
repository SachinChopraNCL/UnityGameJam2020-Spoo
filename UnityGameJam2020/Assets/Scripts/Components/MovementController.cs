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

    public GameObject Torch;
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
       
    }
    
    void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
         {
             isJumping = true;
         }

        if(mouseScreenPosition.x < gameObject.transform.position.x)
        {
            flipLeft();
        }
        else
        {
            flipRight();
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb.AddForce(movement * speed);
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
        Torch.transform.localPosition = new Vector3(0.3f, 0.2f, 0);
        Torch.GetComponent<SpriteRenderer>().flipX = true;
        Torch.transform.localScale = new Vector3(1,-1,1);
    }

    public void flipRight()
    {
        isFacingRight = !isFacingRight;
        gameObject.GetComponent<SpriteRenderer>().sprite = sandeep;
        Torch.GetComponent<SpriteRenderer>().sprite = sandeep_arm;
        Torch.GetComponent<SpriteRenderer>().sortingOrder = -1;
        Torch.transform.localPosition = new Vector3(-0.066f, 0.2f, 0);
        Torch.GetComponent<SpriteRenderer>().flipX = false;
        Torch.transform.localScale = new Vector3(1,1,1);
    }

}