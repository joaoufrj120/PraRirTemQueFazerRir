using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public int teleporterSide;
    private GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch (teleporterSide)
            {
                case (0):
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 7f, player.transform.position.z);
                    break;
                case (1):
                    player.transform.position = new Vector3(player.transform.position.x + 7f, player.transform.position.y, player.transform.position.z);
                    break;
                case (2):
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 7f, player.transform.position.z);
                    break;
                case (3):
                    player.transform.position = new Vector3(player.transform.position.x - 7f, player.transform.position.y, player.transform.position.z);
                    break;
            }
        }
    }
}
