using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, ISpawnable
{
    [SerializeField] private CubeColorChanger _colorChanger;

    private Rigidbody _rigidbody;
    private Material _material;
    private bool _isChanged = false;

    public event Action<Cube> OnChanged;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _material = new Material(renderer.material);
        renderer.material = _material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isChanged == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform _))
                OnCollide();
        }
    }

    public void OnSpawn()
    {
        _colorChanger.SetDefault(_material);
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _isChanged = false;
    }

    private void OnCollide()
    {
        _colorChanger.Change(_material);
        _isChanged = true;
        OnChanged?.Invoke(this);
    }
}
