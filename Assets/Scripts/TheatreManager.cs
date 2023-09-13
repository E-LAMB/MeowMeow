using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreManager : MonoBehaviour
{

    public int current_stage;

    // 0: Player spawned into level
    // 1: Player locked in Theatre
    // 2: Announcement Speech
    // 3: The gates to Zone 1 opens
    // (After the player collects all the needed shards)
    // 4: Player collected all the shards
    // 5: Player runs into Theatre
    // 6: Player locked in Theatre
    // 7: Sparky speaks to you
    // 8: Sparky walks up to door, All doors open
    // 9: His door opens and he activates
    // 10: Extra Doors Open
    // 11: All shards collected!!
    // 12: Player collected Ring
    // 13: Sparky bossfight
    // 14: Gate opens and player can escape

    public float timer;

    public GameObject easy_agatha;
    public GameObject hard_agatha;
    public TheatreAgatha hard_agatha_script;

    public bool entered_theatre;

    public SmallTheatreDoor door_NE;
    public SmallTheatreDoor door_SE;
    public SmallTheatreDoor door_NW;
    public SmallTheatreDoor door_SW;
    public SmallTheatreDoor door_NTH;
    public SmallTheatreDoor door_STH;

    public SmallTheatreDoor door_EX_SE;
    public SmallTheatreDoor door_EX_SW;

    public SmallTheatreDoor door_EX_NE_1;
    public SmallTheatreDoor door_EX_NW_1;
    public SmallTheatreDoor door_EX_NE_2;
    public SmallTheatreDoor door_EX_NW_2;
    public SmallTheatreDoor door_EX_NE_BATH_1;
    public SmallTheatreDoor door_EX_NE_BATH_2;
    public SmallTheatreDoor door_EX_NW_GRASS;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            entered_theatre = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (current_stage == 0 && entered_theatre)
        {
            current_stage = 1;
            door_STH.is_open = false;
            Debug.Log("Entered Theatre");
        }

        if (current_stage == 1)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                current_stage = 2;
                Debug.Log("Locked Door");
            }
        }

        if (current_stage == 2)
        {
            timer += Time.deltaTime;
            if (timer > 10f)
            {
                timer = 0f;
                current_stage = 3;
                door_NE.is_open = true;
                door_SE.is_open = true;
                Debug.Log("Monolouge Start");
                easy_agatha.SetActive(true);
            }
        }

        if (current_stage == 3)
        {
            if (Mind.remaining_shards == 50)
            {
                easy_agatha.SetActive(false);
                current_stage = 4;
                entered_theatre = false;
                Debug.Log("All shards in zone 1 collected");
            }
        }

        if (current_stage == 4)
        {
            if (entered_theatre)
            {
                door_NE.is_open = false;
                door_SE.is_open = false;
                door_EX_NE_BATH_1.is_open = true;
                door_EX_NE_BATH_2.is_open = true;
                current_stage = 5;
                Debug.Log("Player is in theatre");
            }
        }

        if (current_stage == 5)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                current_stage = 6;
                Debug.Log("All doors have closed");
                hard_agatha.SetActive(true);
            }
        }

        if (current_stage == 6)
        {
            timer += Time.deltaTime;
            if (timer > 10f)
            {
                hard_agatha_script.movement_done = true;
                timer = 0f;
                current_stage = 7;
                Debug.Log("Sparky's Speech");
            }
        }

        if (current_stage == 7)
        {
            timer += Time.deltaTime;
            if (timer > 4f)
            {
                timer = 0f;
                current_stage = 8;
                door_NW.opening_speed = 1f;
                door_SW.opening_speed = 1f;
                door_NW.is_open = true;
                door_SW.is_open = true;
                
                Debug.Log("Doors around are opening");
            }
        }

        if (current_stage == 8)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                timer = 0f;
                current_stage = 9;

                door_NTH.is_open = true;
                hard_agatha_script.activated_ai = true;
                Debug.Log("Sparky's Door and others opens");
            }
        }

        if (current_stage == 9)
        {
            timer += Time.deltaTime;
            if (timer > 5f)
            {
                door_EX_NW_GRASS.is_open = true;
                door_EX_NW_1.is_open = true;
                door_EX_NW_2.is_open = true;
                door_EX_NE_1.is_open = true;
                door_EX_NE_2.is_open = true;
                door_EX_SE.is_open = true;
                door_EX_SW.is_open = true;
                current_stage = 10;
                Debug.Log("Extra doors open");
            }
        }

        if (current_stage == 10)
        {
            if (Mind.remaining_shards == 0f)
            {
                entered_theatre = false;
                current_stage = 11;
            }
        }

        if (current_stage == 11)
        {
            if (entered_theatre)
            {
                door_NE.is_open = false;
                door_SE.is_open = false;
                door_NW.is_open = false;
                door_SW.is_open = false;
                door_NTH.is_open = true;
                door_STH.is_open = false;
                door_EX_SE.is_open = true;
                door_EX_SW.is_open = true;
                door_EX_NE_1.is_open = true;
                door_EX_NW_1.is_open = true;
                door_EX_NE_2.is_open = true;
                door_EX_NW_2.is_open = true;
                door_EX_NE_BATH_1.is_open = true;
                door_EX_NE_BATH_2.is_open = true;
                door_EX_NW_GRASS.is_open = true;

                current_stage = 12;
            }
        }
    }
}
