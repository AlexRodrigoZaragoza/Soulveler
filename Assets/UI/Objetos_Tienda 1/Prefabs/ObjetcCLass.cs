using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Objeto/objeto")]
public class ObjetcCLass : ScriptableObject

{
    public int i;
    public bool ObjetoSeleccionado;

    public GameObject Objto;
    public Collider Col;
    public Transform trans;
}
