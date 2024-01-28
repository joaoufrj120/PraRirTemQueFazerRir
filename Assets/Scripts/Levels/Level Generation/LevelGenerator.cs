using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RoomType{
    StartRoom,
    EnemyRoom,
    FinalRoom
}

public class LevelGenerator : MonoBehaviour
{
    #region Classes
    
    public class Room {
        public Dictionary<Vector2Int,bool> Doors = new Dictionary<Vector2Int, bool>(){
            {Vector2Int.up, false},
            {Vector2Int.down, false},
            {Vector2Int.left, false},
            {Vector2Int.right, false}
        };

        //<direção,posição>
        public Dictionary<Vector2Int,Vector2Int> Neighbors = new Dictionary<Vector2Int, Vector2Int>();

        public RoomType Type;

        public Room(RoomType _type = RoomType.EnemyRoom){
            Type = _type;
        }
    }
    #endregion

    #region  Exposed Variables
    [SerializeField] private int _maxRoomsApprox = 15;
    [SerializeField] private int _minRooms = 5;
    [SerializeField] private RoomSpawner _roomSpawner;
    [SerializeField] private int _seed = 70;
    [SerializeField] private bool _useSeed;
    #endregion

    private int _dictionaryIndex;
    private Dictionary<Vector2Int, Room> _mapRooms = new Dictionary<Vector2Int, Room>();


    private void Start() {
        Generate();
    }


    [ContextMenu("Generate")]
    private void Generate(){
        
        if(_useSeed){
            Random.InitState(_seed);
        } else {
            Random.InitState((int)System.DateTime.Now.Ticks);
        }

        _mapRooms.Clear();
        _roomSpawner.DestroyRooms();

        //Setup da primeira sala
        Room startRoom = new Room(RoomType.StartRoom);
        startRoom.Doors[Vector2Int.down] = true;
        _mapRooms.Add(Vector2Int.zero, startRoom);

        _dictionaryIndex = 0;//Reset do index

        while(_mapRooms.Count < _maxRoomsApprox) {

            DictionaryIteration();

            //Parar loop
            if(_dictionaryIndex >= _mapRooms.Count){
                Debug.Log("1");
                break;
            }
            //Executa mais uma iteração para fechar as salas
            if(!(_mapRooms.Count < _maxRoomsApprox)){
                DictionaryIteration();
            }
        }

        if(!IsBigEnough() && !_useSeed){
            Generate();
            return;
        }

        FindFinalRoom();

        _roomSpawner.SpawnLevel(_mapRooms);
    }

    void DictionaryIteration(){
        for(int i = _dictionaryIndex, count = _mapRooms.Count; i < count; i++){
            //Debug.Log("j:" + j + ". count: " + count + ". map.rooms.count: " + _mapRooms.Count + ". dicI: " + _dictionaryIndex);
            var item = _mapRooms.ElementAt(i);
            Room room = item.Value;
            Vector2Int roomPos = item.Key;

            if(room.Doors[Vector2Int.up] && !_mapRooms.ContainsKey(roomPos + Vector2Int.up)){
                CreateRoom(roomPos + Vector2Int.up);
            }
            if(room.Doors[Vector2Int.down] && !_mapRooms.ContainsKey(roomPos + Vector2Int.down)){
                CreateRoom(roomPos + Vector2Int.down);
            }
            if(room.Doors[Vector2Int.left] && !_mapRooms.ContainsKey(roomPos + Vector2Int.left)){
                CreateRoom(roomPos + Vector2Int.left);
            }
            if(room.Doors[Vector2Int.right] && !_mapRooms.ContainsKey(roomPos + Vector2Int.right)){
                CreateRoom(roomPos + Vector2Int.right);
            }
            _dictionaryIndex++;
        }
    }

    private void CreateRoom(Vector2Int position){
        Room newRoom = new Room();

        newRoom.Doors[Vector2Int.up] = AssignDoor(position, Vector2Int.up);
        newRoom.Doors[Vector2Int.down] = AssignDoor(position, Vector2Int.down);
        newRoom.Doors[Vector2Int.left] = AssignDoor(position, Vector2Int.left);
        newRoom.Doors[Vector2Int.right] = AssignDoor(position, Vector2Int.right);

        _mapRooms.Add(position,newRoom);

        //_roomSpawner.SpawnRoom(position,newRoom);
        

        //Debug.Log("Room: " + _mapRooms.Count + ", pos: " + position + ". Top: " + newRoom.Doors[Directions.Top] + ", Bottom: " + newRoom.Doors[Directions.Bottom] + ", Left: " + newRoom.Doors[Directions.Left] + ", Right: "+ newRoom.Doors[Directions.Right]);
    }

    private bool AssignDoor(Vector2Int originalPosition, Vector2Int direction){
        Vector2Int targetPosition = originalPosition + direction;

        if(_mapRooms.TryGetValue(targetPosition, out Room neighbor)){
            if(neighbor.Doors[-direction]){
                ConnectRooms(originalPosition,direction,targetPosition);
                return true;
            }
            return false;
        }

        if(targetPosition.y > -1){
            return false;
        }

        int roomCount = _mapRooms.Count;

        if(_dictionaryIndex >= roomCount){
            return false;
        }

        if(roomCount >= _maxRoomsApprox){
            return false;
        }

        return RandomBoolean();
    }

    void ConnectRooms(Vector2Int position1, Vector2Int direction, Vector2Int position2){
        //conectar salas
        Room room2 = _mapRooms[position2];
        if(room2.Neighbors.ContainsKey(-direction)){
            _mapRooms[position1].Neighbors.Add(direction,position2);
            _mapRooms[position2].Neighbors.Add(-direction,position1);
        }
    }

    void FindFinalRoom(){
        for(int i = _mapRooms.Count-1; i >= 0; i--){
            var item = _mapRooms.ElementAt(i);
            Room room = item.Value;

            int doorCount = room.Doors.Count(doors => doors.Value == true);

            if(doorCount == 1 && !room.Doors[Vector2Int.down]){
                //Debug.Log(item.Key);

                _mapRooms.ElementAt(i).Value.Type = RoomType.FinalRoom;
                break;
            }
        }
    }

    bool IsBigEnough(){
        return _mapRooms.Count >= _minRooms;
    }

    private bool RandomBoolean(){
        if (Random.value >= 0.5){
            return true;
        }
        return false;
    }
}
