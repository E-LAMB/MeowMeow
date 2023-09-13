using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInitialShards : MonoBehaviour
{

    public GameObject[] spawn_locations;
    public GameObject spawn_location;

    public LayerMask blockers;

    public int remaining_spawn;

    public GameObject shard_prefab;

    // Start is called before the first frame update
    void Start()
    {
        spawn_locations = GameObject.FindGameObjectsWithTag("Spawnpoint");
        CreateNew();
    }

    void CreateNew()
    {
        if (remaining_spawn > 0)
        {
            spawn_location = spawn_locations[Random.Range(0, spawn_locations.Length)];

            if (!Physics.CheckSphere(spawn_location.transform.position, 15f, blockers))
            {
                remaining_spawn -= 1;
                Instantiate(shard_prefab, spawn_location.transform.position, spawn_location.transform.localRotation);
            } else
            {
                CreateNew();
            }    
        }
        if (remaining_spawn > 0)
        {
            CreateNew();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
