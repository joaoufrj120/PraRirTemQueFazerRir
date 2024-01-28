using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Dictionary<Vector2Int,RoomManager> Neighbors = new Dictionary<Vector2Int, RoomManager>(){
        {Vector2Int.up, null},
        {Vector2Int.down, null},
        {Vector2Int.left, null},
        {Vector2Int.right, null}
    };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
