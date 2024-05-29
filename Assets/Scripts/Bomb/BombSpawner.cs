using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _nextPosition;

    private void OnEnable()
    {
        _cubeSpawner.OnCubeReleased += OnCubeReleasedHandler;
    }

    private void OnDisable()
    {
        _cubeSpawner.OnCubeReleased -= OnCubeReleasedHandler;
    }

    protected override void TrySpawn(Bomb bomb)
    {
        bomb.transform.position = _nextPosition;
        bomb.gameObject.SetActive(true);
        bomb.OnSpawn();

        bomb.OnExploded += OnExplodedHandler;
    }

    private void OnCubeReleasedHandler(Vector3 cubePosition)
    {
        _nextPosition = cubePosition;
        GetPrefab();
    }

    private void OnExplodedHandler(Bomb bomb)
    {
        bomb.OnExploded -= OnExplodedHandler;
        Release(bomb);
    }
}
