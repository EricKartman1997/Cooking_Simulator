using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private HeroikConfig heroikConfig;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameInput gameInput;

    private float _currentAttractionCharacter;
    private Vector3 _movement;
    
    private float MoveSpeed => heroikConfig.MoveConfig.MoveSpeed;
    private float RotateSpeed => heroikConfig.MoveConfig.RotateSpeed;
    private float GravityForce => heroikConfig.MoveConfig.GravityForce;
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        _movement = new Vector3(inputVector.x,0f, inputVector.y);
    }

    private void FixedUpdate()
    {
        GravityHandling();
        MoveCharacter(_movement);
        RotateCharacter(_movement);
    }

    private void RotateCharacter(Vector3 moveDirection)
    {
        if(!characterController.isGrounded)
            return;
        if(Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, RotateSpeed * Time.deltaTime);
        }
    }

    private void MoveCharacter(Vector3 moveDirection)
    {
        moveDirection *= MoveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void GravityHandling()
    {
        if (!characterController.isGrounded)
        {
            _currentAttractionCharacter -= GravityForce * Time.deltaTime;
        }
        else
        {
            _currentAttractionCharacter = 0;
        }
    }
}

