using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
public PlayerHealth pHP;
public GameObject enemy;

public float spawntime = 3f;

public Transform[] spawnPoints;

[SerializeField]
MonoBehaviour factory;
IFactory Factory { get { return factory as IFactory;}}

    // Start is called before the first frame update
    void Start()
    {
        // mengeksekusi fungsi spawn setiap bbrp detik sesuai dgn nilai spawntime
        InvokeRepeating("Spawn",spawntime, spawntime);
    }

    void Spawn()
    {
        // kalo player mati ga buat e baru
        if(pHP.currHP <= 0f) return;

        // dptkan nilai random
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnEnemy = Random.Range(0,3);
        // duplikat Enemy

        Factory.FactoryMethod(spawnEnemy);
    }

}
