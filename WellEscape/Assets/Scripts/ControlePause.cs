using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlePause : MonoBehaviour
{
    [SerializeField]
    private GameObject painelPause;
    [SerializeField]
    private Image papelAmassado;
    

    public bool jogoEstaParado {get; private set; }
    private MovimentoJogador movJogador;
    private AcoesJogador acJogador;

    private void Start()
    {
        movJogador = GameObject.FindObjectOfType<MovimentoJogador>();
        acJogador = GameObject.FindObjectOfType<AcoesJogador>();
    }
    public void PausarOuContinuarJogo()
    {
        if (jogoEstaParado && !papelAmassado.IsActive())
        {
            StartCoroutine(ContinuarJogo());
        }
        if (!papelAmassado.IsActive() && !jogoEstaParado)
        {
            StartCoroutine(PararJogo());
        }
    }

    private IEnumerator ContinuarJogo()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        painelPause.SetActive(false);
        movJogador.enabled = true;
        Time.timeScale = 1;
        jogoEstaParado = false;
    }

    private IEnumerator PararJogo()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        painelPause.SetActive(true);
        movJogador.enabled = false;
        Time.timeScale = 0;
        jogoEstaParado = true;
    }
}
