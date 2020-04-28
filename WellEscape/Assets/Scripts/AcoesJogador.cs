﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//biblioteca usada para criar UnityEvents

//28/04 - a ideia é repaginar os inputs do jogo. NO caso, como não usamos a ação de abaixar ao longo do jogo, usaremos essa ação para pegar itens
// o pulo ficará no espaço e o cancelar e pause permanecerá no esc, até teste.
public class AcoesJogador : MonoBehaviour
{
    [SerializeField]
    private float forcapulo = 1;  // defini a força do pulo do player
    [SerializeField]
    private Transform groundCheck; //definida pela posição do GameObject "GroundCheck" do Player,
    [SerializeField]
    private AudioClip audioPulo;
    [SerializeField]
    private AudioClip audioMorte;
    [SerializeField]
    private KeyCode pulo;
    [SerializeField]
    private KeyCode pegar;
    [SerializeField]
    private KeyCode cancelar;
    [SerializeField]
    private KeyCode abaixar;
    [SerializeField]
    private UnityEvent aoPressionarAbaixar;
    [SerializeField]
    private UnityEvent aoPressionarPulo;
    [SerializeField]
    private UnityEvent aoPressionarPegar;
    [SerializeField]
    private UnityEvent aoPressionarCancelar;
    [SerializeField]
    private float RaioPulo = 0.1f;//define o raio de ação do CheckGound do Player para o pulo
    [SerializeField]
    private float velocidadeMorte;//define a velocidade de queda que acarretará na morte do jogador.
    [SerializeField]
    private float ajusteDeColisorAgaixado;
    [SerializeField]
    private GameObject plataforma;

    private bool grounded; //variavel de controle do pulo (condição para pular)
    private Rigidbody2D rb2D; //criação de variável de manipulação do rigidbody do player
    private Animator animator; //criação de variavel de manipulaçao do animator
    private float raioDoItem = 1f;
    private Carta[] item;
    private Interface interfaceJogador;
    private AudioSource meuAudioSource;
    private float velocidadeQueda;
    private bool estaMorto;
    private Collider2D colisorPe;
    private int numeroCarta;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();         //Coleta os componentes do player
        animator = GetComponent<Animator>();        // --
        interfaceJogador = GameObject.FindObjectOfType<Interface>();//puxa o script de interface, para poder mandar o comando de exibir o texto.
        meuAudioSource = this.GetComponent<AudioSource>();
        colisorPe = this.GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        item = GameObject.FindObjectsOfType<Carta>(); //carrega todos as cartas que estão no jogo.
    }
    void Update()
    {
       grounded = Physics2D.OverlapCircle(groundCheck.position, RaioPulo, 1 << LayerMask.NameToLayer("Ground"));     
        if (Input.GetKeyDown(pulo) && grounded)
        {
            if (Time.timeScale!=0)
            {
                aoPressionarPulo.Invoke();
            }
        }

        //if (Input.GetKeyDown(pegar))
        //{
        //aoPressionarPegar.Invoke();
        //}

        if (Input.GetKeyDown(cancelar))
        {
            aoPressionarCancelar.Invoke();
        }
        if(Input.GetKeyDown(abaixar)&& grounded)
        {
            aoPressionarAbaixar.Invoke();
        }

        if (Input.GetKeyUp(abaixar))
        {
            animator.SetBool("abaixando", false);
        }
        AnimacaoPulo(grounded);
     
    }

    private void FixedUpdate()
    {
        velocidadeQueda = Math.Abs(rb2D.velocity.y); //caso o jogador caia muito rápido ele morrerá, de acordo com o OnCollisionEnter no fim desse código.
    }

    public void Morrer(bool morrer)
    {
        //executa animação de morrer, que seria o inverso do acordar
        ControleAudio.instancia.PlayOneShot(audioMorte);
        interfaceJogador.Reiniciar();       
        estaMorto = false;
    }

    public void Pulo()//PULO DO PLAYER bool j
    {
        rb2D.AddForce(transform.up * forcapulo, ForceMode2D.Impulse);
        meuAudioSource.PlayOneShot(audioPulo);
    }

    public void AnimacaoPulo(bool jump) //CONTROLE DAS ANIMAÇÕES DO PLAYER
    {
        if (!jump)
        {
            animator.SetBool("pulando", true);           
        }            //Se não estiver no chão anima o pulo
        if (jump)
        {
            animator.SetBool("pulando", false);
        }          // --- situação oposta ...,
    }

    public void PegarItem()//Aqui o jogador pegará cartas no chão, que contará a história do jogo.
    {
        for(int i=0; i<item.Length; i++) //varre toda a lista para ver se tem algum item perto para pegar
        {
            var distancia = Vector2.Distance(this.transform.position, item[i].transform.position);//calcula a distancia do jogador para o item.
            if (distancia < raioDoItem)//se estiver próximo o suficiente.
            {
                //desativa a carta que foi coletada, mas a mantém na lista (não consegui encontrar uma forma de remover sem dar erro no próximo item.
                item[i].gameObject.SetActive(false);
                numeroCarta = item[i].NumeroCarta();
                interfaceJogador.MostrarCarta(numeroCarta);
            }
        }
    }

    public void Abaixar()
    {
        animator.SetBool("abaixando", true);    
    }
    void OnDrawGizmos()//desenha a esfera de detecção do chão para o pulo, apenas para visualização
    {                                               
        Gizmos.DrawWireSphere(groundCheck.position, RaioPulo);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (velocidadeQueda > velocidadeMorte && other.gameObject.tag=="Plataforma")
        {            
            estaMorto = true;
            Morrer(estaMorto);
        }
    }

}
