using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : MonoBehaviour
{
    private Material lineMaterial;

    public bool showMain = true;
    public bool showSub = false;

    public int gridSizeX;
    public int gridSizeY;

    public float startX;
    public float startY;
     public float startZ;

     public float smallStep;
     public float largeStep;

     public Colour mainColour = new Colour(0f, 1f, 0f, 1f);
     public Colour subColour = new Colour(0f, 0.5f, 0f, 1f);



    void CreateLineMaterial ()
    {
        if(!lineMaterial)
        {
            var shader = shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);

            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            //-Turn on alpha blending
            lineMaterial.SetInt("_ScrBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            //-Turn off depth writing
            lineMaterial.SetInt("_ZWrite", 0);

            //-turn off backface culling
            lineMaterial.SetInt("_Cull",(int)UnityEngine.Rendering.CulMode.Off);
        }
    }
    private void OnDisable()
    {
        DestroyImmediate(lineMaterial);
    }
    private void OnPostRender()
    {
        CreateLineMaterial();

        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);

        GL.End();
    }
}
