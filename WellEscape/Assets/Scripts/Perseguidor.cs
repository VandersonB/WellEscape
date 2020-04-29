using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Perseguidor : ControleInimigos //Classe filha da ControleInimigo (herda as variaveis)
{
    private AudioSource meuAudioSource;
    void Start() //funcão start da filha (sobreescreve a da mae)
    {
        acoesJogador= GameObject.FindObjectOfType<AcoesJogador>();
        meuAudioSource = this.GetComponent<AudioSource>();
    }
    void Update() //função update da filha (sobreescreve a da mae)
    {
        distancia = DistanciaDoPlayer(); //calcula a distancia do player
        estaMovendo = (distancia <= distanciaAtaque);
        if(estaMovendo && (Mathf.Abs(player.transform.position.y) - 
        Mathf.Abs(transform.position.y)) <= distanciaPlataforma)
        {
            if ((player.position.x > transform.position.x && sprite.flipX) || 
            (player.position.x < transform.position.x && !sprite.flipX))
            {
                Flip();
            }    
        }
    }
    void FixedUpdate() 
    {
        if(estaMovendo && (Mathf.Abs(player.transform.position.y - 
        transform.position.y)) <= distanciaPlataforma)
        {
            if(Mathf.Abs(distancia) <= distanciaAtaque/2)
            {
                rb2D.velocity = new Vector2 (velocidade*1.5f, rb2D.velocity.y);
            }   
            else
            {
                rb2D.velocity = new Vector2 (velocidade, rb2D.velocity.y);
            }    
        }
        if( Mathf.Abs(distancia) >= distanciaAtaque || rb2D.velocity.x ==0)
        {
             animator.SetBool("empurrar", false);
             animator.SetBool("andando", false );
             animator.SetBool("correndo", false);    
         }
        if( Mathf.Abs(distancia) < distanciaAtaque && Mathf.Abs(distancia) >= (distanciaAtaque/2) && rb2D.velocity.x !=0)
        {
            animator.SetBool("empurrar", false);
            animator.SetBool("andando", true );
            animator.SetBool("correndo", false);
            //meuAudioSource.Play();
        }
        if(Mathf.Abs(distancia) < (distanciaAtaque/2) && Mathf.Abs(distancia) >= (distanciaAtaque/8) && rb2D.velocity.x !=0)
        {
            animator.SetBool("empurrar", false);
            animator.SetBool("andando", false );
            animator.SetBool("correndo", true);
            //meuAudioSource.Play();
        }   
    if(Mathf.Abs(distancia) < (distanciaAtaque/8))
        {
            animator.SetBool("andando", false );
            animator.SetBool("empurrar", true);    
            animator.SetBool("correndo", false);
        }   
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision;
        if (obj.gameObject.tag == "Player")// se o perseguidor encostar, o jogador morre.;
        {
            atingiujogador = true;
            acoesJogador.Morrer(atingiujogador);
        }
    }   
    
    private void Gemer()
    {
        meuAudioSource.Play();
    }
}