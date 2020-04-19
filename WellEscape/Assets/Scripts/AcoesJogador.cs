using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//biblioteca usada para criar UnityEvents

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

    private bool grounded; //variavel de controle do pulo (condição para pular)
    private Rigidbody2D rb2D; //criação de variável de manipulação do rigidbody do player
    private Animator animator; //criação de variavel de manipulaçao do animator
    private float raioDoItem = 1f;
    private GameObject[] item;
    private Interface interfaceJogador;
    private AudioSource meuAudioSource;
    private float velocidadeQueda;
    private bool estaMorto;
    private Collider2D colisorJogador;
    private Vector2 colisorJogadorInicial;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();         //Coleta os componentes do player
        animator = GetComponent<Animator>();        // --
        interfaceJogador = GameObject.FindObjectOfType<Interface>();//puxa o script de interface, para poder mandar o comando de exibir o texto.
        meuAudioSource = this.GetComponent<AudioSource>();
        colisorJogador = this.GetComponent<Collider2D>();
    }

    private void Start()
    {
        item = GameObject.FindGameObjectsWithTag("Carta"); //carrega todos as cartas que estão no jogo.
        colisorJogadorInicial = colisorJogador.offset;
    }
    void Update()
    {
       grounded = Physics2D.OverlapCircle(groundCheck.position, RaioPulo, 1 << LayerMask.NameToLayer("Ground"));     
        if (Input.GetKeyDown(pulo) && grounded)
        {
            aoPressionarPulo.Invoke();
        }

        if (Input.GetKeyDown(pegar))
        {
            aoPressionarPegar.Invoke();
        }

        if (Input.GetKeyDown(cancelar))
        {
            aoPressionarCancelar.Invoke();
        }
        if(Input.GetKeyDown(abaixar)&& grounded)
        {
            Debug.Log("Abaixou");
            aoPressionarAbaixar.Invoke();
        }

        if (Input.GetKeyUp(abaixar))
        {
            colisorJogador.offset = colisorJogadorInicial;
        }
        AnimacaoPulo(grounded);
     
    }

    private void FixedUpdate()
    {
        //a morte é definida nas seguintes condições: queda de altura elevada, tiro arremessado
        if (!grounded)
        {
            velocidadeQueda = Math.Abs(rb2D.velocity.y);
            Debug.Log("velocidade em y = " + velocidadeQueda);
            if (velocidadeQueda > velocidadeMorte)
            {
                    estaMorto = true;
                    Morrer(estaMorto);
            }

        }
        velocidadeQueda = 0;
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
        Debug.Log("Executou");
        for(int i=0; i<item.Length; i++) //varre toda a lista para ver se tem algum item perto para pegar
        {
            var distancia = Vector2.Distance(this.transform.position, item[i].transform.position);//calcula a distancia do jogador para o item.
            if (distancia < raioDoItem)//se estiver próximo o suficiente.
            {
                //desativa a carta que foi coletada, mas a mantém na lista (não consegui encontrar uma forma de remover sem dar erro no próximo item.
                item[i].SetActive(false);
                interfaceJogador.MostrarCarta(i);
            }
        }
    }

    public void Abaixar()
    {
        //o código devera reduzir o tamanho do collider e animar a animação de abaixar;
        colisorJogador.offset = new Vector2(0, ajusteDeColisorAgaixado);
       
    }
    void OnDrawGizmos()//desenha a esfera de detecção do chão para o pulo, apenas para visualização
    {                                               
        Gizmos.DrawWireSphere(groundCheck.position, RaioPulo);
    }
}
