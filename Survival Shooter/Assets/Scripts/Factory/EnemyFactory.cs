using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory{
    [SerializeField]
    public GameObject[] ePrefab;

    public GameObject FactoryMethod(int tag)
    {
        GameObject enemy = Instantiate(ePrefab[tag]);
        return enemy;
    }
}
