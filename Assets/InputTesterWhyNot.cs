using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class InputTesterWhyNot : MonoBehaviour
{

    public Transform self;

    public float size;
    public bool direction;

    // Start is called before the first frame update
    void Start()
    {
        // Mind.target_position = self;
    }

    private void Update()
    {
        if (direction) { size += Time.deltaTime; } else { size -= Time.deltaTime; }

        if (size < 0.3f) { size = 0.3f;}
        if (size > 10f) { size = 10f; }

        self.localScale = new Vector3(size, size, size);

        //if (Mind.move_to_target)
        //{
        //    self.position = Vector3.MoveTowards(self.position, Mind.target_position.position, Time.deltaTime);
        //}

    }

    // Update is called once per frame
    public void DOTHING(bool setme)
    {
        direction = setme;
    }
}
