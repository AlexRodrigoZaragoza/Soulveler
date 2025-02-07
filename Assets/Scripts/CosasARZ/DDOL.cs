using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    private static DDOL instance;
    public Canvas canv;
    public GameObject player;
    public GameObject gm;
    public GameObject Camera;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        canv.gameObject.SetActive(true);

        if (scene.name == "SampleScene")
        {
            canv.gameObject.SetActive(false);
        }
        else
        {
            gm = GameObject.FindGameObjectWithTag("SpawnPoint");
            player.transform.position = gm.transform.position;

            if (scene.name != "Creditos")
            {
                canv.gameObject.SetActive(true);
                canv.transform.GetChild(0).gameObject.SetActive(true);
                canv.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        if (scene.name == "Creditos")
        {
            canv.gameObject.SetActive(false);
        }
        if (scene.name == "SampleScene"|| scene.name == "Creditos")
        {
            Camera.SetActive(false);
        }
        else { Camera.SetActive(true); }
    }

}
