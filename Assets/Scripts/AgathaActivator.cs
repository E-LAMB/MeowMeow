using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgathaActivator : MonoBehaviour
{

    public float waiting_time;
    public bool collected_shard;
    
    public NavMeshAgent agatha_agent;

    public Transform warp_1;
    public Transform warp_2;

    public Transform player_transform;

    public AgathaScript agatha_script;

    public AgathaActivator self;
    public GameObject snapshotter;

    public AudioSource voicebox;

    //public Transform agatha_body;

    // Start is called before the first frame update
    void Start()
    {
        player_transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mind.remaining_shards > Mind.max_shards)
        {
            Mind.max_shards = Mind.remaining_shards;
        }

        waiting_time += Time.deltaTime;

        if (waiting_time > 0.05f)
        {
            snapshotter.SetActive(false);
        }

        if (Mind.max_shards > Mind.remaining_shards && waiting_time > 1f)
        {
            collected_shard = true;
        }

        if (collected_shard && waiting_time > 3f)
        {
            agatha_agent.enabled = true;
            voicebox.enabled = true;
            if (Vector3.Distance(player_transform.position, warp_1.position) > Vector3.Distance(player_transform.position, warp_2.position))
            {
                agatha_agent.Warp(warp_1.position);
            } else
            {
                agatha_agent.Warp(warp_2.position);
            }
            agatha_script.enabled = true;
            agatha_script.tr.volume = 1f;
            agatha_script.tr_shrinking = false;
            self.enabled = false;
        }
    }
}
