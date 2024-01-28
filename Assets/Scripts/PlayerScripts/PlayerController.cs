using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D player;
    public float speed, timerDamage;

    Animator anim;
    private Vector2 lastMoveDirection, stopped;

    private float imunityFrames;
    private float hungerTimer = 300f;

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

        if (imunityFrames > 0)
        {
            imunityFrames -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(hungerTimer > 0)
        {
            hungerTimer -= Time.deltaTime;
            Debug.Log(hungerTimer);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy")) && imunityFrames <= 0)
        {
            imunityFrames = 0.5f;
            hungerTimer -= timerDamage;
            
        }
    }

}
