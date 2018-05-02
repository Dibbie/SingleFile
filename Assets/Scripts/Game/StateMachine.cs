using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine {

    protected Agent activeAgent;

    protected StateMachine(Agent activeAgent)
    {
        this.activeAgent = activeAgent;
    }
}
