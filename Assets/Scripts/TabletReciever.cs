using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletReciever : MonoBehaviour
{

    public TabletBroadcaster my_broadcaster;

    public bool player_is_close;

    // Start is called before the first frame update
    void Start()
    {
        my_broadcaster = GameObject.FindGameObjectWithTag("Broadcaster").GetComponent<TabletBroadcaster>();
        Debug.Break();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
