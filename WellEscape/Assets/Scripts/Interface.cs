using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private Text[] textoAmassado;
    [SerializeField]
    private Image papelAmassado;

    private GameObject[] cartas;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var textos in textoAmassado)
        {
            textos.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MostrarCarta(int i)
    {
        textoAmassado[i].gameObject.SetActive(true);
        papelAmassado.gameObject.SetActive(true);        
    }

    public void DesligarImagem()
    {
        papelAmassado.gameObject.SetActive(false);
        foreach (var textos in textoAmassado)
        {
            textos.gameObject.SetActive(false);
        }
    }
}
