using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaSubir : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] GameObject fade;
    bool cantSpam;

    private void Start()
    {
        Select.RUN = 1;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * velocidad);
        if (transform.localPosition.y >= 300 && !cantSpam)
        {
            cantSpam = true;
            fade.SetActive(true);
            StartCoroutine(changeScene());
        }
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneLoader.Instance.LoadScene("Base");
    }
}
