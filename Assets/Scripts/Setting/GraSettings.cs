using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ResolutionList
{
    public int _width, _height;
}

public class GraSettings : MonoBehaviour
{
    [SerializeField] TMP_Text resolutionText;
    [SerializeField] GameObject AplicadoText;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle vsyncToggle;
    [SerializeField] TMP_Text fpsText;

    public List<ResolutionList> resolutions = new List<ResolutionList>();
    public List<int> fps = new List<int>();
    public int selectedResolution;
    public int selectedFPS = 1;

    public TMP_Text displayACTUALResolutionText; //Todo: la primera vez q lo abras sea fullscreen y a la maxima resolucion de la pantalla entonces el juego guarda en la lista de resoluciones tu resolucion maxima
    public TMP_Text displayScreenFPSText;
    public TMP_Text displayACTUALFPSText;

    //Parametros para calcular los FPS
    #region FPS
    private float pollingTime = 1f;
    private float time;
    private int frameCount;
    #endregion

    void Start()
    {
        AplicadoText.SetActive(false);

        fullscreenToggle.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
        {
            vsyncToggle.isOn = true;
        } 

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i]._width && Screen.height == resolutions[i]._height)
            {
                foundRes = true;

                selectedResolution = i;

                UpdateResText();
            }
        }

        Application.targetFrameRate = fps[PlayerPrefs.GetInt("FPSPrefs")];

        UpdateFPSText();

        resolutionText.text = Screen.width.ToString() + " X " + Screen.height.ToString();
    }
    private void Update()
    {
        FPSCounter(); //Calculate FPS

        displayACTUALResolutionText.text = Screen.currentResolution.width.ToString() + " X " + Screen.currentResolution.height.ToString(); //FPS actuales

        displayScreenFPSText.text = Screen.currentResolution.refreshRate.ToString() + " FPS "; //FPS de la pantalla
    }
    #region FPS Counter
    void FPSCounter()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            displayACTUALFPSText.text = frameRate.ToString() + " FPS ";

            time -= pollingTime;
            frameCount = 0;
        }
    }
    #endregion

    public void NextResolution()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count -1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResText();
    }
    public void BackResolution()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResText();
    }
    void UpdateResText()
    {
        resolutionText.text = resolutions[selectedResolution]._width.ToString() + " X " + resolutions[selectedResolution]._height.ToString();
    }
    public void NextFPS()
    {
        selectedFPS = Mathf.Clamp(selectedFPS, 0, fps.Count - 2);
        selectedFPS++;
        UpdateFPSText();
    }
    public void BackFPS()
    {
        selectedFPS = Mathf.Clamp(selectedFPS, 1, fps.Count);
        selectedFPS--;
        UpdateFPSText();
    }
    void UpdateFPSText()
    {
        fpsText.text = fps[selectedFPS].ToString();
    }

    public void AplicarCambios()
    {
        //Screen.fullScreen = fullscreenToggle.isOn;

        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution]._width, resolutions[selectedResolution]._height, fullscreenToggle.isOn);

        Application.targetFrameRate = fps[selectedFPS];
        PlayerPrefs.SetInt("FPSPrefs", selectedFPS);

        StartCoroutine(Aplicado());
    }

    IEnumerator Aplicado()
    {
        AplicadoText.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        AplicadoText.SetActive(false);
    }
}
