using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, ISpawnable
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField, Min(0)] private float _vanishMinDelay;
    [SerializeField, Min(0)] private float _vanishMaxDelay;

    private Rigidbody _rigidbody;

    private bool _isChanged = false;

    private void OnValidate()
    {
        if (_vanishMinDelay > _vanishMaxDelay)
            _vanishMinDelay = _vanishMaxDelay;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnSpawn()
    {
        _colorChanger.ChangeToDefault();
        _rigidbody.velocity = Vector3.zero;
        _isChanged = false;
    }

    private void OnCollide()
    {
        float vanishDelay = Random.Range(_vanishMinDelay, _vanishMaxDelay);
        _colorChanger.ChangeToRandom();
        _isChanged = true;

        Invoke(nameof(Vanish), vanishDelay);
    }

    private void Vanish()
    {
        Spawner.Instance.ReleaseCube(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isChanged == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform _))
                OnCollide();
        }
    }
}
