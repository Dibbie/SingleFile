using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachine, IState
{

    public IdleState(Agent activeAgent) : base(activeAgent) { } //auto-init

    private float idleTime, rotateTime;
    private float rotateSpeed = 6f;
    private Vector3 randomRot;

    public void OnEnter()
    {
        Debug.Log("Active Agent is: " + activeAgent);
        activeAgent.GetComponent<MeshRenderer>().material.color = Color.red;
        idleTime = 0;
    }

    public void OnExit()
    {
        Debug.Log("destroying the state for transitioning");
    }

    public void Tick()
    {
        idleTime += Time.deltaTime;
        rotateTime += Time.deltaTime;
        //Debug.Log("Idle for: " + idleTime + "...");

        if (idleTime >= 3f) { activeAgent.SetState(new MoveState(activeAgent)); }
        activeAgent.TEMP_time = idleTime;

        if (rotateTime >= 0.75f)
        {
            randomRot = GetRandomVector3();
            rotateTime = 0f;
        }

        //rotate franticaly
        activeAgent.transform.rotation = Quaternion.Slerp(activeAgent.transform.rotation, Quaternion.LookRotation(randomRot), rotateSpeed * Time.deltaTime);
    }

    Vector3 GetRandomVector3()
    {
        return new Vector3(Random.Range(-360f, 360f), 0f, 0f);
    }
}
