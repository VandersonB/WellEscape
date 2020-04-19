using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Perseguidor : ControlePerseguidor //Classe filha da ControleInimigo (herda as variaveis)
{
    void Start() //funcão start da filha (sobreescreve a da mae)
    {

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
        //Debug.Log("Distancia = " + distancia);
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
        }
        if(Mathf.Abs(distancia) < (distanciaAtaque/2) && Mathf.Abs(distancia) >= (distanciaAtaque/8) && rb2D.velocity.x !=0)
        {
            animator.SetBool("empurrar", false);
            animator.SetBool("andando", false );
            animator.SetBool("correndo", true);    
        }   
    if(Mathf.Abs(distancia) < (distanciaAtaque/8))
        {
            animator.SetBool("andando", false );
            animator.SetBool("empurrar", true);    
            animator.SetBool("correndo", false);
        }
    
    }
    void OnCollisionEnter2D(Collision2D collision2D) 
    {           //Detecta se colidiu
        Debug.Log("COLIDIU com " + collision2D.gameObject.tag);    
        /*if(collision2D.gameObject.CompareTag("Player"))
        {
            rb2DPlayer.AddForce(new Vector2 (forcaEmpurrao , 0f));
        }*/
    }
}