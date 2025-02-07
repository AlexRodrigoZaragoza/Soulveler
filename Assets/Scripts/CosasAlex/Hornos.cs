using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornos : MonoBehaviour
{
    public List<GameObject> Horno = new List<GameObject>();
    public bool Encender_Hornos;
    List<int> Random_Select = new List<int>();
    int resetFurnaces;
    List<GameObject> activateFurnaces = new List<GameObject>();
    bool HornoCDActivos;
    [SerializeField] Material on, off;
    [SerializeField] GameObject fuegovfx;

    private void Start()
    {
        for (int i = 0; i < Horno.Count; i++)
        {
            Random_Select.Add(i);
        }
    }
    void Update()
    {
        if (Encender_Hornos&&!HornoCDActivos)
        {
            StartCoroutine(EncenderLosHornos());
        }
    }

    IEnumerator EncenderLosHornos()
    {
        HornoCDActivos = true;
        yield return new WaitForSeconds(2);
        resetFurnaces++;
        if (resetFurnaces >= 4) 
        {
            resetFurnaces = 0;
            Random_Select.Clear();
            for (int i = 0; i < Horno.Count; i++)
            {
                Random_Select.Add(i);
            }
        }
        int randPattern = Random.Range(0, Random_Select.Count);
        int positionReset = Random_Select[randPattern];
        Random_Select.Remove(positionReset);
        activateFurnaces.Add(Horno[positionReset]);
        randPattern = Random.Range(0, Random_Select.Count);
        positionReset = Random_Select[randPattern];
        Random_Select.Remove(positionReset);
        activateFurnaces.Add(Horno[positionReset]);
        activateFurnaces[0].GetComponent<Renderer>().material= on;
        GameObject fuego = Instantiate(fuegovfx, activateFurnaces[0].transform.GetChild(0).transform.position, Quaternion.identity);
        activateFurnaces[0].tag = "Hornos";
        activateFurnaces[1].GetComponent<Renderer>().material = on;
        GameObject fuego2 = Instantiate(fuegovfx, activateFurnaces[1].transform.GetChild(0).transform.position, Quaternion.identity);
        activateFurnaces[1].tag = "Hornos";
        yield return new WaitForSeconds(4);
        Destroy(fuego);Destroy(fuego2);
        activateFurnaces[0].GetComponent<Renderer>().material = off;
        activateFurnaces[1].GetComponent<Renderer>().material = off;
        activateFurnaces[1].tag = "Untagged";
        activateFurnaces[1].tag = "Untagged";
        activateFurnaces.Clear();
        HornoCDActivos = false;
    }
}
