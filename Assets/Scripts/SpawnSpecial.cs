using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpecial : MonoBehaviour
{

    public GameObject reveal_prefab;
    public GameObject stun_prefab;
    public GameObject magnet_prefab;

    public GameObject reveal_shard;
    public GameObject stun_orb;
    public GameObject magnet_orb;

    public GameObject[] positions;
    public GameObject selected_position;

    public bool can_do_magnet = true;

    public float spawntime;
    public int chosen_special;
    // 1 = Reveal
    // 2 = Stun
    // 3 = Magnet

    public bool avaliable_special;

    public LayerMask blocking_layers;
    public LayerMask player_layer;

    // Start is called before the first frame update
    void Start()
    {
        spawntime = Random.Range(25f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        positions = GameObject.FindGameObjectsWithTag("SpecialSpawn");

        spawntime -= Time.deltaTime;

        Mind.special_reveal -= Time.deltaTime;
        Mind.special_stun -= Time.deltaTime;
        Mind.special_magnet -= Time.deltaTime;

        if (spawntime < 0f)
        {
            if (positions.Length > 0)
            {
                selected_position = positions[Random.Range(0, positions.Length)];
                chosen_special = Random.Range(1, 4);

                avaliable_special = true;

                // if ((reveal_shard != null || Mind.special_reveal > 0f) && (stun_orb != null || Mind.special_stun > 0f)) { avaliable_special = false; } else { avaliable_special = true;  }

                if (chosen_special == 1 && (reveal_shard != null || Mind.special_reveal > -15f)) { chosen_special += 1; }
                if (chosen_special == 2 && (stun_orb != null || Mind.special_stun > -15f)) { chosen_special += 1; }
                if (can_do_magnet && ((chosen_special == 3 && Mind.remaining_shards < 46) || (chosen_special == 3 && (magnet_orb != null || Mind.special_magnet > 0f)))) { chosen_special = 1; }

                if (chosen_special == 1 && (reveal_shard != null || Mind.special_reveal > -15f)) { chosen_special += 1; }
                if (chosen_special == 2 && (stun_orb != null || Mind.special_stun > -15f)) { chosen_special += 1; }
                if (can_do_magnet && ((chosen_special == 3 && Mind.remaining_shards < 46) || (chosen_special == 3 && (magnet_orb != null || Mind.special_magnet > 0f)))) { avaliable_special = false; }

                if (!Physics.CheckSphere(selected_position.transform.position, 45f, player_layer) && !Physics.CheckSphere(selected_position.transform.position, 10f, blocking_layers) && avaliable_special)
                {
                    // Debug.Log("Spawned in: " + chosen_special.ToString());
                    if (chosen_special == 1)
                    {
                        reveal_shard = Instantiate(reveal_prefab, selected_position.transform, false);
                    }
                    if (chosen_special == 2)
                    {
                        stun_orb = Instantiate(stun_prefab, selected_position.transform, false);
                    }
                    if (chosen_special == 3)
                    {
                        magnet_orb = Instantiate(magnet_prefab, selected_position.transform, false);
                    }

                    spawntime = Random.Range(20f, 35f);
                }

            }
        }
    }
}
