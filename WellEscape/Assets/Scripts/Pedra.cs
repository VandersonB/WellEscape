using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedra : MonoBehaviour
{
    
    private bool atingiujogador = false;

    public AcoesJogador AcoesJogador;
    public Atirador Atirador;

    public void OnCollisionEnter2D(Collision2D collision)
    {
       
        var obj = collision;
        if (obj.gameObject.tag == "Player")// se a bala colide com o jogador ele deverá morrer;
        {
            atingiujogador = true;
            AcoesJogador.Morrer(atingiujogador);
            Destroy(this);
        }
        else //acertou a plataforma ele não precisa continuar existindo.
        {
            Atirador.Tocar();
            Destroy(this.gameObject);
        }
    }
}
