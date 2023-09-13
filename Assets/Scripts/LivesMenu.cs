using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class LivesMenu : MonoBehaviour
{

    public GameObject[] canvas_objects;
    public int remaining_lives;

    public float fader_time;

    public RawImage background;

    public GameObject restart_option;

    public GameObject[] skulls;

    public TextMeshProUGUI advice_text;
    public string[] all_advice;

    public JumpscareScript my_scarer;

    public GameObject player;

    public GameObject agatha_body;
    public NavMeshAgent agatha_agent;
    public Transform agatha_spawnpoint;
    public AgathaScript agatha_script;

    public AudioClip[] death_clips;
    public AudioSource my_source;
    public AudioListener death_listener;

    public PlayerController to_zero;

    public int menu_state;
    // 0 = Not active
    // 1 = Got jumpscared
    // 2 = Faded in
    // 3 = Lives show
    // 4 = Lives are taken away
    // 5 = Lives vanish
    // 6 = Player respawns (then reverts to 0)

    // Start is called before the first frame update
    void Start()
    {
        remaining_lives = Mind.maximum_lives;
        advice_text.enabled = false;
        skulls[0].SetActive(false);
        skulls[1].SetActive(false);
        skulls[2].SetActive(false);
        skulls[3].SetActive(false);
        advice_text.color = new Vector4(1f, 1f, 1f, 0f);
        background.color = new Vector4(0f, 0f, 0f, 0f);
        death_listener.enabled = false;
    }

    void RestartLevel()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (menu_state == 0)
        {
            if (my_scarer.playing_jumpscare)
            {
                for (int i = 0; i < canvas_objects.Length; i++)
                {
                    canvas_objects[i].SetActive(false);
                }
                advice_text.text = all_advice[Random.Range(0, all_advice.Length)];
                menu_state = 1;
            }
        }

        if (menu_state == 1)
        {
            if (my_scarer.playing_jumpscare && my_scarer.animation_time > 2.2f)
            {
                menu_state = 2;
                fader_time = 0f;
                advice_text.enabled = true;
            }
        }

        if (menu_state == 2)
        {
            fader_time += Time.deltaTime;
            if (fader_time > 1.5f)
            {
                menu_state = 6;
                fader_time = 0f;
                if (remaining_lives == 1)
                {
                    advice_text.text = "YOU ARE DEAD";
                }
                advice_text.color = new Vector4(1f, 1f, 1f, 1f);
                background.enabled = true;
                background.color = new Vector4(0f, 0f, 0f, 1f);
                my_scarer.playing_jumpscare = false;
                my_scarer.animation_time = 0f;
                skulls[0].SetActive(false);
                skulls[1].SetActive(false);
                skulls[2].SetActive(false);
                skulls[3].SetActive(false);
                remaining_lives -= 1;
                skulls[remaining_lives].SetActive(true);
            }
        }

        if (menu_state == 6)
        {
            fader_time += Time.deltaTime;
            if (fader_time > 1f)
            {
                menu_state = 3;
                fader_time = 0f;
                my_source.clip = death_clips[Random.Range(0, death_clips.Length)];
                death_listener.enabled = true;
                my_source.Play();
            }
        }

        if (menu_state == 3)
        {
            fader_time += Time.deltaTime;
            if (fader_time > 5f)
            {
                if (remaining_lives == 0)
                {
                    //Application.Quit();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                }
                menu_state = 4;
            }
        }

        if (menu_state == 4)
        {
            fader_time -= Time.deltaTime;

            if (fader_time < 0f)
            {
                menu_state = 5;

                skulls[0].SetActive(false);
                skulls[1].SetActive(false);
                skulls[2].SetActive(false);
                skulls[3].SetActive(false);

                death_listener.enabled = false;

                player.SetActive(true);
                player.transform.position = new Vector3(0f, 1.1f, -19f);

                to_zero.yaw = 0;
                to_zero.pitch = 0;

                agatha_body.SetActive(true);
                agatha_agent.enabled = false;
                agatha_body.transform.position = agatha_spawnpoint.position;
                agatha_agent.enabled = true;

                agatha_script.can_see_player = false;
                agatha_script.shut_up_time = 1f;
                agatha_script.my_voicebox.Stop();
                agatha_script.chase_volume = 0f;

                agatha_script.memory_time = -1f;
                agatha_script.chase_time = -1f;

                Mind.special_reveal = -1f;
                Mind.special_stun = -1f;

                my_scarer.my_cam.enabled = false;

                for (int i = 0; i < my_scarer.to_disable.Length; i++)
                {
                    my_scarer.to_disable[i].SetActive(true);
                }
                for (int i = 0; i < canvas_objects.Length; i++)
                {
                    canvas_objects[i].SetActive(true);
                }

                fader_time = 1f;
            }
        }

        if (menu_state == 5)
        {
            fader_time -= Time.deltaTime / 2.5f;
            background.color = new Vector4(0f, 0f, 0f, fader_time);
            advice_text.color = new Vector4(1f, 1f, 1f, 0f);
            if (fader_time < -0.1f)
            {
                menu_state = 0;
            }
        }
    }
}
