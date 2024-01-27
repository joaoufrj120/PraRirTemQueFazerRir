using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(1, 3)]
    public int enemyStrenght;
    private float imunityFrames;
    //EnemyStats
    private int life;
    // Start is called before the first frame update
    void Start()
    {
        switch (enemyStrenght)
        {
            case(1):
                life = 1;
                break;
            case (2):
                life = 2;
                break;
            case (3):
                life = 3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(imunityFrames > 0)
        {
            imunityFrames -= Time.deltaTime;
        }
        if (life <= 0){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("X" + collision.gameObject.tag + "Y");
        if (collision.gameObject.CompareTag("Bullet") && imunityFrames <= 0){
            imunityFrames = 0.5f;
            life--;
        }
    }

}
