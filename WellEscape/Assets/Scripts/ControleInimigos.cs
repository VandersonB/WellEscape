using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleInimigos : MonoBehaviour//função que controla variaveis gerais de todos os iminigos deste tipo
{
public float distanciaAtaque;
public int velocidade;

protected float distancia;

protected Rigidbody2D rb2D; //protected é um tipo de variavel para ser vista pela classe filha
protected Animator animator;
protected Transform player;
protected SpriteRenderer sprite;

protected bool estaMovendo = false;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Jogador").GetComponent<Transform>();    
        sprite = GetComponent<SpriteRenderer>();
    }
     
    protected float DistanciaDoPlayer() // retorna a distancia do GameObjetct
    {
        return Vector2.Distance(player.position, transform.position);
    }  
    protected void Flip() //vira o sprite do Inimigo e inverte o vetor velocidade do iminigo
    {
        sprite.flipX = !sprite.flipX;
        velocidade *= -1;
    }
}
