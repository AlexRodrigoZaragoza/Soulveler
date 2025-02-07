using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaNoDestruir : MonoBehaviour
{
    public static MusicaNoDestruir instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TimerDuel")
        {
            GetComponent<AudioSource>().pitch = 1.20f;
        }
        else if (SceneManager.GetActiveScene().name == "FinalBoss")
        {
            GetComponent<AudioSource>().pitch = -0.2f;
        }
        else if (SceneManager.GetActiveScene().name != "TimerDuel" || SceneManager.GetActiveScene().name != "FinalBoss")
        {
            GetComponent<AudioSource>().pitch = 1.0f;
        }
    }
}
