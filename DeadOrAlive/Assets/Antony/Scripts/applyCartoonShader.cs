using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyCartoonShader : MonoBehaviour
{
    public Shader cartoonShader;

    void Start()
    {
        if (cartoonShader != null)
        {
            Camera.main.SetReplacementShader(cartoonShader, "RenderType");
        }
        else
        {
            Debug.LogError("Cartoon shader not assigned.");
        }
    }

    void OnDisable()
    {
        Camera.main.ResetReplacementShader();
    }
}
