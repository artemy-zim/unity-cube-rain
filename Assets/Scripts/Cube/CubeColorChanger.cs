using UnityEngine;

public class CubeColorChanger : ColorChanger
{
    public override void Change(Material material)
    {
        material.color = Random.ColorHSV();
    }
}
