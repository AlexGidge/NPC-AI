using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D CharacterBody;
    private float MoveSpeed;
    private float RotationSpeed;
    private bool initialised;

    public Transform BallPosition;

    public GameObject TargetObject { get; private set; }
    public Quaternion TargetRotation { get; set; }
    public BallController BallInPossession { get; set; }

    public void Initialise(Rigidbody2D rigidBody, float moveSpeed, float rotationSpeed)
    {
        CharacterBody = rigidBody;
        MoveSpeed = moveSpeed;
        RotationSpeed = rotationSpeed;
        initialised = true;
    }

    public bool HasBall()
    {
        return BallInPossession != null;
    }

    public void PickupBall(BallController ball)
    {
        if (!HasBall())
        {
            BallInPossession = ball;
            ball.ChangePossession(this);
        }
    }

    public void BallTackled()
    {
        BallInPossession = null;
    }

    public bool HasActiveTarget()
    {
        return (TargetObject != null);
    }
    
    public void ClearTarget()
    {
        TargetObject = null;
        BallInPossession?.DropBall(this);
        BallInPossession = null;
        SetLookPoint(Vector3.zero);
    }

    public void ApplyMovement(Vector2 movement)
    {
        CharacterBody.AddForce(movement * MoveSpeed * Time.deltaTime);
    }

    public void MoveInDirectionOfTarget()
    {
        ApplyMovement(TargetRotation * Vector2.up);
    }

    public void LookInDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            TargetRotation = VectorHelper.Calculate2DLookRotation(new Vector3(0, 0, 0), direction);
        }
    }

    public void SetLookPoint(Vector3 point)
    {
        TargetRotation = VectorHelper.Calculate2DLookRotation(transform.position, point);//TODO: Refactor
    }
    
    public void Rotate()
    {
        if (1 - Mathf.Abs(Quaternion.Dot(transform.rotation, TargetRotation)) < 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, RotationSpeed);
        }
    }

    public void SetLookTarget(GameObject go)
    {
        TargetObject = go;//TODO: SetLookTarget vs SetLookPoint 
    }

    public void SetRotationForTarget()
    {
        if (HasActiveTarget())
        {
            TargetRotation = VectorHelper.Calculate2DLookRotation(transform.position, TargetObject.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ball"))
        {
            BallController ballController = other.collider.gameObject.GetComponent<BallController>();
            PickupBall(ballController);
        }
    }
}
