using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    [SerializeField, Range(0, 35)]
    private int numeroCarta;

    public int NumeroCarta()
    {
        return numeroCarta;
    }
}