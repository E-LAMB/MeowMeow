using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shards : MonoBehaviour
{

    public ParticleSystem my_system;
    public GameObject body;
    public GameObject my_object;

    public Transform outer_shard;
    public Transform inner_shard;

    public GameObject special_spawnpoint;

    public bool is_collected;

    public GameObject my_light;
    public LayerMask player_layer;

    public bool trigger_collection;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !is_collected)
        {
            Mind.remaining_shards -= 1;
            GameObject new_spawn = Instantiate(special_spawnpoint, gameObject.transform);
            new_spawn.GetComponent<AutoParent>().DoParent();
            my_system.Play();
            is_collected = true;
            body.SetActive(false);
            Destroy(my_object);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Mind.AddShard();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Q))
        {
            if (Random.Range(1,20) == 1)
            {
                trigger_collection = true;
            }
        }
        

        if (!is_collected && trigger_collection)
        {
            Mind.remaining_shards -= 1;
            is_collected = true;
            body.SetActive(false);
            Destroy(my_object);
        }

        my_light.SetActive(Physics.CheckSphere(gameObject.transform.position, 40f, player_layer));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        outer_shard.Rotate (Mind.shard_turn_speed * Time.deltaTime * Random.Range(-1f, 1f), Mind.shard_turn_speed * Time.deltaTime * Random.Range(-1f, 1f), Mind.shard_turn_speed * Time.deltaTime * Random.Range(-1f, 1f));
        inner_shard.Rotate (Mind.shard_turn_speed * Time.deltaTime * Random.Range(-1f, 1f), Mind.shard_turn_speed * Time.deltaTime * Random.Range(-1f, 1f), Mind.shard_turn_speed * Time.deltaTime * Random.Range(-1f, 1f));
    }
}
