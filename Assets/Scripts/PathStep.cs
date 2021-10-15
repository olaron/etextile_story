using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathStep : MonoBehaviour
{
 
    private float _lastTimeStatusInRange = float.MinValue;
    public float _statusDelayBeforeDisapear = 1f; // time without any variation to switch state

    public float PathSpeed = 3f;
    public string chapterName;
    public GameObject Representation;
    private bool _isOpen = false;

    private float _dialogueTriggerTime;
    private bool _dialogueTriggered = false;

    public List<float> MoodsTimes;
    public List<String> Moods;

    private int _nextMood = 0;
    
    public bool isOpen
    {
        get { return _isOpen; }
        set
        {
            _isOpen = value;
            Representation.SetActive(value);
        }
    }
    
    void Awake()
    {
        Representation.SetActive(isOpen);
    }

    void Update()
    {
        bool timePassed = _lastTimeStatusInRange + _statusDelayBeforeDisapear < Time.time;
        if (timePassed && isOpen)
            isOpen = false;
        if (_dialogueTriggered && _nextMood < MoodsTimes.Count)
        {
            if (Time.time - _dialogueTriggerTime > MoodsTimes[_nextMood])
            {
                AkSoundEngine.PostEvent(Moods[_nextMood], gameObject);
                _nextMood += 1;
            }
        }
        
    }

    public void PlayerEnterZone(PathMover mover)
    {
        // ?
    }

    public void PlayerExitZone(PathMover mover)
    {
        Debug.Log("Play audio : " + chapterName);
        AkSoundEngine.PostEvent(chapterName, gameObject);
        mover.MaxSpeed = PathSpeed;
        _dialogueTriggerTime = Time.time;
        _nextMood = 0;
        _dialogueTriggered = true;
    }

    public void StatusInRange()
    {
        _lastTimeStatusInRange = Time.time;
        if (!isOpen)
            isOpen = true;
    }
}