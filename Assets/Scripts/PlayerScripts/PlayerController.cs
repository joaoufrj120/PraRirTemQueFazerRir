using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D player;
    public float speed;

    Animator anim;
    private Vector2 lastMoveDirection, stopped;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        stopped = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.velocity = movement.normalized * speed;
        if(movement != stopped)
        {
            lastMoveDirection = movement;
        }
        anim.SetFloat("xSpeed", movement.x);
        anim.SetFloat("ySpeed", movement.y);
        anim.SetFloat("MoveMagnitude", movement.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }
    
}
