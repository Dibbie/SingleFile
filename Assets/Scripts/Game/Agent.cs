using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    public float TEMP_time;

    IState currentState;

    // Use this for initialization
    void Awake()
    {
        //do stuff for state setup
        SetState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Tick();
    }

    public void SetState(IState transitionState)
    {
        currentState = transitionState;
        currentState.OnEnter();
        if (currentState != null) { currentState.OnExit(); }
    }
}
