using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashTrail : MonoBehaviour
{
    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;
    public float meshDestroyDelay = 3;
    public Transform positionToSpawn;

    [Header("Shader Related")]
    public Material mat;

    bool isTrailActive;
    SkinnedMeshRenderer skinnedMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {

        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderer == null)
            {
                skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            }

            GameObject GO = new GameObject();
            GO.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

            MeshRenderer mr =  GO.AddComponent<MeshRenderer>();
            MeshFilter mf =  GO.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();
            skinnedMeshRenderer.BakeMesh(mesh);
            mf.mesh = mesh;
            mr.material = mat;

            Destroy(GO, meshDestroyDelay);

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }
}
