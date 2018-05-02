using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallbackState : StateMachine, IState
{

    private const float minimalDist = 1f;

    #region Component Variables
    private Rigidbody rb;
    #endregion

    #region Private Variables
    private Vector3 destination;
    private float moveRate = 7.5f;
    private float rotateSpeed = 4f;
    #endregion

    public FallbackState(Agent activeAgent) : base(activeAgent) //auto-init
    {

    }

    public void OnEnter()
    {
        Debug.Log("Active Agent is: " + activeAgent);
        activeAgent.GetComponent<MeshRenderer>().material.color = Color.green;

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
            Debug.Log("Aaaand were back!");
            activeAgent.SetState(new IdleState(activeAgent));
        }

        activeAgent.transform.rotation = Quaternion.Slerp(activeAgent.transform.rotation, Quaternion.LookRotation(destination), rotateSpeed * Time.deltaTime);
        activeAgent.transform.position = Vector3.MoveTowards(activeAgent.transform.position, destination, moveRate * Time.deltaTime);
    }

    private Vector3 GetDestination()
    {
        return new Vector3(10f, activeAgent.transform.position.y, -14f);
    }

    private bool HasReachedDestination()
    {
        return Vector3.Distance(activeAgent.transform.position, destination) < minimalDist;
    }
}
