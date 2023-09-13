using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgathaScript : MonoBehaviour
{
    public bool easy_mode;

    public Transform boundary_a;
    public Transform boundary_b;

    public int location_priority;
    // 0 = A location selected by the wander script. This location never warrents a teleport.
    // 1 = A suspected sound made by a door opening (specifically by the player). This location does not warrent a teleport.
    // 2 = The location the player was last spotted. If it is too far away, Agatha will teleport to them.

    public NavMeshPath path_data;

    public float teleport_cooldown_maxi;
    public float teleport_cooldown;
    public bool currently_teleporting;

    public float memory_time; // The time where Agatha remembers the player's position

    public float chase_time; // The amount of time Agatha has been in chase for
    public bool is_in_chase; // Is Agatha in chase?

    public float target_distance;

    public Vector3 proposed_location;

    public bool can_see_player;
    public bool can_see_wall;
    public LayerMask player_layer;
    public LayerMask wall_layer;
    public LayerMask floor_layer;

    public Vector3 player_direction;

    public Vector3 target_location;

    public NavMeshAgent my_agent;

    public GameObject player_object;
    public Transform player_transform;

    public GameObject self_object;
    public Transform self_transform;

    public float movement_speed;
    public float wander_speed;
    public float chasing_speed;

    public float player_distance;

    public float teleport_timer;
    public Vector3 teleport_location;

    public Transform bean_indicator;

    public Vector3 looking_location;

    public bool can_teleport_infront;
    public bool is_looking_teleport;

    public SkinnedMeshRenderer myself;
    public GameObject map_sprite;

    public Animator my_animator;
    public int anim_speed;

    public Transform player_seeker;

    public bool leaving_ground;

    public float time_since_tp_patrol;

    public Light head;
    public ParticleSystem head_particles;

    public float brightness;

    public float chase_volume;
    public AudioSource chasing_music;
    public AudioSource wander_music;

    public float time_since_spoken;

    public AudioClip[] clips_taunt;
    public AudioClip[] clips_sight;
    public AudioClip[] clips_warp;
    public AudioClip[] clips_stunned;
    
    public AudioSource my_voicebox;
    public AudioClip chosen_clip;

    public bool played_stun_clip;

    public ParticleSystemRenderer particle_renderer;

    public Material active_material;
    public Material stunned_material;

    public Color active_color;
    public Color stunned_color;

    public Collider my_collider;

    public JumpscareScript my_jumpscare;

    public Light enrage_light;

    public bool has_enraged;

    public float shut_up_time;

    public float shard_speed_modifier;
    // 1  =  +180 speed at the end
    // 2  =  + 90 speed at the end
    // (divides by that number)
    // 180 = + 1 speed at the end! (Still insane!!) 

    public AudioSource tr;

    public bool tr_shrinking;

    //public float time_since_seen;
    public void Speak(string type)
    {
        // Random (taunts)
        // Sight
        // Warp
        // Stunned

        bool should_play;

        if (Mind.special_stun > 0f && type != "Stunned")
        {
            should_play = false;
        } else
        {
            should_play = true;
        }

        if (type == "Random")
        {
            chosen_clip = clips_taunt[Random.Range(0, clips_taunt.Length)];
        }
        if (type == "Sight")
        {
            chosen_clip = clips_sight[Random.Range(0, clips_sight.Length)];
        }
        if (type == "Warp")
        {
            chosen_clip = clips_warp[Random.Range(0, clips_warp.Length)];
        }
        if (type == "Stunned")
        {
            chosen_clip = clips_stunned[Random.Range(0, clips_stunned.Length)];
        }

        if (should_play)
        {
            time_since_spoken = 0f;
            my_voicebox.clip = chosen_clip;
            my_voicebox.Play();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Death");
            my_jumpscare.InitiateJumpscare();
        }
    }

    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        my_jumpscare = GameObject.FindGameObjectWithTag("Jumpscare").GetComponent<JumpscareScript>();
        enrage_light = GameObject.FindGameObjectWithTag("Enrage").GetComponent<Light>();
        player_transform = player_object.GetComponent<Transform>();
        // Mind.my_enemy = gameObject.GetComponent<AgathaScript>();
        brightness = 50f;
        // target_location = self_transform.position;
        tr.volume = 0f;
    }

    public void Alerted(Vector3 position, int priority)
    {
        if (priority >= location_priority)
        {
            location_priority = priority;
            target_location = position;
        }
    }

    public void HasLeftGround()
    {
        leaving_ground = false;
        // Debug.Log("LEFT!");
    }

    public void BeginTeleport(Vector3 position)
    {
        if (teleport_cooldown < 0f && !currently_teleporting && !easy_mode)
        {
            currently_teleporting = true;
            my_agent.enabled = false;
            teleport_location = position;
            teleport_timer = 0f;
            my_animator.SetBool("Warp Complete", false);
            my_animator.SetTrigger("Begin Warp");
            //Debug.Break();
            leaving_ground = true;
            tr_shrinking = true;
        }
    }

    public void CeaseTeleport()
    {
        //Debug.Log(Vector3.Distance(player_transform.position, looking_location));
        if (Vector3.Distance(player_transform.position, looking_location) > 5f && is_looking_teleport)
        {
            teleport_location = looking_location;
        }
        if (is_looking_teleport)
        {
            Speak("Warp");
        }
        self_transform.position = teleport_location;

        player_seeker.position = self_transform.position;
        player_seeker.LookAt(player_transform.position);

        Vector3 looking_angle = self_transform.eulerAngles;
        looking_angle.x = 0f;
        looking_angle.y = 0f;
        looking_angle.z = looking_angle.z + 90f;
        self_transform.eulerAngles = looking_angle;

        currently_teleporting = false;
        my_agent.enabled = true;
        teleport_cooldown = teleport_cooldown_maxi;
        teleport_timer = 0f;
        is_looking_teleport = false;
        tr_shrinking = false;
        my_animator.SetBool("Warp Complete", true);
    }

    public void TryFindDestination()
    {
        proposed_location = self_transform.position;
        proposed_location.x = Random.Range(boundary_a.position.x, boundary_b.position.x);
        proposed_location.z = Random.Range(boundary_a.position.z, boundary_b.position.z);

        float proposed_distance = Vector3.Distance(self_transform.position, proposed_location);
        float proposed_to_player = Vector3.Distance(self_transform.position, player_transform.position);

        // my_agent.CalculatePath(proposed_location, path_data);

        if (Physics.Raycast(proposed_location, -transform.up, 2f, floor_layer) && proposed_to_player < 140f)
        {
            Alerted(proposed_location, 0);
            if (time_since_tp_patrol > 35f && player_distance > 35f && proposed_to_player < 15f && proposed_distance > 40f)
            {
                time_since_tp_patrol = 0f;
                is_looking_teleport = false;
                //Debug.Log("Teleported on Patrol");
                BeginTeleport(proposed_location);
            }
        }
    }

    void Update()
    {
        if (shut_up_time > 0f)
        {
            my_voicebox.volume = 0f;
            shut_up_time -= Time.deltaTime;
        } else
        {
            my_voicebox.volume = 0.8f;
        }

        if (!currently_teleporting && Mind.special_stun <= 0f && !leaving_ground)
        {
            my_collider.enabled = true;
        } else
        {
            my_collider.enabled = false;
        }

        time_since_spoken += Time.deltaTime;
        //time_since_seen += Time.deltaTime;

        if (time_since_spoken > 35f)
        {
            Speak("Random");
        }

        if (currently_teleporting)
        {
            brightness -= Time.deltaTime * 60f;
            if (0f > brightness)
            {
                brightness = 0f;
            }
        }

        if (!currently_teleporting && leaving_ground)
        {
            player_seeker.position = self_transform.position;
            player_seeker.LookAt(player_transform.position);

            Vector3 looking_angle = self_transform.eulerAngles;
            looking_angle.x = 0f;
            looking_angle.y = 0f;
            self_transform.eulerAngles = looking_angle;

            if (is_looking_teleport)
            {
                target_location = player_transform.position;
            }

            brightness += Time.deltaTime * 100f;
            if (30f < brightness)
            {
                brightness = 30f;
            }
        }

        time_since_tp_patrol += Time.deltaTime;

        if (teleport_timer < 2f && currently_teleporting)
        {
            teleport_timer += Time.deltaTime;
            if (teleport_timer > 1f)
            {
                self_transform.position = teleport_location;
                CeaseTeleport();
            }
        }

        if (-1f < teleport_cooldown)
        {
            teleport_cooldown -= Time.deltaTime;
        }

        player_direction = (self_transform.position - player_transform.position);
        player_direction = player_direction * -1f;

        player_distance = Vector3.Distance(self_transform.position, player_transform.position);
        target_distance = Vector3.Distance(self_transform.position, target_location);

        can_see_player = Physics.Raycast(self_transform.position, player_direction, player_distance, player_layer);
        can_see_wall = Physics.Raycast(self_transform.position, player_direction, player_distance, wall_layer);

        if (Mind.remaining_shards == 0)
        {
            can_see_wall = false;
        }

        if (memory_time > -1f && !currently_teleporting)
        {
            memory_time -= Time.deltaTime;
        }

        Debug.DrawRay(self_transform.position, player_direction);

        if (can_see_player && !can_see_wall && Mind.special_stun < 0f)
        {
            if (easy_mode)
            {
                memory_time = 5f;
            } else
            {
                memory_time = 5.75f;
            }
            
            if (chase_time < 6f)
            {
                chase_time = 6f;
            }
            chase_time += Time.deltaTime * 1.5f;
            if (!is_in_chase)
            {
                Speak("Sight");
            }
        } else
        {
            chase_time -= Time.deltaTime;
        }

        if (chase_time > 14f)
        {
            chase_time = 14f;
        }

        if (chase_time > 0f)
        {
            is_in_chase = true;
        } else
        {
            is_in_chase = false;
        }

        if (is_in_chase && chase_time > 6f)
        {
            if (player_distance > 30f)
            {
                is_looking_teleport = true;
                BeginTeleport(looking_location);
            }
        }

        if (is_in_chase)
        { 
            chase_volume += Time.deltaTime / 2f;
            if (1f < chase_volume)
            {
                chase_volume = 1f;
            }
            movement_speed = chasing_speed + ((Mind.remaining_shards / Mind.max_shards) * shard_speed_modifier);
            anim_speed = 2;
        } else
        {
            chase_volume -= Time.deltaTime / 2f;
            if (0f > chase_volume)
            {
                chase_volume = 0f;
            }
            movement_speed = wander_speed + ((Mind.remaining_shards / Mind.max_shards) * shard_speed_modifier);
            anim_speed = 1;
        }

        if (currently_teleporting)
        {
            movement_speed = 0f;
            anim_speed = 0;
        }

        if (target_distance < 1.5f)
        {
            location_priority = -1;
            TryFindDestination();
        }

        if (memory_time > 0f)
        {
            Alerted(player_transform.position, 2);
        }

        my_agent.speed = movement_speed;

        if (Mind.special_reveal > 0f)
        {
            map_sprite.SetActive(true);
        } else
        {
            map_sprite.SetActive(false);
        }

        if (Mind.special_stun > 1f && !played_stun_clip)
        {
            played_stun_clip = true;
            Speak("Stunned");
        }
        if (Mind.special_stun < 0f)
        {
            played_stun_clip = false;
        }

        if (Mind.special_stun > 0f)
        {
            my_agent.speed = 0;
            anim_speed = 0;
            particle_renderer.material = stunned_material;
            head.color = stunned_color;
        } else
        {
            particle_renderer.material = active_material;
            head.color = active_color;
        }

        if (leaving_ground)
        {
            my_agent.speed = 0;
            anim_speed = 0;
        }

        if (!currently_teleporting)
        {
            my_agent.SetDestination(target_location);
        }

        /*
        if (tr_shrinking)
        {
            tr.volume -= Time.deltaTime * 20f;
            if (tr.volume < 0f)
            {
                tr.maxDistance = 0f;
            }
        } else
        {
            tr.volume += Time.deltaTime * 20f;
            if (tr.volume > 35f)
            {
                tr.maxDistance = 35f;
            }
        }
        */

        chasing_music.volume = chase_volume;
        wander_music.volume = 1f - chase_volume;

        chasing_music.volume = chasing_music.volume / 0.8f;
        wander_music.volume = wander_music.volume / 0.8f;

        my_animator.SetInteger("Current Speed", anim_speed);
        bean_indicator.position = looking_location;

        head.range = brightness;

        if (Mind.remaining_shards == 0)
        {
            enrage_light.enabled = true;
            memory_time = 10f;
            chase_time = 10f;
            has_enraged = true;
        }

    }
}
