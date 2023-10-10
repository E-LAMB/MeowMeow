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

    public int look_progress;
    public int look_state;
    // 0 = Waiting For Completion
    // 1 = Hiding Copy + Dupes
    // 2 = Shutter Hidden - Swapping
    // 3 = Opening Shutter
    public Transform look_curtain;
    public Transform look_copies;
    public float look_shutter_size;
    public float look_shutter_wait;
    public float look_copies_y;
    public LookPuzzle lookPuzzle;

    public CosmeticsManager cosmetics_manager;

    // Start is called before the first frame update
    void Start()
    {
        look_progress = 1;
        lookPuzzle.SetUpGame(look_progress);
    }

    public void AwardCosmetic()
    {
        cosmetics_manager.RewardRandomCosmetic();
    }

    public void CompletedLook()
    {

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

        if (look_state == 1)
        {
            if (look_shutter_size > 0f) { look_shutter_size -= Time.deltaTime; }
            if (look_shutter_size < 0f) { look_shutter_size = 0f; }

            look_copies_y = -8.75f + (look_shutter_size * 4f);

            look_curtain.localScale = new Vector3(1.8f - look_shutter_size, 1f, 1f);
            look_copies.localPosition = new Vector3(0f, look_copies_y, 0f);

            if (look_shutter_size == 0)
            {
                look_state = 2;
                look_shutter_wait = 1f;
                lookPuzzle.SetUpGame(look_progress);
            }
        }

        if (look_state == 2)
        {
            look_shutter_wait -= Time.deltaTime;
            if (0f > look_shutter_wait && look_progress < 7)
            {
                look_state = 3;
            }
        }

        if (look_state == 3)
        {

            if (look_shutter_size < 1.8f) { look_shutter_size += Time.deltaTime; }
            if (look_shutter_size > 1.8f) { look_shutter_size = 1.8f; }

            look_copies_y = -8.75f + (look_shutter_size * 4f);

            look_curtain.localScale = new Vector3(1.8f - look_shutter_size, 1f, 1f);
            look_copies.localPosition = new Vector3(0f, look_copies_y, 0f);

            if (look_shutter_size == 1.8f)
            {
                look_state = 0;
            }
        }
    }
}
