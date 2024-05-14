using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField, Min(0)] private float _spawnRate;
    [SerializeField, Min(0)] private float _spawnDelay;

    [SerializeField, Min(0)] private int _poolSize;
    [SerializeField, Min(0)] private int _poolMaxSize;

    private ObjectPool<Cube> _cubePool;

    public static Spawner Instance { get; private set; }

    private void OnValidate()
    {
        if (_poolSize > _poolMaxSize)
            _poolSize = _poolMaxSize;
    }

    private void Awake()
    {
        Instance = this;

        _cubePool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (cube) => TrySpawn(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolSize,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), _spawnDelay, _spawnRate);
    }
    public void ReleaseCube(Cube cube)
    {
        _cubePool.Release(cube);
    }

    private void GetCube()
    {
        _cubePool.Get();
    }

    private void TrySpawn(Cube cube)
    {
        Vector3 position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;

        cube.transform.position = position;
        cube.gameObject.SetActive(true);

        cube.OnSpawn();
    }
}

