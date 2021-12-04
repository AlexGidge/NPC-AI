using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private CharacterMovement CurrentPossessor;

    public void ChangePossession(CharacterMovement newPossessor)
    {
        if (newPossessor == null)
        {
            ClearPossessor();
        }
        else if(CurrentPossessor == null)
        {
            SetPosessor(newPossessor);
        } 
        else if (CurrentPossessor != newPossessor)
        {
            CurrentPossessor.BallTackled();
            SetPosessor(newPossessor);
        }
    }

    private void ClearPossessor()
    {
        CurrentPossessor = null;
        transform.SetParent(null);
    }

    private void SetPosessor(CharacterMovement newPossessor)
    {
        CurrentPossessor = newPossessor;
        transform.SetParent(newPossessor.BallPosition);
    }

    private void FixedUpdate()
    {
        if (CurrentPossessor != null && transform.parent != null)
        {
            //TODO: Add force in direction of 0,0,0
            transform.localPosition = Vector3.zero;
        }
    }

    public void DropBall(CharacterMovement characterMovement)
    {
        if (characterMovement != null && characterMovement == CurrentPossessor)
        {
            ClearPossessor();
        }
    }
}