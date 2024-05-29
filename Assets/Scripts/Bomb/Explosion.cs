using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _baseForce;
    [SerializeField] private float _radius;

    public void Explode(Rigidbody explodableRigidbody)
    {
        Collider[] colliders = Physics.OverlapSphere(explodableRigidbody.position, _radius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                if (rigidbody != explodableRigidbody)
                {
                    float force = GetExplosionForce(rigidbody.position, explodableRigidbody.position);

                    rigidbody.AddExplosionForce(force, explodableRigidbody.position, _radius);
                }
            }
        }
    }

    private float GetExplosionForce(Vector3 objectPosition, Vector3 explosionPosition)
    {
        float distance = (explosionPosition - objectPosition).magnitude;

        return _baseForce / distance;
    }
}
