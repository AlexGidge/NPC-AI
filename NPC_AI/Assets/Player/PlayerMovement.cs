using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput PlayerInput;
    
    // Start is called before the first frame update
    void Start()
    {
        RegisterInputEvents();
    }

    private void RegisterInputEvents()
    {
        PlayerInput.Events.Movement += OnMovement;
    }

    private void OnMovement(Vector2 value)
    {
        Debug.Log($"Movement: {value}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
