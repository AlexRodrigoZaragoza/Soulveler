using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.Collections;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider, musicSlider, effectsSlider;
    [SerializeField] TMP_InputField masterInputField, musicInputField, effectsInputField;

    [SerializeField] GameObject APLICADO;

    private string MIXER_MASTER = "MasterVolume", MIXER_MUSIC = "MusicVolume", MIXER_EFFECTS = "EffectsVolume"; //Referencias a los grupos del audio mixer

    void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        APLICADO.SetActive(false);
        //Rescatamos los valores guardados en el Player Prefs
        masterSlider.value = PlayerPrefs.GetFloat("master_prefs", masterSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat("music_prefs", musicSlider.value);
        effectsSlider.value = PlayerPrefs.GetFloat("effects_prefs", effectsSlider.value);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
        masterInputField.text = value.ToString("0.0");

        PlayerPrefs.SetFloat("master_prefs", masterSlider.value);
        PlayerPrefs.SetFloat("music_prefs", musicSlider.value);
        PlayerPrefs.SetFloat("effects_prefs", effectsSlider.value);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        musicInputField.text = value.ToString("0.0");

        PlayerPrefs.SetFloat("master_prefs", masterSlider.value);
        PlayerPrefs.SetFloat("music_prefs", musicSlider.value);
        PlayerPrefs.SetFloat("effects_prefs", effectsSlider.value);
    }

    void SetEffectsVolume(float value)
    {
        mixer.SetFloat(MIXER_EFFECTS, Mathf.Log10(value) * 20);
        effectsInputField.text = value.ToString("0.0");

        PlayerPrefs.SetFloat("master_prefs", masterSlider.value);
        PlayerPrefs.SetFloat("music_prefs", musicSlider.value);
        PlayerPrefs.SetFloat("effects_prefs", effectsSlider.value);
    }

    public void AplicarCambios()
    {
        StartCoroutine(Aplicado());
    }
    IEnumerator Aplicado()
    {
        APLICADO.SetActive(true);
        yield return new WaitForSeconds(1);
        APLICADO.SetActive(false);
    }
}

