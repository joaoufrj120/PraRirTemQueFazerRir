using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyScript : MonoBehaviour
{
    public GameObject ps;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Buraco")
        {
            Destroy(this.gameObject);
            GameObject explosion = Instantiate(ps, transform.position, transform.rotation);
            explosion.tag = "Effect";
        }
    }
}
