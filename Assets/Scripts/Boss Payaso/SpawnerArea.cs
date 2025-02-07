using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArea : MonoBehaviour
{

    public Vector3 center;
    public Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.localPosition;
    }

    public List<GameObject> SpawnOnAreaGO(GameObject prefab, int times) //por si necesita referencia del GO spawneado
    {
        List<GameObject> myList = new List<GameObject>();

        for (int i=0; i < times; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.2f, Random.Range(-size.z / 2, size.z / 2));
            GameObject returned = Instantiate(prefab, pos, Quaternion.identity);
            myList.Add(returned);
        }
        return myList;
    }

    public void SpawnOnArea(GameObject prefab, int times) //por si no necesita referencia del GO spawneado
    {
        for (int i = 0; i < times; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.2f, Random.Range(-size.z / 2, size.z / 2));
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    void SpawnWithSpace()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition, size);
    }
}
