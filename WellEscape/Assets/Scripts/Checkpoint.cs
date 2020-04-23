using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Transform novoLocalInicial;
    [SerializeField]
    private Interface interfaceJogador;

    public bool check {get; private set; }
    private Vector2 posicaoCheckpoint;

    private void Awake()
    {
        check = false;
    }

    private void Start()
    {
        interfaceJogador = GameObject.FindObjectOfType<Interface>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision;
        if (obj.gameObject.tag == "Player")
        {
            check = true;
            AtualizaCheckpoint();
            Debug.Log("Passou pelo Checkpoint");
        }
    }

    public void AtualizaCheckpoint()
    {
        posicaoCheckpoint = new Vector2(novoLocalInicial.position.x, novoLocalInicial.position.y);
        interfaceJogador.AtualizaPosicao(posicaoCheckpoint);//posicaoCheckpoint;
        Debug.Log("Mandou nova posição");
    }
    
    public bool PassouPeloCheck()
    {
        Debug.Log("retornou o check");
        return check;
    }
}
