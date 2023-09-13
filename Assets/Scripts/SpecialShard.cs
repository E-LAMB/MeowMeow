using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialShard : MonoBehaviour
{

    public GameObject body;
    public GameObject my_object;

    public Transform outer_shard;
    public Transform inner_shard;

    public bool is_collected;

    public string my_type;

    public int shards_absorbed;
    public GameObject[] shards;

    public float existence_time;

    public string my_title;
    public Color my_color;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !is_collected)
        {
            is_collected = true;
            if (my_type == "Reveal")
            {
                Mind.special_reveal = 30f;
            } 
            if (my_type == "Stun")
            {
                Mind.special_stun = 12f;
            }

            if (my_type == "Magnet")
            {
                Mind.special_magnet = 10f;
            }

            Mind.stext_color = new Vector3(my_color.r, my_color.g, my_color.b);
            Mind.stext_string = my_title;
            Mind.stext_float = 2f;

            if (my_type != "Magnet")
            {
                body.SetActive(false);
                Destroy(my_object, 0.5f);
            } else
            {
                body.transform.position = new Vector3 (0f, 50f, 0f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        existence_time = Random.Range(30f, 60f);
    }

    void Update()
    {
        existence_time -= Time.deltaTime;
        if (existence_time < 0f)
        {
            Destroy(my_object);
        }
        if (my_type == "Magnet" && Mind.remaining_shards < 30)
        {
            Destroy(my_object);
        }

        shards = GameObject.FindGameObjectsWithTag("ShardScript");
        if (my_type == "Magnet" && Mind.special_magnet > 0f)
        {
            for (int i = 0; i < shards.Length; i++)
            {
                if (shards[i] != null && shards_absorbed != 15)
                {
                    shards[i].GetComponent<Shards>().trigger_collection = true;
                    shards_absorbed += 1;
                }
                if (shards_absorbed == 15)
                {
                    Destroy(my_object);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        outer_shard.Rotate(0f, 0f, Mind.shard_turn_speed * Time.deltaTime * 0.5f);
        inner_shard.Rotate(0f, 0f, Mind.shard_turn_speed * Time.deltaTime * -1f);
    }
}