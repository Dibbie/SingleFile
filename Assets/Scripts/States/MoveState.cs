using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateMachine, IState
{

    private const float minimalDist = 1f;

    #region Private Variables
    private Vector3 destination;
    private float moveRate = 5f;
    private float rotateSpeed = 2f;
    #endregion

    public MoveState(Agent activeAgent) : base(activeAgent) //auto-init
    {
        
    } 

    public void OnEnter()
    {
        Debug.Log("Active Agent is: " + activeAgent);
        activeAgent.GetComponent<MeshRenderer>().material.color = Color.blue;

        destination = GetDestination();
    }

    public void OnExit()
    {
        Debug.Log("destroying the state for transitioning");
    }

    public void Tick()
    {
        if (HasReachedDestination())
        {
            Debug.Log("We made it!");
            activeAgent.SetState(new FallbackState(activeAgent));
        }

        activeAgent.transform.rotation = Quaternion.Slerp(activeAgent.transform.rotation, Quaternion.LookRotation(destination), rotateSpeed * Time.deltaTime);
        activeAgent.transform.position = Vector3.MoveTowards(activeAgent.transform.position, destination, moveRate * Time.deltaTime);
    }

    private Vector3 GetDestination()
    {
        return new Vector3(-10f, activeAgent.transform.position.y, 14f);
    }

    private bool HasReachedDestination()
    {
        return Vector3.Distance(activeAgent.transform.position, destination) < minimalDist;
    }
}
