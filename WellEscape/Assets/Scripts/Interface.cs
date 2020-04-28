using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private Text[] textoAmassado;
    [SerializeField]
    private Image papelAmassado;
    [SerializeField]
    private Text[] textoCreditos;
    [SerializeField]
    private GameObject painel;
    [SerializeField]
    private Button[] botoes;
    [SerializeField]
    private Text titulo;
    [SerializeField]
    private GameObject reiniciar;
    [SerializeField]
    private Checkpoint[] checkpoints;
    [SerializeField]
    private Text textoFim;
    [SerializeField]
    private Button reiniciarFim;

    private MovimentoJogador movJogador;
    private AcoesJogador acJogador;
    private GameObject[] cartas;
    private ControlePause controlePause;   
    private bool passouPeloCheckpoint;
    private Vector2 novaPosicaoInicial;
    private bool terminouJogo;
    private GestaoInimigos gestaoInimigosJogo;
    void Awake()
    {
        terminouJogo = false;
        foreach (var textos in textoAmassado)
        {
            textos.gameObject.SetActive(false);
        }
        passouPeloCheckpoint = false;
    }

    void Start()
    {
        movJogador = GameObject.FindObjectOfType<MovimentoJogador>();
        acJogador = GameObject.FindObjectOfType<AcoesJogador>();
        controlePause = GameObject.FindObjectOfType<ControlePause>();
        gestaoInimigosJogo = GameObject.FindObjectOfType<GestaoInimigos>();
    }

    private void Update()
    {
        //só preciso verificar o primeiro checkpoint
        passouPeloCheckpoint = checkpoints[0].PassouPeloCheck();
    }
    public void MostrarCarta(int i)
    {
        textoAmassado[i].gameObject.SetActive(true);
        papelAmassado.gameObject.SetActive(true);
        controlePause.enabled = false;
        Time.timeScale = 0;
    }

    public void DesligarImagem()
    {
        if (textoAmassado[31].IsActive() )//última carta do jogo, quando o jogador sair da tela, deverá indicar o fim dele.
        {
            FinalDoJogo();
            terminouJogo = true;
        }

        else
        {
            Time.timeScale = 1;
            StartCoroutine(ApagaACarta());
        }

    }

    private void FinalDoJogo()
    {
        StartCoroutine(EsperarEApagarTexto(2, textoAmassado[31]));//iniciar a rotina que irá desaparecer a tela de crédito
        textoFim.gameObject.SetActive(true);
        StartCoroutine(EsperarEApareceFim(2, textoFim));
    }

    private IEnumerator EsperarEApareceFim(float tempoParaAparecer, Text textoFim)
    { 
        Color corTexto = textoFim.color;
        corTexto.a = 0f;
        textoFim.color = corTexto;
        float contador = 0;
        yield return new WaitForSeconds(2);
        //a partir desse momento, a ideia é fazer os textos e o painel esvanecescer
        while (textoFim.color.a <=1)
        {
            contador += Time.deltaTime / tempoParaAparecer;
            corTexto.a = Mathf.Lerp(0, 1, contador);// faz uma interpolação entre o ponto "a" e "b" a partir de um certo coeficiente, dado pelo contador.
            textoFim.color = corTexto;
            //uma vez que texto esta transparentes, desliga o GO.
            if (textoFim.color.a == 1 && terminouJogo)
            {
                acJogador.enabled = false;
                movJogador.enabled = false;
                reiniciarFim.gameObject.SetActive(true);
            }
            yield return null;
        }
    }

    private IEnumerator EsperarEApagarTexto(float tempoParaSumirTexto, Text texto)
    {
        //aqui abaixo, a ideia é garantir que o termo alfa da cor (que deixa ela transparente) é regulada de acordo com o seu valor inicial
        Color corTexto = texto.color;
        corTexto.a = 1f;
        texto.color = corTexto;
        float contador = 0;
        yield return new WaitForSeconds(2);
        //a partir desse momento, a ideia é fazer os textos e o painel esvanecescer
        while (texto.color.a > 0)
        {
            contador += Time.deltaTime / tempoParaSumirTexto;
            corTexto.a = Mathf.Lerp(1, 0, contador);// faz uma interpolação entre o ponto "a" e "b" a partir de um certo coeficiente, dado pelo contador.
            texto.color = corTexto;
            //uma vez que texto esta transparentes, desliga o GO.
            if (texto.color.a <= 0)
            {
                texto.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    private IEnumerator ApagaACarta()//o objetivo é esse método executar DEPOIS do método de PararOuContinuarJOgo, do script que controla o pause
    {
        yield return new WaitForSeconds(0.2f);
        papelAmassado.gameObject.SetActive(false);//inativa a imagem do papel
        foreach (var textos in textoAmassado)//garante que qualquer texto tenha sido inativado
        {
            textos.gameObject.SetActive(false);
        }
        controlePause.enabled = true;//habilita o script que controla o pause;
    }
    public void Creditos()
    {
            StartCoroutine(EsperarEApagarTela(2,painel));//iniciar a rotina que irá desaparecer a tela de crédito
    }

    private IEnumerator EsperarEApagarTela(float tempoParaSumir, GameObject objeto)
    {
        objeto.SetActive(true);//habilita o painel de creditos
        //aqui abaixo, a ideia é garantir que o termo alfa da cor (que deixa ela transparente) é regulada de acordo com o seu valor inicial
        Color corPainel = objeto.GetComponent<Image>().color;      
        corPainel.a = 0.3843137f;
        objeto.GetComponent<Image>().color = corPainel;
        for (int i = 0; i < textoCreditos.Length; i++)
        {
            Color corTexto = textoCreditos[i].color;
            corTexto.a = 1;
            textoCreditos[i].color = corTexto;
        }
        float contador = 0;
        yield return new WaitForSeconds(2);
        //a partir desse momento, a ideia é fazer os textos e o painel esvanecescer
        while (textoCreditos[textoCreditos.Length-1].color.a>0)
        {
            contador += Time.deltaTime / tempoParaSumir;
            corPainel.a = Mathf.Lerp(0.3843137f, 0, contador);// faz uma interpolação entre o ponto "a" e "b" a partir de um certo coeficiente, dado pelo contador.
            objeto.GetComponent<Image>().color = corPainel;
            for (int j = 0; j < textoCreditos.Length; j++)
            {
                Color corTexto = textoCreditos[j].color;
                corTexto.a = Mathf.Lerp(1, 0, contador);
                textoCreditos[j].color = corTexto;
            }
            //uma vez que todos os itens estão transparentes, desliga o GO.
            if (textoCreditos[textoCreditos.Length-1].color.a <= 0)
            {
                painel.gameObject.SetActive(false);
            }
            yield return null;
        }


    }

    public void Iniciar()
    {
        titulo.gameObject.SetActive(false);
        reiniciar.SetActive(false);
        movJogador.enabled = true;
            
        if (passouPeloCheckpoint)
        {
            //posiciona o jogador no local do checkpoint. Caso ele retorne no jogo e passe por outro checkpoint, esse será seu novo local de nascer;
            movJogador.GetComponent<Transform>().position = novaPosicaoInicial;
        }
            else 
        {
                movJogador.GetComponent<Transform>().position = movJogador.posicaoInicial;//ele não passou em nenhum checkpoint, retorna ao inicio
        }
        acJogador.enabled = true;
        Time.timeScale = 1;
        for (int i=0; i < botoes.Length; i++)
        {
            botoes[i].gameObject.SetActive(false);
        }
        gestaoInimigosJogo.ReposicionarInimigos();
    }

    public void AtualizaPosicao(Vector2 posicao)
    {
        novaPosicaoInicial = posicao;
    }
    public void Reiniciar()
    {
        Time.timeScale = 0;
        reiniciar.SetActive(true);
        acJogador.enabled = false;
        movJogador.enabled = false;
    }

    public void ReiniciarTodoJogo()
    {
        StartCoroutine(ReiniciaTudo());    
    }

    private IEnumerator ReiniciaTudo()
    {

        yield return new WaitForSecondsRealtime (0.8f);
        terminouJogo = false;
        textoFim.gameObject.SetActive(false);
        papelAmassado.gameObject.SetActive(false);
        //habilita o texto de titulo
        titulo.gameObject.SetActive(true);
        //habilita os botões de inicio
        foreach (var botao in botoes)
        {
            botao.gameObject.SetActive(true);
        }
        //garante que todos os checkpoints foram resetados
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.DesabilitaCheck();
        }
        //habilita o script de movimentação e desloca o jogador para o início
        movJogador.GetComponent<MovimentoJogador>().enabled = true;
        movJogador.GetComponent<Transform>().position = movJogador.posicaoInicial;
        reiniciarFim.gameObject.SetActive(false);
        movJogador.enabled = false;
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("Saiu do Jogo");
    }
}
