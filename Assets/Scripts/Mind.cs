using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mind 
{

    public static int remaining_shards;
    public static int max_shards;

    public static float shard_turn_speed = 60f;

    public static float special_reveal;
    public static float special_stun;
    public static float special_magnet;

    public static bool has_ring;

    public static int maximum_lives = 3;

    public static string stext_string;
    public static float stext_float;
    public static Vector3 stext_color;

    public static void AddShard()
    {
        Mind.remaining_shards += 1;
        Mind.max_shards += 1;
    }
    
}
