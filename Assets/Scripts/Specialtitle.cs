using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Specialtitle : MonoBehaviour
{
    public TextMeshProUGUI the_object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mind.stext_float > -1f)
        {
            Mind.stext_float -= Time.deltaTime;
        }
        the_object.text = Mind.stext_string;
        the_object.color = new Vector4(Mind.stext_color.x, Mind.stext_color.y, Mind.stext_color.z, Mind.stext_float);
    }
}
