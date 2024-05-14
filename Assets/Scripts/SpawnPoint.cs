using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void TrySpawn(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out ISpawnable spawnable))
        {
            gameObject.transform.position = transform.position;
            gameObject.SetActive(true);

            spawnable.OnSpawn();
        }
    }
}
