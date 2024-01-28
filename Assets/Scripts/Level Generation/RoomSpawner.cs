using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private float _xMultiplier = 10f, _yMultiplier = 10f;
    private Transform _parent;
    [SerializeField] private GameObject T,L,R,B, TL,TR,TB,LR,LB,RB, TLR, TLB, TRB, LRB, Cross;

    void Start(){
        _parent = new GameObject().transform;
    }
    public void SpawnRoom(Vector2Int position, LevelGenerator.Room room){
        Vector3 realPosition = new Vector3(position.x * _xMultiplier, position.y * _yMultiplier, 0);


        if(room.Doors[LevelGenerator.Directions.Top]){
            if(room.Doors[LevelGenerator.Directions.Left]){
                if(room.Doors[LevelGenerator.Directions.Right]){
                    if(room.Doors[LevelGenerator.Directions.Bottom]){
                        Instantiate(Cross, realPosition, Quaternion.identity,_parent);
                        return;
                    }
                    Instantiate(TLR, realPosition, Quaternion.identity,_parent);
                    return;
                } else if(room.Doors[LevelGenerator.Directions.Bottom]){
                   Instantiate(TLB, realPosition, Quaternion.identity,_parent);
                   return;
                }
                Instantiate(TL, realPosition, Quaternion.identity,_parent);
                return;
            }     
            if(room.Doors[LevelGenerator.Directions.Right]){
                if(room.Doors[LevelGenerator.Directions.Bottom]){
                    Instantiate(TRB, realPosition, Quaternion.identity,_parent);
                    return;
                }
                Instantiate(TR, realPosition, Quaternion.identity,_parent);
                return;
            }
            if(room.Doors[LevelGenerator.Directions.Bottom]){
                Instantiate(TB, realPosition, Quaternion.identity,_parent);
                return;
            }
            Instantiate(T, realPosition, Quaternion.identity,_parent);
            return;
        }
        if(room.Doors[LevelGenerator.Directions.Left]){
            if(room.Doors[LevelGenerator.Directions.Right]){
                if(room.Doors[LevelGenerator.Directions.Bottom]){
                    Instantiate(LRB, realPosition, Quaternion.identity,_parent);
                    return;
                }
                Instantiate(LR, realPosition, Quaternion.identity,_parent);
                return;
            }
            if(room.Doors[LevelGenerator.Directions.Bottom]){
                Instantiate(LB, realPosition, Quaternion.identity,_parent);
                return;
            }
            Instantiate(L, realPosition, Quaternion.identity,_parent);
            return;
        }
        if(room.Doors[LevelGenerator.Directions.Right]){
            if(room.Doors[LevelGenerator.Directions.Bottom]){
                Instantiate(RB, realPosition, Quaternion.identity,_parent);
                return;
            }
            Instantiate(R, realPosition, Quaternion.identity,_parent);
            return;
        }
        Instantiate(B, realPosition, Quaternion.identity,_parent);
    }

    public void DestroyRooms(){
        Destroy(_parent.gameObject);
        _parent = new GameObject().transform;
        //Instantiate(_parent);
    }
}
