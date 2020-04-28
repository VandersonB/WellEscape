using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestaoInimigos : MonoBehaviour
{
    [SerializeField]
    private GameObject[] perseguidores;
    [SerializeField]
    private GameObject[] atiradores;

    [SerializeField]
    private Vector3[] posicaoInicialPerseguidores;
    [SerializeField]
    private Vector3[] posicaoInicialAtiradores;
    private void Start()
    {
        PosicaoInimigos();
    }

    private void Update()
    {
        
    }

    public void PosicaoInimigos()
    {
        for (int i = 0; i < perseguidores.Length; i++)
        {
            posicaoInicialPerseguidores[i] = new Vector3(perseguidores[i].transform.position.x, perseguidores[i].transform.position.y, perseguidores[i].transform.position.z);
        }
        for (int j = 0; j < atiradores.Length; j++)
        {
            posicaoInicialAtiradores[j] = atiradores[j].transform.position;
        }
    }
    public void ReposicionarInimigos()
    {
        StartCoroutine(Reposicionar());
       
    }

    private IEnumerator Reposicionar()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < posicaoInicialPerseguidores.Length; i++)
        {
            perseguidores[i].transform.position = new Vector3(posicaoInicialPerseguidores[i].x, posicaoInicialPerseguidores[i].y, posicaoInicialPerseguidores[i].z);
        }
        for (int j = 0; j < posicaoInicialAtiradores.Length; j++)
        {
            atiradores[j].transform.position = new Vector3(posicaoInicialAtiradores[j].x, posicaoInicialAtiradores[j].y, posicaoInicialAtiradores[j].z);
        }

    }
}
