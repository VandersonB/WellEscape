
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    [SerializeField] // para aparecer no inspector do player
    private float velocidade = 4; // defini a velocidade do player 

    private bool temParede;
    private Rigidbody2D rb2D; //criação de variável de manipulação do rigidbody do player
    private bool eLadoDireito; //parametro para identificar o lado q o player está virado para o pivotamento de sprite
    private Animator animator; //criação de variavel de manipulaçao do animator
    private float horizontal;  //variavel para controlar player 1 Eixo X.
    public Vector2 posicaoInicial { get; private set; }
    private void Awake()
    {
        posicaoInicial = this.GetComponent<Transform>().position;
        rb2D = GetComponent<Rigidbody2D>();         //Coleta os componentes do player
        animator = GetComponent<Animator>();        // --    
    }
    void Start()
    {
        eLadoDireito = transform.localScale.x > 0; // --
    }
    void FixedUpdate() //CONTROLES (asdw - esquerda,abaixar,direita,pulo)
    {
        //Debug.DrawRay(transform.position, Vector2.right, Color.blue, 0.0001f);
        //Debug.DrawRay(transform.position, Vector2.left, Color.blue, 0.0001f);
        if((Input.GetButton("Direita")&& animator.GetBool("abaixando") == false)||
        (Input.GetButton("Esquerda")&&animator.GetBool("abaixando")==false))
        {
        if(Input.GetButton("Direita"))
            {
                /*Debug.Log("Apertou para direita!");
                if(Physics2D.Raycast( transform.position, Vector2.right, 0.0001f))
                {
                    temParede = true;
                    Debug.Log("Encontrou parede a direita!");
                }
                else temParede = false;
                //if(!temParede)
                //{
                //    transform.position += new Vector3 (Input.GetAxis("Direita")*velocidade*Time.deltaTime, 0f, 0f);
                //    horizontal = 0.5f;
                //}*/
                transform.position += new Vector3 (Input.GetAxis("Direita")*velocidade*Time.deltaTime, 0f, 0f);
                horizontal = 0.5f;
            }
            if(Input.GetButton("Esquerda"))
            {
                /*Debug.Log("Apertou para equerda!");
                if(Physics2D.Raycast( transform.position, Vector2.right, 0.0001f))
                {
                    temParede = true;
                    Debug.Log("Encontrou parede a esquerda!");
                }
                else temParede = false;
                //if(!temParede)
                //{
                //    transform.position += new Vector3 (Input.GetAxis("Esquerda")*velocidade*Time.deltaTime, 0f, 0f);
                //    horizontal = -0.5f;
                //}*/
                transform.position += new Vector3 (Input.GetAxis("Esquerda")*velocidade*Time.deltaTime, 0f, 0f);
                horizontal = -0.5f;
            }
        }
        else horizontal = 0;      
        MudarDirecao(horizontal);                   //função de direção que recebe o valor do eixo X (-1~1)
        Animacao(horizontal);
        //horizontal = Input.GetAxis("Horizontal");   //coloca na variavel o valor o eixo Horizontal (config A e B na Unity)
        //Movimentar(horizontal);                     //funçao de deslocamento que recebe o valor do eixo X (-1~1)
    }

    

    private void MudarDirecao(float horizontal) //VIRADA DE SPRITE DO PLAYER
    {
        if (horizontal > 0 && !eLadoDireito || horizontal < 0 && eLadoDireito)
        {                                                                       // se (X > 0 && sprite pra esquerda < 0 OU X < 0 && sprite pra direita)
            eLadoDireito = !eLadoDireito;                                       // inverte o argumento e o sprite
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);// --
        }
    }
    private void Animacao(float horizontal)
    {
        animator.SetFloat("velocidade", Mathf.Abs(horizontal));               //Se tiver velocidade abs > 0 anima corrida
    }
    /*private void Movimentar(float h)//MOVIMENTAÇÃO LATERAL DO PLAYER
    {
        rb2D.velocity = new Vector2(h * velocidade, rb2D.velocity.y); //parametro velocidade do rb2D = (eixo X * velocidade, mantem eixo y)     
        Animacao(rb2D.velocity.x);
    }*/
}
