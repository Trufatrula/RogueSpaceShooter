using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class TypingFinishedEvent : UnityEvent<string>
{ }

public class MecanicaTexto : MonoBehaviour
{
    public TextMeshProUGUI TexoAEscribir;
    public string texto = "Texto";
    public float delayEscribir = 0.05f;

    public float waitText = 1f;
    public float waitAutocompletable = 1f;

    public TypingFinishedEvent OnTypingFinished;

    private void OnEnable()
    {
        TexoAEscribir.text = "";
        StartCoroutine(RevealText());
    }

    private IEnumerator RevealText()
    {
        TexoAEscribir.text = "";

        yield return new WaitForSeconds(waitText);

        for (int i = 0; i < texto.Length; i++)
        {
            TexoAEscribir.text += texto[i];
            TexoAEscribir.ForceMeshUpdate();

            yield return new WaitForSeconds(delayEscribir);
        }

        yield return new WaitForSeconds(waitAutocompletable);
        completeSentence();
    }

    public void completeSentence()
    {
        TexoAEscribir.text = texto;
        Debug.Log("Terminada");
        OnTypingFinished?.Invoke(texto);
    }
}