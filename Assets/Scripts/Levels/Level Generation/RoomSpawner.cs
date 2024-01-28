using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private float _xMultiplier = 10f, _yMultiplier = 10f;
    private Transform _parent;
    [SerializeField] private RoomManager StartRoom,T,L,R,B, TL,TR,TB,LR,LB,RB, TLR, TLB, TRB, LRB, Cross;
    [SerializeField] private GameObject _endPortal;

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

        AddConnections(rooms);
    }

    public void SpawnRoom(Vector2Int position, LevelGenerator.Room room){

        if(room.Doors[Vector2Int.up]){
            if(room.Doors[Vector2Int.left]){
                if(room.Doors[Vector2Int.right]){
                    if(room.Doors[Vector2Int.down]){
                        InstantiateRoom(Cross, position, room);
                        return;
                    }
                    InstantiateRoom(TLR, position, room);
                    return;
                } else if(room.Doors[Vector2Int.down]){
                   InstantiateRoom(TLB, position, room);
                   return;
                }
                InstantiateRoom(TL, position, room);
                return;
            }     
            if(room.Doors[Vector2Int.right]){
                if(room.Doors[Vector2Int.down]){
                    InstantiateRoom(TRB, position, room);
                    return;
                }
                InstantiateRoom(TR, position, room);
                return;
            }
            if(room.Doors[Vector2Int.down]){
                InstantiateRoom(TB, position, room);
                return;
            }
            InstantiateRoom(T, position, room);
            return;
        }
        if(room.Doors[Vector2Int.left]){
            if(room.Doors[Vector2Int.right]){
                if(room.Doors[Vector2Int.down]){
                    InstantiateRoom(LRB, position, room);
                    return;
                }
                InstantiateRoom(LR, position, room);
                return;
            }
            if(room.Doors[Vector2Int.down]){
                InstantiateRoom(LB, position, room);
                return;
            }
            InstantiateRoom(L, position, room);
            return;
        }
        if(room.Doors[Vector2Int.right]){
            if(room.Doors[Vector2Int.down]){
                InstantiateRoom(RB, position, room);
                return;
            }
            InstantiateRoom(R, position, room);
            return;
        }
        InstantiateRoom(B, position, room);
    }

    void InstantiateRoom(RoomManager original, Vector2Int position, LevelGenerator.Room room){
        Vector3 realPosition = new Vector3(position.x * _xMultiplier, position.y * _yMultiplier, 0);
        RoomManager realRoom;

        if(room.Type == RoomType.StartRoom && StartRoom != null){
            realRoom = Instantiate(StartRoom, realPosition, Quaternion.identity,_parent);
            _instantiatedRooms.Add(position,realRoom);
            return;
        }
        realRoom = Instantiate(original, realPosition, Quaternion.identity,_parent);
        _instantiatedRooms.Add(position,realRoom);

        if(room.Type == RoomType.FinalRoom){
            //criar coisas da sala final
            Vector3 middle = new Vector3(-2,5,0) + realPosition;
            Instantiate(_endPortal,middle,Quaternion.identity, realRoom.transform);
        }
    }

    void AddConnections(Dictionary<Vector2Int, LevelGenerator.Room> rooms){
        foreach(var item in _instantiatedRooms){
            //Debug.Log("aaaa");
            RoomManager roomManager = item.Value;
            Vector2Int position = item.Key;

            foreach(var neighbor in rooms[position].Neighbors){
                //Debug.Log("1");
                Vector2Int direction = neighbor.Key;
                RoomManager realNeighbor = _instantiatedRooms[neighbor.Value];


                roomManager.AddNeighbor(neighbor.Key,realNeighbor);
            }
            
        }
    }

    public void DestroyRooms(){
        Destroy(_parent.gameObject);
        _parent = new GameObject().transform;
        _parent.name = "Mapa";
    }
}
