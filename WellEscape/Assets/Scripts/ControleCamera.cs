using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour
{
    //Objetivo: criar uma camera que acompanhe o jogador, com ele centralizado. Ele precisa, além disso, perceber quando atinge o final da tela e parar de acompanhar o jogador.
    // Prosposta: verificar se a câmera atingiu o limite de borda, definido por um GO na cena (LimiteDaCamera) e, quando isso ocorrer, a câmera deixa de acompanhar o jogador.
    [SerializeField]
    private Transform jogador;
    [SerializeField]
    private GameObject limiteCamera;

    private float alturaDaCamera;
    private float larguraDaCamera;

    public float fatorDeCorrecao = 28f;

    void Awake()
    {
        alturaDaCamera = Camera.main.orthographicSize; //5
        larguraDaCamera = alturaDaCamera * Camera.main.aspect;
        Camera.main.transform.position = new Vector3(jogador.position.x, jogador.position.y, Camera.main.transform.position.z);
    }
    void Update()
    {
        // 15/04 com a elaboração do level design definitivo, a câmera precisou ter um fator de correção no segundo if, visto que ele era sempre verdadeiro. 
        // provavelmente há formas melhores de fazer esse código.
        if (jogador.position.x + larguraDaCamera < limiteCamera.transform.position.x + limiteCamera.GetComponent<BoxCollider2D>().size.x )
        {   
            if ((jogador.position.x - larguraDaCamera)* fatorDeCorrecao > limiteCamera.transform.position.x - limiteCamera.GetComponent<BoxCollider2D>().size.x) 
            {
                SeguirJogadorHorizontal();
            }
        }
            if (jogador.position.y + alturaDaCamera > limiteCamera.transform.position.y + limiteCamera.GetComponent<BoxCollider2D>().size.y / 2)
            {
                SeguirJogadorVertical();
            }        
    }

    private void SeguirJogadorHorizontal()
    {
        Camera.main.transform.position = new Vector3(jogador.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
    private void SeguirJogadorVertical()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, jogador.position.y, Camera.main.transform.position.z);
    }
  
}

