
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint{
        public Transform SpawnTransform;
        public GameObject Enemy;
    }

    [SerializeField]
    private Dictionary<Vector2Int,RoomManager> Neighbors = new Dictionary<Vector2Int, RoomManager>(){
        {Vector2Int.up, null},
        {Vector2Int.down, null},
        {Vector2Int.left, null},
        {Vector2Int.right, null}
    };

    [SerializeField] private List<Transform> _spawnTransform = new List<Transform>();
    [SerializeField, Range(0,1)] private float _spawnChance = 0.5f;

    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    private void Awake() {
        _enemies.Add(Resources.Load<GameObject>("EnemyMedium"));
        _enemies.Add(Resources.Load<GameObject>("EnemyStrong"));
        _enemies.Add(Resources.Load<GameObject>("EnemyWeak"));
        _enemies.Add(Resources.Load<GameObject>("ShooterEnemyMedium"));
        _enemies.Add(Resources.Load<GameObject>("ShooterEnemyStrong"));
        _enemies.Add(Resources.Load<GameObject>("ShooterEnemyWeak"));
    }


    private void Start() {
        SpawnEnemies();
    }
    
    public void AddNeighbor(Vector2Int direction, RoomManager room){
        Neighbors[direction] = room;
    }

    [ContextMenu("Print neghbors")]
    void PrintNeighbors(){
        foreach(var item in Neighbors){
            Debug.Log(item.Key + " - " + item.Value);
        }
    }

    void SpawnEnemies(){
        foreach(Transform spawn in _spawnTransform){
            //Debug.Log("Spawn 1");
            if(RandomBoolean()){
                //Debug.Log("spawn 2");
                int randIndex = (int)Random.Range(0,_enemies.Count);

                Instantiate(_enemies[randIndex], spawn.position,Quaternion.identity,this.transform);
            }
            //Instantiate(spawn.Enemy,spawn.SpawnTransform.position,Quaternion.identity,this.transform);
        }
    }

    private bool RandomBoolean(){
        if (Random.value < _spawnChance){
            return true;
        }
        return false;
    }
}
