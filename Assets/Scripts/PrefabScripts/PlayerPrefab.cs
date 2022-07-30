using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefab : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField] 
    private Vector3 spawnPosition;

    [SerializeField]
    private bool random;

    public void onSpawnPlayer()
    {
        if(random)
        {
            float x = Random.Range(-4,4);
            float y = Random.Range(-4,4);
            float z = Random.Range(-4,4);

            Instantiate(player, new Vector3(x,y,z), Quaternion.identity);
        }
        else
        {
            Instantiate(player, spawnPosition, Quaternion.identity);            
        }
    }
}
