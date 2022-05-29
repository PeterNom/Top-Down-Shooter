using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject []spawn;
    public GameObject Enemy;
    public float []SpawnRate = { 30.0f, 20.0f, 10.0f };
    public float []elapsedTime = { 0.0f, 0.0f, 0.0f };


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<3; i++)
        {
            elapsedTime[i] += Time.deltaTime;

            if (elapsedTime[i] > SpawnRate[i])
            {
                elapsedTime[i] = 0;
                Debug.Log(true);

                GameObject newEnemy = (GameObject)Instantiate(Enemy, spawn[i].transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
        

        
    }
}
