using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inimigo : ControleInimigos //Classe filha da ControleInimigo (herda as variaveis)
{
    void Start() //funcão start da filha (sobreescreve a da mae)
    {

    }
    void Update() //função update da filha (sobreescreve a da mae)
    {
        distancia = DistanciaDoPlayer(); //calcula a distancia do player
        estaMovendo = (distancia <= distanciaAtaque);
        if(estaMovendo)
        {
            if ((player.position.x > transform.position.x && sprite.flipX) || 
            (player.position.x < transform.position.x && !sprite.flipX))
            {
                Flip();
            }
        
        
        }
        //Debug.Log("Distancia = " + distancia);
        }
    void FixedUpdate() 
    {
        if(estaMovendo)
        {   
            rb2D.velocity = new Vector2 (velocidade, rb2D.velocity.y);
            if( Mathf.Abs(distancia)> distanciaAtaque)
            {
                animator.SetBool("andando", false );
                animator.SetBool("correndo", false);    
            }
            if( Mathf.Abs(distancia) < distanciaAtaque && Mathf.Abs(distancia) > (distanciaAtaque/2))
            {
                animator.SetBool("andando", true );
                animator.SetBool("correndo", false);    
            }
            if(Mathf.Abs(distancia) < (distanciaAtaque/2))
            {
                animator.SetBool("andando", false );
                animator.SetBool("correndo", true);    
            }
        }
        

    }
}
