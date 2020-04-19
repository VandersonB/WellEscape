using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atirador : ControleInimigos //Classe filha da ControleInimigo (herda as variaveis)
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
        void OnCollisionEnter2D(Collision2D collision2D) 
        {           //Detecta se colidiu
            Debug.Log("COLIDIU com " + collision2D.gameObject.tag);    
        /*if(collision2D.gameObject.CompareTag("Player"))
        {
            rb2DPlayer.AddForce(new Vector2 (forcaEmpurrao , 0f));
        }*/
        }
    }
}
