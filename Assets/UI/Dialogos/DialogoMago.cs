using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoMago : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public string[] lines;

    public float textSpeed = 0.1f;

    public GameObject Canvas_Interaccion, Mazmorra, Espectral_Canvas, DialogMago, mago;


    int index;

    Interaccion Interact;
    // Start is called before the first frame update
    void Awake()
    {
        dialogueText.text = string.Empty;
        Interact = FindObjectOfType<Interaccion>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                nextline();
            }

            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }

    }

    public void StartDialogueMago()
    {

        TestingInputSystem.stop = true;
        index = 0;

        Mazmorra.gameObject.SetActive(false);
        Canvas_Interaccion.gameObject.SetActive(true);
        mago.gameObject.SetActive(true);

        DialogMago.SetActive(true);

        StartCoroutine(WriteLine());

    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);

        }
    }

    public void nextline()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }

        else
        {
            Canvas_Interaccion.gameObject.SetActive(false);
            DialogMago.SetActive(false);
            dialogueText.text = string.Empty;
            Espectral_Canvas.SetActive(true);

        }
    }
}
