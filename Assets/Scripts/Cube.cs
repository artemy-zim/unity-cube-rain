using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Renderer))]
public class Cube : MonoBehaviour, ISpawnable 
{
    [SerializeField] private Color _defaultColor;
    [SerializeField, Min(0)] private float _vanishMinDelay;
    [SerializeField, Min(0)] private float _vanishMaxDelay;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private bool _isChanged = false;

    private void OnValidate()
    {
        if(_vanishMinDelay > _vanishMaxDelay)
            _vanishMinDelay = _vanishMaxDelay;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void OnSpawn()
    {
        _renderer.material.color = _defaultColor;
        _rigidbody.velocity = Vector3.zero;
        _isChanged = false;
    }

    public void OnCollide()
    {
        if(_isChanged == false)
        {
            float vanishDelay = Random.Range(_vanishMinDelay, _vanishMaxDelay);
            _renderer.material.color = Random.ColorHSV();
            _isChanged = true;

            Invoke(nameof(Vanish), vanishDelay);
        }
    }

    private void Vanish()
    {
        Spawner.Instance.ReleaseObject(gameObject);
    }
}
