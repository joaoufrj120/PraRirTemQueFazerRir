using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Dictionary<Vector2Int,RoomManager> Neighbors = new Dictionary<Vector2Int, RoomManager>(){
        {new Vector2Int(0,1), null},
        {new Vector2Int(0,-1), null},
        {new Vector2Int(-1,0), null},
        {new Vector2Int(1,0), null}
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
