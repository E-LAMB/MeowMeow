using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{

    public UnityEvent pushed_down;
    public UnityEvent pushed_up;

    float push_threshold;
    float push_deadzone;

    public bool currently_pressed;
    public Vector3 start_position;
    public ConfigurableJoint joint;

    private void Start()
    {
        start_position = transform.localPosition;
    }

    private void Update()
    {
        if (!currently_pressed && CalculateValue() + push_threshold >= 1) { Is_pressed(); }

        if (currently_pressed && CalculateValue() - push_threshold <= 0) { Is_released(); }
    }

    float CalculateValue()
    {
        float value = Vector3.Distance(start_position, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < push_deadzone) { value = 0f; }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Is_pressed()
    {
        Debug.Log("P");
        pushed_down.Invoke();
    }

    void Is_released()
    {
        Debug.Log("R");
        pushed_up.Invoke();
    }

}
