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
    public class DefaultDirections{
        public readonly Vector2Int Top, Bottom, Left, Right;

        public DefaultDirections(){
            Top = new Vector2Int(0,1);
            Bottom = new Vector2Int(0,-1);
            Left = new Vector2Int(-1,0);
            Right = new Vector2Int(1,0);
        }
    }
    public class Room {
        public Dictionary<Vector2Int,bool> Doors = new Dictionary<Vector2Int, bool>(){
            {new Vector2Int(0,1), false},
            {new Vector2Int(0,-1), false},
            {new Vector2Int(-1,0), false},
            {new Vector2Int(1,0), false}
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

    public static DefaultDirections Directions = new DefaultDirections();
    private int _dictionaryIndex;
    private Dictionary<Vector2Int, Room> _mapRooms = new Dictionary<Vector2Int, Room>();


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
        startRoom.Doors[Directions.Bottom] = true;
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

            if(room.Doors[Directions.Top] && !_mapRooms.ContainsKey(roomPos + Directions.Top)){
                CreateRoom(roomPos + Directions.Top);
            }
            if(room.Doors[Directions.Bottom] && !_mapRooms.ContainsKey(roomPos + Directions.Bottom)){
                CreateRoom(roomPos + Directions.Bottom);
            }
            if(room.Doors[Directions.Left] && !_mapRooms.ContainsKey(roomPos + Directions.Left)){
                CreateRoom(roomPos + Directions.Left);
            }
            if(room.Doors[Directions.Right] && !_mapRooms.ContainsKey(roomPos + Directions.Right)){
                CreateRoom(roomPos + Directions.Right);
            }
            _dictionaryIndex++;
        }
    }

    private void CreateRoom(Vector2Int position){
        Room newRoom = new Room();

        newRoom.Doors[Directions.Top] = AssignDoor(position, Directions.Top);
        newRoom.Doors[Directions.Bottom] = AssignDoor(position, Directions.Bottom);
        newRoom.Doors[Directions.Left] = AssignDoor(position, Directions.Left);
        newRoom.Doors[Directions.Right] = AssignDoor(position, Directions.Right);

        _mapRooms.Add(position,newRoom);

        //_roomSpawner.SpawnRoom(position,newRoom);
        

        //Debug.Log("Room: " + _mapRooms.Count + ", pos: " + position + ". Top: " + newRoom.Doors[Directions.Top] + ", Bottom: " + newRoom.Doors[Directions.Bottom] + ", Left: " + newRoom.Doors[Directions.Left] + ", Right: "+ newRoom.Doors[Directions.Right]);
    }

    private bool AssignDoor(Vector2Int originalPosition, Vector2Int direction){
        Vector2Int targetPosition = originalPosition + direction;

        if(_mapRooms.TryGetValue(targetPosition, out Room neighbor)){
            ConnectRooms(originalPosition,direction,targetPosition);
            if(neighbor.Doors[-direction]){
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
    }

    void FindFinalRoom(){
        for(int i = _mapRooms.Count-1; i >= 0; i--){
            var item = _mapRooms.ElementAt(i);
            Room room = item.Value;

            int doorCount = room.Doors.Count(doors => doors.Value == true);

            if(doorCount == 1 && !room.Doors[Directions.Bottom]){
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
