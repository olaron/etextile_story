using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    public float MaxSpeed = 3f;
    public float Acceleration = 0.5f; // speed per second
    public float Decceleartion = 2f;
    
    private PathFollower PathFollower;
    private PathStep CurrentPathStep;

    void Awake()
    {
        PathFollower = GetComponent<PathFollower>();
    }

    private void Start()
    {
        AkSoundEngine.PostEvent("play_dialogue_intro", gameObject);
        AkSoundEngine.PostEvent("play_mood_calm", gameObject);
    }

    void Update()
    {
        if (CurrentPathStep)
        {
            float currentSpeed = CurrentPathStep.isOpen ? MaxSpeed : 0f;
            float currentAcceleration = CurrentPathStep.isOpen ? Acceleration : Decceleartion;
            PathFollower.speed = Mathf.MoveTowards(PathFollower.speed, currentSpeed, currentAcceleration * Time.deltaTime);
        }
        else
        {
            PathFollower.speed = Mathf.MoveTowards(PathFollower.speed, MaxSpeed, Acceleration * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PathStep step = other.gameObject.GetComponent<PathStep>();
        if (step)
        {
            CurrentPathStep = step;
            step.PlayerEnterZone(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PathStep step = other.gameObject.GetComponent<PathStep>();
        if (step)
        {
            CurrentPathStep = null;
            step.PlayerExitZone(this);
        }
    }
}