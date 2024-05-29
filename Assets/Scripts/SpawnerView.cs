using TMPro;
using UnityEngine;

public abstract class SpawnerView<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] protected TextMeshProUGUI CreatedText;
    [SerializeField] protected TextMeshProUGUI ActiveText;
    [SerializeField] private Spawner<T> _spawner;

    private void OnEnable()
    {
        _spawner.OnActiveChanged += UpdateActive;
        _spawner.OnCreatedChanged += UpdateCreated;
    }

    private void OnDisable()
    {
        _spawner.OnActiveChanged -= UpdateActive;
        _spawner.OnCreatedChanged -= UpdateCreated;
    }

    protected abstract void UpdateCreated(int count);

    protected abstract void UpdateActive(int count);
}
