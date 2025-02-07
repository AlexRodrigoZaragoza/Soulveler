using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OpenSettings : MonoBehaviour
{
    [SerializeField] GameObject player;
    GameObject otherToClose;
    [SerializeField] bool onlyPlayerStops;
    [SerializeField] Image ArmaEspectral;
    [SerializeField] List<Sprite> armasEspectrales = new List<Sprite>();

    [SerializeField] GameObject generalPause;
    
    #region settings
    public bool settingsOpen = false;

    public GameObject settingsCanvas;
    [SerializeField] GameObject soundCanvas, graphicsCanvas;
    #endregion

    [SerializeField] GameObject controls;
    [SerializeField] GameObject confirmBase;
    [SerializeField] GameObject confirmExit;
    [SerializeField] GameObject deathCanvas;
    [SerializeField] GameObject bestiarioCanvas;

    [SerializeField] TestAttack TEAtck;
    [SerializeField] PlayerGM PGM;
    [SerializeField] GameManager GM;

    [SerializeField] List<GameObject> otrosCanvas = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        BacktoBase();
        generalPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RemoveCurrent();
        }
    }

    public void ChangeToBestiario()
    {
        GetComponent<Bestiario>().UpdatearLista();
        bestiarioCanvas.SetActive(true);
        
    }

    public void ChangeToPause()
    {
        bestiarioCanvas.SetActive(false);
    }

    public void RemoveCurrent()
    {
        if (!generalPause.activeSelf)
        {
            Pause();
        }
        else if (!OtherOpen())
        {
            Resume();
        }
        else
        {
            otherToClose.SetActive(false);
            otherToClose = null;
        }
    }

    bool OtherOpen()
    {
        if (settingsCanvas.activeSelf)
        {
            if (soundCanvas.activeSelf )
            {
                otherToClose = soundCanvas;
            }
            else if (graphicsCanvas.activeSelf)
            {
                otherToClose = graphicsCanvas;
                
            }
            else
            {
                otherToClose = settingsCanvas;
            }
            return true;
        }
        else if (confirmExit.activeSelf)
        {
            otherToClose = confirmExit;
            return true;
        }
        else if (controls.activeSelf)
        {
            otherToClose = controls;
            return true;
        }
        else if (confirmBase.activeSelf)
        {
            otherToClose = confirmBase;
            return true;
        }
        else if (confirmExit.activeSelf)
        {
            otherToClose = confirmExit;
            return true;
        }
        else if (bestiarioCanvas.activeSelf)
        {
            bestiarioCanvas.SetActive(false);
            return false;
        }
        return false;
    }

    void Pause()
    {

        if (PlayerGM.armas == 1)
        {
            ArmaEspectral.enabled = true;
            ArmaEspectral.sprite = armasEspectrales[0];
        }
        else if (PlayerGM.armas == 2)
        {
            ArmaEspectral.enabled = true;
            ArmaEspectral.sprite = armasEspectrales[1];
        }
        else if (PlayerGM.armas == 3)
        {
            ArmaEspectral.enabled = true;
            ArmaEspectral.sprite = armasEspectrales[2];
        }
        else
        {
            ArmaEspectral.enabled = false;
        }

        if (onlyPlayerStops)
        {
            player.GetComponent<TestingInputSystem>().enabled = false;
            player.GetComponent<PlayerInput>().enabled = false;
        }
        else
        {
            Time.timeScale = 0;
        }

        generalPause.SetActive(true);
    }
    public void Resume()
    {
        if (onlyPlayerStops)
        {
            player.GetComponent<TestingInputSystem>().enabled = true;
            player.GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            Time.timeScale = 1;
        }

        generalPause.SetActive(false);
    }
    public void Settings()
    {
        settingsCanvas.SetActive(true);
    }

    public void ShowControls()
    {
        controls.SetActive(true);
    }

    public void HideControls()
    {
        controls.SetActive(false);
    }
    public void ConfirmBackToBase()
    {
        confirmBase.SetActive(true);
    }
    public void BacktoBase()
    {
        Resume();

        PlayerGM.maxHealth = 100;
        PGM.HealthBar.maxValue = 100;
        PlayerGM.Espectral = 0;
        PlayerGM.dieOnce = true;
        PGM.ArmaEspectral.maxValue = 100;
        TEAtck.basicAttackDamage = 5;
        PlayerGM.armas = 0;

        foreach(Image img in GM.armaEspectralImages)
        {
            img.enabled = false;
        }

        deathCanvas.SetActive(false);
        confirmBase.SetActive(false);
        foreach (GameObject canvas in otrosCanvas)
        {
            canvas.SetActive(true);
        }
        SaveData();
        SceneManager.LoadScene("Base");
    }
    public void ConfirmExitGame()
    {
        confirmExit.SetActive(true);
    }
    public void ExitGame()
    {
        SaveData();
        Application.Quit();
    }

    void SaveData()
    {
        //guardar todo
    }
}
