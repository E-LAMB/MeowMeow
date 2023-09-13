using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareScript : MonoBehaviour
{

    public GameObject[] to_disable;

    public bool playing_jumpscare;

    public AnimationCurve my_curve;
    public AnimationCurve my_curve_2;

    public float animation_time;

    public Camera my_cam;

    public Transform my_position;

    public AudioListener my_listener;

    public Vector3 initial;

    public LivesMenu my_lives;

    public AudioSource screamer;

    public void InitiateJumpscare()
    {
        playing_jumpscare = true;
        for (int i = 0; i < to_disable.Length; i++)
        {
            to_disable[i].SetActive(false);
        }
        my_cam.enabled = true;
        my_listener.enabled = true;
        screamer.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        initial = my_position.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing_jumpscare)
        {
            my_position.position = new Vector3(initial.x, initial.y + my_curve_2.Evaluate(animation_time), initial.z + my_curve.Evaluate(animation_time));
            animation_time += Time.deltaTime * 1.5f;
            my_position.eulerAngles = new Vector3 (Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        }
    }
}
