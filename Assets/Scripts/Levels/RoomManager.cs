using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<Vector2Int,RoomManager> Neighbors = new Dictionary<Vector2Int, RoomManager>(){
        {Vector2Int.up, null},
        {Vector2Int.down, null},
        {Vector2Int.left, null},
        {Vector2Int.right, null}
    };
    
    public void AddNeighbor(Vector2Int direction, RoomManager room){
        Neighbors[direction] = room;
    }

    [ContextMenu("Print neghbors")]
    void PrintNeighbors(){
        foreach(var item in Neighbors){
            Debug.Log(item.Key + " - " + item.Value);
        }
    }
}
