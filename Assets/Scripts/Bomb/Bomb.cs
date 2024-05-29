using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour, ISpawnable
{
    [SerializeField] private MaterialFadeSetter _fadeSetter;
    [SerializeField] private BombColorChanger _colorChanger;
    [SerializeField] private Explosion _explosion;

    private Rigidbody _rigidbody;
    private Material _material;

    public event Action<Bomb> OnExploded;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _material = new Material(renderer.material);
        _fadeSetter.Set(_material);
        renderer.material = _material;
    }

    private void OnEnable()
    {
        _colorChanger.OnAlphaChanged += Explode;
    }

    private void OnDisable()
    {
        _colorChanger.OnAlphaChanged -= Explode;    
    }

    public void OnSpawn()
    {
        _colorChanger.Change(_material);
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private void Explode()
    {
        _explosion.Explode(_rigidbody);

        OnExploded?.Invoke(this);
    }
}
