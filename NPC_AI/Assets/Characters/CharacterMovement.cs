using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D CharacterBody;
    private float MoveSpeed;
    private bool initialised;

    public void Initialise(Rigidbody2D rigidBody, float moveSpeed)
    {
        CharacterBody = rigidBody;
        MoveSpeed = moveSpeed;
        initialised = true;
    }
    
    public void ApplyMovement(Vector2 movement)
    {
        if (initialised)
        {
            CharacterBody.AddForce(movement * MoveSpeed);
        }
        else
        {
            Debug.LogError("CharacterMovement not yet initialised.");
        }
    }
}
