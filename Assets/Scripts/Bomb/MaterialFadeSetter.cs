using UnityEngine;

public class MaterialFadeSetter : MonoBehaviour
{
    private const string Mode = "_Mode";
    private const string SrcBlend = "_SrcBlend";
    private const string DstBlend = "_DstBlend";
    private const string ZWrite = "_ZWrite";
    private const string AlphaTestOn = "_ALPHATEST_ON";
    private const string AlphaBlendOn = "_ALPHABLEND_ON";
    private const string AlphaPreMultiplyOn = "_ALPHAPREMULTIPLY_ON";

    private const int ModeIndex = 2;
    private const int ZWriteOff = 0;

    public void Set(Material material)
    {
        material.SetInt(Mode, ModeIndex); 
        material.SetInt(SrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt(DstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt(ZWrite, ZWriteOff);
        material.DisableKeyword(AlphaTestOn);
        material.EnableKeyword(AlphaBlendOn);
        material.DisableKeyword(AlphaPreMultiplyOn);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}
