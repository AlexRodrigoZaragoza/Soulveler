using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsStart : MonoBehaviour
{
    bool SettingsActive;
    float TimeScale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SettingsActive)
            {
                TimeScale = Time.timeScale;
                this.transform.GetChild(0).gameObject.SetActive(true);
                SettingsActive = true;

                Time.timeScale = 0f;
            }
            else if (SettingsActive)
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
                SettingsActive = false;
                Time.timeScale = TimeScale;
            }
            
        }
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
