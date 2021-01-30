using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Enemy : MonoBehaviour
{
    private Animator animator;
    public float health;
    public GameObject player;
    bool inStart = true;
    private MovementController movementController;
    private Transform playerPosition;
    public Vector3 startingPosition;
    public SpriteRenderer spriteRenderer;
    public Light2D light;
    public AIDestinationSetter destination;
    void Start()
    {
      player = GameObject.Find("Sandeep");
      animator = GetComponent<Animator>();
      destination = GetComponent<AIDestinationSetter>();
      movementController = player.GetComponent<MovementController>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      GetComponent<AIPath>().maxSpeed = 0;
      destination.target = player.transform;
      playerPosition = player.transform;
      startingPosition = transform.position;
      animator.Play("Appear");
    }

    void Update()
    {
        GetComponent<AIPath>().maxSpeed = 3;
        
        if(inStart)
        {
            GetComponent<AIPath>().maxSpeed = 0;
        }

  
        if(transform.position.x < playerPosition.transform.position.x)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if(transform.position.x > playerPosition.transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }

        if(health <= 0)
        {
            animator.Play("Death");
        }
        else if(Vector3.Distance(playerPosition.position, this.transform.position) < 0.35f)
        {
            inStart = true;
            movementController.stunned = true;
            animator.Play("Attack");
        }

    

    }

    public void Set()
    {
        animator.Play("Default");
        inStart = false;
    }

    public void Reset()
    {
        animator.Play("Default");
        inStart = false;
    }

    public void Stun()
    {
        movementController.createParticle = true;
    }

    public void Attack()
    {
        spriteRenderer.enabled = true;
        light.enabled = true;
        this.transform.position = startingPosition;
        inStart = true;
        animator.Play("Reappear");
    }
    public void Die(){
       Destroy (gameObject); 
    }

    public void Disappear()
    {
        spriteRenderer.enabled = false;
        light.enabled = false;
    }

}

