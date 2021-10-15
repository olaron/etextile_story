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
    }

    public void StatusInRange()
    {
        _lastTimeStatusInRange = Time.time;
        if (!isOpen)
            isOpen = true;
    }
}