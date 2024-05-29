using UnityEngine;

public abstract class ColorChanger : MonoBehaviour 
{
    [SerializeField] private Color _defaultColor;

    public abstract void Change(Material material);

    public void SetDefault(Material material)
    {
        material.color = _defaultColor;
    }
}
