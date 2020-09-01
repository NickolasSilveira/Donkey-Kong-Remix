using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawncontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Barrel;
    public float rateSpawn;
    public float currentTime;

    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= rateSpawn)
        {
            currentTime = 0;
            GameObject tempBarrel = Instantiate(Barrel) as GameObject;
        }
    }
}
