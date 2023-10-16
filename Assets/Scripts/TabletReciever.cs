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

    // Start is called before the first frame update
    void Start()
    {
        //my_broadcaster = GameObject.FindGameObjectWithTag("Broadcaster").GetComponent<TabletBroadcaster>();
        // Debug.Break();
        notification.text = "";
    }

    public void Awarded(string item_name)
    {
        notification.text = "Well done! You unlocked the:" + '"' + item_name + '"';
        notification_time = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        thumbs_up_sprite.SetActive(notification_time > 0);
        notification_gob.SetActive(notification_time > 0);
    }
}
