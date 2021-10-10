using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public CharacterMovement CharacterMovement;

    public Rigidbody2D PlayerRigidBody;
    public float PlayerMoveSpeed;

    [SerializeField] private Vector2 currentMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        RegisterEvents();
        CharacterMovement.Initialise(PlayerRigidBody, PlayerMoveSpeed);
    }

    private void RegisterEvents()
    {
        PlayerInput.Events.Movement += OnMovement;
        EngineManager.Current.Events.EveryPhysicsUpdate += ApplyMovement;
    }

    private void OnMovement(Vector2 value)
    {
        currentMovement = value;
    }
    
    private void ApplyMovement()
    {
        CharacterMovement.ApplyMovement(currentMovement);
    }
}
