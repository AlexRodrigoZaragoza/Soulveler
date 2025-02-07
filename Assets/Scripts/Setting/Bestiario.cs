using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bestiario : MonoBehaviour
{


    [System.Serializable]
    public class Bestia
    {
        public Sprite image;
        public string name;
        public string description;
        public int timesDefeated;
    }

    [SerializeField] List<Bestia> enemigos = new List<Bestia>();

    [SerializeField] Image Enemigo;
    [SerializeField] Text nombre, descripcion, derrotado;
    [SerializeField] Sprite noHayEnemigo;

    int currentEnemy;

    public void GetKill(string name)   //PARA JORGE: ESCRIBE BESTIARIO.INSTANCE.GETKILL(NOMBREDELENEMIGO) PARA LA MUERTE DEL ENEMIGO.
    {
        foreach (Bestia enemigo in enemigos)
        {
            if (enemigo.name == name)
            {
                enemigo.timesDefeated++;
            }
        }
    }

    public void UpdatearLista()
    {
        Enemigo.sprite = enemigos[currentEnemy].image;
        nombre.text = enemigos[currentEnemy].name;
        descripcion.text = enemigos[currentEnemy].description;

        if (enemigos[currentEnemy].name == "ANDRES")
        {
            derrotado.text = "Protegido por Maranon";
        }
        else
        {
            if (enemigos[currentEnemy].timesDefeated > 0)
            {
                derrotado.text = "Nº MUERTES: " + enemigos[currentEnemy].timesDefeated;

            }
            else
            {
                if (enemigos[currentEnemy].name == "CASPIN")
                {
                    derrotado.text = "   ---   ";
                }
                else
                {
                    Enemigo.sprite = noHayEnemigo;
                    nombre.text = "SIN DESCUBRIR";
                    descripcion.text = "?";
                    derrotado.text = "?";
                }
            }
        }
    }

    public void NextEnemy()
    {
        currentEnemy++;

        if (currentEnemy >= enemigos.Count)
        {
            currentEnemy = 0;
        }
        UpdatearLista();
    }
    public void PreviousEnemy()
    {
        currentEnemy--;
        if (currentEnemy < 0)
        {
            currentEnemy = enemigos.Count - 1;
        }
        UpdatearLista();
    }
}
