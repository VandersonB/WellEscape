using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atirador : ControleInimigos //Classe filha da ControleInimigo (herda as variaveis)
{
    [SerializeField]
    private GameObject pedraPrefab;
    [SerializeField]
    private Transform arremesso;
    [SerializeField]
    private float ajusteY;
    [SerializeField]
    private float ajusteX;

    private Vector2 lancamento;
    private AcoesJogador acJogador;
    private bool flipou;
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
            if ((player.GetComponent<Transform>().position.x > transform.position.x && sprite.flipX) || 
            (player.GetComponent<Transform>().position.x < transform.position.x && !sprite.flipX))
            {
                Flip();
                flipou = true;
            }
        }
        flipou = false;
    }
    void FixedUpdate() 
    {
        if((Mathf.Abs(player.transform.position.y - 
        transform.position.y)) <= distanciaPlataforma)
        {
            Debug.Log("Executou ataque");
            if( Mathf.Abs(distancia) >= distanciaAtaque)
            {
                animator.SetBool("atacando", false);
                animator.SetBool("construindo", true);
                Debug.Log("Cancelou ataque");
            }
            if( Mathf.Abs(distancia) < distanciaAtaque)
            {
                Debug.Log("Iniciou Ataque");
                animator.SetBool("atacando", true);
                animator.SetBool("construindo", false);
            }
        }
    }

    private void AtacarPedra()
    {
        posicaoArremesso = arremesso.position;
        if (flipou) //não esta funcionando.
        {
            posicaoArremesso = new Vector2(-arremesso.position.x, arremesso.position.y);
        }
        lancamento = new Vector2(ajusteX, ajusteY);
        var obj = GameObject.Instantiate(pedraPrefab, posicaoArremesso, arremesso.rotation); //cria a pedra que esta sendo arremessada;
        obj.GetComponent<Pedra>().AcoesJogador = acJogador; //endereça o script "AcoesJogador" para a pedra para o caso dela acionar o metodo Morrer do jogador, caso ela acerte.
        obj.GetComponent<Rigidbody2D>().AddForceAtPosition(lancamento, posicaoArremesso, ForceMode2D.Force);
    }
}
