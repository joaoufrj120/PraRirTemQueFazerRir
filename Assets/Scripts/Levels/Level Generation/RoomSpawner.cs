using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private float _xMultiplier = 10f, _yMultiplier = 10f;
    private Transform _parent;
    [SerializeField] private GameObject StartRoom,T,L,R,B, TL,TR,TB,LR,LB,RB, TLR, TLB, TRB, LRB, Cross;

    private Dictionary<Vector2Int,RoomManager> _instantiatedRooms = new Dictionary<Vector2Int, RoomManager>();

    void Start(){
        _parent = new GameObject().transform;
        _parent.name = "Mapa";
    }

    public void SpawnLevel(Dictionary<Vector2Int, LevelGenerator.Room> rooms){
        foreach(var item in rooms){
            Vector2Int position = item.Key;
            LevelGenerator.Room room = item.Value;

            SpawnRoom(position,room);
        }
    }

    public void SpawnRoom(Vector2Int position, LevelGenerator.Room room){
        Vector3 realPosition = new Vector3(position.x * _xMultiplier, position.y * _yMultiplier, 0);


        if(room.Doors[Vector2Int.up]){
            if(room.Doors[Vector2Int.left]){
                if(room.Doors[Vector2Int.right]){
                    if(room.Doors[Vector2Int.down]){
                        InstantiateRoom(Cross, realPosition, room);
                        return;
                    }
                    InstantiateRoom(TLR, realPosition, room);
                    return;
                } else if(room.Doors[Vector2Int.down]){
                   InstantiateRoom(TLB, realPosition, room);
                   return;
                }
                InstantiateRoom(TL, realPosition, room);
                return;
            }     
            if(room.Doors[Vector2Int.right]){
                if(room.Doors[Vector2Int.down]){
                    InstantiateRoom(TRB, realPosition, room);
                    return;
                }
                InstantiateRoom(TR, realPosition, room);
                return;
            }
            if(room.Doors[Vector2Int.down]){
                InstantiateRoom(TB, realPosition, room);
                return;
            }
            InstantiateRoom(T, realPosition, room);
            return;
        }
        if(room.Doors[Vector2Int.left]){
            if(room.Doors[Vector2Int.right]){
                if(room.Doors[Vector2Int.down]){
                    InstantiateRoom(LRB, realPosition, room);
                    return;
                }
                InstantiateRoom(LR, realPosition, room);
                return;
            }
            if(room.Doors[Vector2Int.down]){
                InstantiateRoom(LB, realPosition, room);
                return;
            }
            InstantiateRoom(L, realPosition, room);
            return;
        }
        if(room.Doors[Vector2Int.right]){
            if(room.Doors[Vector2Int.down]){
                InstantiateRoom(RB, realPosition, room);
                return;
            }
            InstantiateRoom(R, realPosition, room);
            return;
        }
        InstantiateRoom(B, realPosition, room);
    }

    void InstantiateRoom(GameObject original, Vector3 position, LevelGenerator.Room room){

        if(room.Type == RoomType.StartRoom && StartRoom != null){
            Instantiate(StartRoom, position, Quaternion.identity,_parent);
            return;
        }
        if(room.Type == RoomType.FinalRoom){
            //criar coisas da sala final
        }
        Instantiate(original, position, Quaternion.identity,_parent);
    }

    void AddConnections(){
        
    }

    public void DestroyRooms(){
        Destroy(_parent.gameObject);
        _parent = new GameObject().transform;
        _parent.name = "Mapa";
    }
}