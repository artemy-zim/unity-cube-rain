using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private T _spawnable;
    [SerializeField, Min(0)] private int _poolSize;
    [SerializeField, Min(0)] private int _poolMaxSize;

    private ObjectPool<T> _pool;
    private int _createdCount;
    private int _activeCount;

    public event Action<int> OnCreatedChanged;
    public event Action<int> OnActiveChanged;

    private void OnValidate()
    {
        if (_poolSize > _poolMaxSize)
            _poolSize = _poolMaxSize;
    }

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_spawnable),
            actionOnGet: (spawnable) => TrySpawn(spawnable),
            actionOnRelease: (spawnable) => spawnable.gameObject.SetActive(false),
            actionOnDestroy: (spawnable) => Destroy(spawnable),
            collectionCheck: true,
            defaultCapacity: _poolSize,
            maxSize: _poolMaxSize
            );

        OnCreatedChanged?.Invoke(_createdCount);
        OnActiveChanged?.Invoke(_activeCount);
    }

    protected void GetPrefab()
    {
        _pool.Get();

        _createdCount++;
        OnCreatedChanged?.Invoke(_createdCount);

        _activeCount++;
        OnActiveChanged?.Invoke(_activeCount);
    }

    protected void Release(T spawnable)
    {
        _pool.Release(spawnable);

        _activeCount--;
        OnActiveChanged?.Invoke(_activeCount);
    }

    protected abstract void TrySpawn(T spawnable);
}

