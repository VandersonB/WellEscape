using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atirador : ControleInimigos //Classe filha da ControleInimigo (herda as variaveis)
{
    private Quaternion rotacao;
    [SerializeField]
    private GameObject pedraPrefab;
    [SerializeField]
    private Transform arremessoR;
    [SerializeField]
    private Transform arremessoL;
    [SerializeField]
    private float ajusteY;
    [SerializeField]
    private float ajusteX;

    private Vector2 lancamento;
    private AcoesJogador acJogador;
    public bool flipou;
    private Vector2 posicaoArremesso;
    void Start() //funcão start da filha (sobreescreve a da mae)
    {
        acJogador = GameObject.FindObjectOfType<AcoesJogador>();
    }
    void Update() //função update da filha (sobreescreve a da mae)
    {
        distancia = DistanciaDoPlayer(); //calcula a distancia do player
        estaMovendo = (distancia <= distanciaAtaque);
        if(estaMovendo && (Mathf.Abs(player.transform.position.y) - 
        Mathf.Abs(transform.position.y)) <= distanciaPlataforma)
        {
            if(velocidade <= 0)//olhando para esquerda
                {
                    posicaoArremesso = arremessoL.position;
                    rotacao = arremessoL.rotation;
                }
                if(velocidade > 0)//olhando para direita
                {
                    posicaoArremesso = arremessoR.position;
                    rotacao = arremessoR.rotation;
                }
            if ((player.GetComponent<Transform>().position.x > transform.position.x && sprite.flipX) || 
            (player.GetComponent<Transform>().position.x < transform.position.x && !sprite.flipX))
            {
                Flip();
            }    //posicaoArremesso = new Vector3(-arremesso.position.x, arremesso.position.y, arremesso.position.z);
            lancamento = new Vector2(velocidade*ajusteX, ajusteY);//forca corrigida por vel(-+1)
        }
        
       
    }
    void FixedUpdate() 
    {
        if((Mathf.Abs(player.transform.position.y - 
        transform.position.y)) <= distanciaPlataforma)
        {
            if( Mathf.Abs(distancia) >= distanciaAtaque)
            {
                animator.SetBool("atacando", false);
                animator.SetBool("construindo", true);
            }
            if( Mathf.Abs(distancia) < distanciaAtaque)
            {
                animator.SetBool("atacando", true);
                animator.SetBool("construindo", false);
            }
        }
    }

    private void AtacarPedra()
    {
        var obj = GameObject.Instantiate(pedraPrefab, posicaoArremesso, rotacao); //cria a pedra que esta sendo arremessada;
        obj.GetComponent<Pedra>().AcoesJogador = acJogador; //endereça o script "AcoesJogador" para a pedra para o caso dela acionar o metodo Morrer do jogador, caso ela acerte.
        obj.GetComponent<Rigidbody2D>().AddForceAtPosition(lancamento, posicaoArremesso, ForceMode2D.Force);
    }
}
