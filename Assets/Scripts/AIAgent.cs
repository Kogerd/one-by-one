﻿using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public float maxSpeed = 3.5f;
    public float maxAccel = 100;
    public float maxRotation = 200f;
    public float maxAngularAccel = 10000f;
    public float orientation;
    public float rotation;
    public Vector2 velocity;
    protected EnemySteering steering;


    private void Start()
    {
        velocity = Vector2.zero;
        steering = new EnemySteering();
        
        orientation = -transform.rotation.eulerAngles.z;
        rotation = 0;
    }

    public void SetSteering(EnemySteering steering, float weight)
    {
        this.steering.linear += steering.linear * weight;
        this.steering.angular += steering.angular * weight;
    }

    protected virtual void Update()
    {
        if (Pause.Paused) return;

        Vector2 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        
        orientation %= 360.0f;
        if (orientation < 0.0f)
        {
            orientation += 360.0f;
        }
        transform.Translate(displacement, Space.World);
        transform.rotation = Quaternion.Euler(0, 0, -orientation);
        
        var behaviors = GetComponents<EnemyBehavior>();
        foreach (var i in behaviors)
        {
            i.CalledUpdate();
        }

        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }
        steering = new EnemySteering();
    }

    // private Dictionary<int, List<EnemySteering>> groups;
}
