using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField, Min(0)] private float _spawnRate;
    [SerializeField, Min(0)] private float _spawnDelay;

    [SerializeField, Min(0)] private int _poolSize;
    [SerializeField, Min(0)] private int _poolMaxSize;

    private ObjectPool<GameObject> _pool;

    public static Spawner Instance { get; private set; }

    private void OnValidate()
    {
        if (_poolSize > _poolMaxSize)
            _poolSize = _poolMaxSize;
    }

    private void Awake()
    {
        Instance = this;

        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => _spawnPoints[Random.Range(0, _spawnPoints.Length)].TrySpawn(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolSize,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), _spawnDelay, _spawnRate);
    }

    private void GetObject()
    {
        _pool.Get();
    }

    public void ReleaseObject(GameObject gameObject)
    {
        _pool.Release(gameObject);
    }
}
