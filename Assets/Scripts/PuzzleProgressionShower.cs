using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public TabletReciever match_receiver;
    public TabletReciever look_receiver;

    // Start is called before the first frame update
    void Start()
    {
        look_progress = 1;
        lookPuzzle.SetUpGame(look_progress);
    }

    // Flip the tiles to pair the matching items together.Match them all together to win!

    public void CompletedMatch()
    {
        if (Mind.total_solves == 4 || Mind.total_solves == 8)
        {
            match_receiver.UnlockedPuzzle();
        } else
        {
            match_receiver.Awarded(cosmetics_manager.RewardRandomCosmetic());
        }

        if (match_progress == 3)
        {
            match_receiver.the_description.text = "Flip the tiles to pair the matching items together. Match them all together to win! Make sure to match up The Anomaly last!";
        }
    }

    public void CompletedLook()
    {
        if (Mind.total_solves == 4 || Mind.total_solves == 8)
        {
            match_receiver.UnlockedPuzzle();
        }
        else
        {
            look_receiver.Awarded(cosmetics_manager.RewardRandomCosmetic());
        }
    }

    // Update is called once per frame
    void Update()
    {

        match_receiver.completion.text = Mathf.RoundToInt(match_progress / 6f).ToString() + "%";
        look_receiver.completion.text = Mathf.RoundToInt((look_progress - 1f) / 6f).ToString() + "%";

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
            if (0f > look_shutter_wait && look_progress < 8)
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
