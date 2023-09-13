using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingShards : MonoBehaviour
{

    public GameObject[] shards;

    public Vector3 pointing_position;
    public float closest_shard;

    public Transform self;

    public GameObject pointer;

    public LayerMask shard_layer;

    public int activation_amount;

    // Start is called before the first frame update
    void Start()
    {
        shards = GameObject.FindGameObjectsWithTag("Shard");
        pointer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mind.remaining_shards < activation_amount && Mind.remaining_shards != 0 && !Physics.CheckSphere(self.position, 25f, shard_layer))
        {
            pointer.SetActive(true);
            closest_shard = 999f;

            for (int i = 0; i < shards.Length; i++)
            {
                if (shards[i] != null)
                {
                    if (Vector3.Distance(shards[i].transform.position, self.position) < closest_shard)
                    {
                        closest_shard = Vector3.Distance(shards[i].transform.position, self.position);
                        pointing_position = shards[i].transform.position;
                    }
                }
            }

            self.LookAt(pointing_position);
            Vector3 looking_angle = self.eulerAngles;
            looking_angle.x = 0f;
            looking_angle.z = 0f;
            self.eulerAngles = looking_angle;
        } else
        {
            pointer.SetActive(false);
        }

    }
}
