using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{

    [SerializeField] Animator anim;

    public void playStartButton()
    {
        anim.SetTrigger("StartGame");
        Invoke("StartGame", 4);
    }

    void StartGame()
    {
        SceneLoader.Instance.LoadScene("Historieta");
    }
}
