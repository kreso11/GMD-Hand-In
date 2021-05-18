using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject unityChan;
    public bool stop;
    public int spawnTime;
    public int spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnChan",spawnTime,spawnDelay);
    }

    public void SpawnChan(){
        Instantiate(unityChan, transform.position,transform.rotation);
        if (stop){
            CancelInvoke("SpawnChan");
        }
    }
}
