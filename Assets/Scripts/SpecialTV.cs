using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpecialTV : MonoBehaviour
{

    public float open_time;
    public float display_time;

    public RingGate my_gate;
    public bool in_endgame;

    public TextMeshPro text;

    public float random_time;

    public string[] guidance;

    public Camera security_takeover;

    // Start is called before the first frame update
    void Start()
    {
        random_time = 2f;
    }

    // Update is called once per frame
    void Update()
    {

        random_time -= Time.deltaTime;

        if (random_time < 0f && !in_endgame)
        {
            random_time = Random.Range(0.1f, 0.6f);
            text.text = guidance[Random.Range(0, guidance.Length)];
            if (text.text == "/shards")
            {
                text.text = Mind.remaining_shards.ToString() + " LEFT";
            }
        }

        if (Mind.remaining_shards == 0 && !in_endgame)
        {
            open_time = 35f;
            in_endgame = true;
            security_takeover.enabled = true;
        }

        if (in_endgame)
        {
            text.color = new Vector4(0.3f, 0f, 0f, 1f);
            display_time = Mathf.Round(open_time * 10f) / 10f;
            text.fontSize = 25f;
            if (display_time.ToString().Contains("."))
            {
                text.text = "GATE OPENS IN:   " + display_time.ToString();
            } else
            {
                text.text = "GATE OPENS IN:   " + display_time.ToString() + ".0";
            }
            
            open_time -= Time.deltaTime;
            if (0f > open_time)
            {
                Mind.has_ring = true;
            }
        }

        if (Mind.has_ring)
        {
            text.text = "ESCAPE";
        }
    }
}
