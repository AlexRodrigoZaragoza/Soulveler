using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedTransDyn : MonoBehaviour
{
    private ParedeTransparente paredeTransparenteScript;
    private Renderer renderMaterial = new Renderer();

    // Start is called before the first frame update
    void Start()
    {
        GameObject transp = GameObject.Find("Main Camera");
        paredeTransparenteScript = transp.GetComponent<ParedeTransparente>();

        renderMaterial = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int m = 0; m < renderMaterial.materials.Length; m++)
        {
            if(paredeTransparenteScript.hitpoint.transform == transform)
            {
                Debug.Log("HOLA");

                if(renderMaterial.materials[m].color.a > 0.5f)
                {
                    Debug.Log("HOLA 2");
                    Color cor = renderMaterial.materials[m].color;
                    cor.a -= 0.02f;
                    renderMaterial.materials[m].color = cor;
                }
            }
            else if(renderMaterial.materials[m].color.a < 1)
            {
                Debug.Log("HOLA 3");
                Color cor = renderMaterial.materials[m].color;
                cor.a += 0.02f;
                renderMaterial.materials[m].color = cor;
            }
        }
    }
}
