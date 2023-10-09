using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleProgressionShower : MonoBehaviour
{

    public int match_progress;
    public GameObject[] match_puzzles;
    public int match_state;
    // 0 = Waiting For Completion
    // 1 = Hiding Shutter
    // 2 = Shutter Hidden - Swapping
    // 3 = Opening Shutter
    public Transform match_shutter;
    public float match_shutter_rotation;
    public float match_shutter_wait;

    public CosmeticsManager cosmetics_manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AwardCosmetic()
    {
        cosmetics_manager.RewardRandomCosmetic();
    }

    // Update is called once per frame
    void Update()
    {
        if (match_state == 1)
        {
            match_shutter_rotation += Time.deltaTime * 60f;
            match_shutter.localEulerAngles = new Vector3(180f, 270f, match_shutter_rotation);

            if (match_shutter_rotation > 90f)
            {
                match_state = 2;
                match_shutter_wait = 1f;
                match_puzzles[match_progress].SetActive(false);
                match_progress += 1;
                match_puzzles[match_progress].SetActive(true);
            }
        }
        if (match_state == 2)
        {
            match_shutter_wait -= Time.deltaTime;
            if (0f > match_shutter_wait)
            {
                match_state = 3;
            }
        }
        if (match_state == 3)
        {
            match_shutter_rotation -= Time.deltaTime * 60f;
            match_shutter.localEulerAngles = new Vector3(180f, 270f, match_shutter_rotation);
            if (match_shutter_rotation < -30f)
            {
                match_state = 0;
            }
        }
    }
}
