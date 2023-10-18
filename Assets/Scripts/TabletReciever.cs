using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabletReciever : MonoBehaviour
{

    public bool player_is_close;

    public TextMeshPro notification;
    public GameObject thumbs_up_sprite;
    public GameObject notification_gob;
    public float notification_time;

    public GameObject other_things;

    public TextMeshPro the_description;
    public TextMeshPro completion;

    // Start is called before the first frame update
    void Start()
    {
        //my_broadcaster = GameObject.FindGameObjectWithTag("Broadcaster").GetComponent<TabletBroadcaster>();
        // Debug.Break();
        notification.text = "";
    }

    public void Awarded(string item_name)
    {
        notification.text = "Well done! You unlocked the:" + '"' + item_name + '"' + "! Why don't you see how it looks?";
        notification_time = 7f;
    }

    public void UnlockedPuzzle()
    {
        notification.text = "A new puzzle seems to have opened up in The Aquarium, Maybe you should go and try it out!";
        notification_time = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        notification_time -= Time.deltaTime;
        thumbs_up_sprite.SetActive(notification_time > 0);
        notification_gob.SetActive(notification_time > 0);
        other_things.SetActive(notification_time < 0);
        notification.enabled = notification_time > 0;
    }
}
