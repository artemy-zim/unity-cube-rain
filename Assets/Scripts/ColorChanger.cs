using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeToRandom()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void ChangeToDefault()
    {
        _renderer.material.color = _defaultColor;
    }
}
