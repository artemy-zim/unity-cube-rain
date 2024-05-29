using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField, Min(0)] private float _spawnDelay;
    [SerializeField, Min(0)] private float _spawnRate;
    [SerializeField, Min(0)] private float _minDelay;
    [SerializeField, Min(0)] private float _maxDelay;

    public event Action<Vector3> OnCubeReleased;

    private void OnValidate()
    {
        if(_minDelay > _maxDelay)
            _minDelay = _maxDelay;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetPrefab), _spawnDelay, _spawnRate);
    }

    protected override void TrySpawn(Cube cube)
    {
        Vector3 position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;

        cube.transform.position = position;
        cube.gameObject.SetActive(true);
        cube.OnSpawn();

        cube.OnChanged += OnChangedHandler;
    }

    private void OnChangedHandler(Cube cube)
    {
        StartCoroutine(OnReleaseCoroutine(cube));
    }

    private IEnumerator OnReleaseCoroutine(Cube cube)
    {
        yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay)); ;

        cube.OnChanged -= OnChangedHandler;
        Release(cube);

        OnCubeReleased?.Invoke(cube.transform.position);
    }
}
