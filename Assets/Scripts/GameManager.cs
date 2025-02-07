using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject _player;
    public GameObject _ultyObject;
    public GameObject _pala;
    public GameObject _martillo;
    public GameObject _ballesta;
    public GameObject _escopeta;
    TestingInputSystem tsi;
    public bool soulWeapon = false;
    public bool ulted;
    public bool Restando;
    public bool Cooldown;
    bool dontSpam;
    public CinemachineVirtualCamera cam;

    public static int ArmaActiva;

    PlayerGM gm;


    [SerializeField] public List<Image> palaImages = new List<Image>();
    [SerializeField] public List<Image> armaEspectralImages = new List<Image>();

    public void Awake()
    {
        tsi = FindObjectOfType<TestingInputSystem>();
        gm = FindObjectOfType<PlayerGM>();
    }

    private void Start()
    {
        PalaActiva();
    }

    public void Update()
    {
        //Debug.Log(ArmaActiva);

        if (SceneManager.GetActiveScene().name == "WitchBoss" || SceneManager.GetActiveScene().name == "Boss")
        {
            cam.m_Lens.OrthographicSize = 20;
        }
        else if (SceneManager.GetActiveScene().name == "FinalBoss")
        {
            if (_player.transform.position.x <= 0)
            {
                if (_player.transform.position.x < -20 && !dontSpam)
                {
                    cam.m_Lens.OrthographicSize = 20 + (1 * -((_player.transform.position.x + 20) / 2));
                }
                else
                {
                    dontSpam = true;
                    Debug.Log("a");
                    cam.m_Lens.OrthographicSize = 20;
                }


            }


        }
        else
        {
            dontSpam = false;
            cam.m_Lens.OrthographicSize = 12;
        }


        if (!soulWeapon && tsi.escopeteando == false)
        {
            ArmaActiva = 0;



            _pala.SetActive(true);
            _escopeta.SetActive(true);
            _martillo.SetActive(false);
            _ballesta.SetActive(false);
        }

        if (soulWeapon && PlayerGM.armas == 1)
        {
            ArmaActiva = 1;

            _martillo.SetActive(true);
            _ballesta.SetActive(false);
            _escopeta.SetActive(false);
            _pala.SetActive(false);
        }

        if (soulWeapon && PlayerGM.armas == 2)
        {
            ArmaActiva = 2;

            _ballesta.SetActive(true);
            _martillo.SetActive(false);
            _escopeta.SetActive(false);
            _pala.SetActive(false);
        }

        if (soulWeapon && PlayerGM.armas == 3)
        {
            ArmaActiva = 3;

            _martillo.SetActive(false);
            _ballesta.SetActive(false);
            _escopeta.SetActive(false);
            _pala.SetActive(false);
        }

        if (PlayerGM.armas == 1)
        {
            if (tsi.ulting == true)
            {
                cam.Follow = _ultyObject.transform;
                ulted = true;
            }

            if (ulted == true && tsi.ulting == false)
            {
                _player.transform.position = _ultyObject.transform.position;
                ulted = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (PlayerGM.armas != 0)
            {
                if (PlayerGM.Espectral >= 5 && Cooldown == false)//SI PULSO F
                {
                    soulWeapon = !soulWeapon;

                    //activar feedback arma espectral
                    ArmaEspectralActiva();

                    gm.StartCoroutine(gm.RestarEspectral());



                    if (!soulWeapon) //ARMA NORMAL
                    {

                        PalaActiva();
                        //activar feedback arma normal

                        Cooldown = true;
                        gm.StartCoroutine(gm.CDEspectral());
                        cam.Follow = _player.transform;
                        _ultyObject.SetActive(false);
                        tsi.ulting = false;
                        _player.SetActive(true);
                        tsi.attacking = false;
                        tsi.canMove = true;
                        tsi.canSecAttack = true;
                        Restando = false;
                    }
                }
            }
        }
    }

    public void PalaActiva()
    {
        foreach (Image im in armaEspectralImages)
        {
            im.color = new Color(im.color.r, im.color.g, im.color.b, 0.3f);
        }
        foreach (Image im in palaImages)
        {
            im.color = new Color(im.color.r, im.color.g, im.color.b, 1f);
        }
    }

    public void ArmaEspectralActiva()
    {
        foreach (Image im in armaEspectralImages)
        {
            im.color = new Color(im.color.r, im.color.g, im.color.b, 1f);
        }
        foreach (Image im in palaImages)
        {
            im.color = new Color(im.color.r, im.color.g, im.color.b, 0.3f);
        }
    }

}
