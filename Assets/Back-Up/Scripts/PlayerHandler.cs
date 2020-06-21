using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInputActions inputActions;

    Vector2 movementInput;
    Vector2 lookPosition;

    private Animator animator;
    private Vector2 mLastDirection;

    public float keyFrameDelta = 1.5f;

    public Transform PlayerModel;

    public float RotationSpeed = 0f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        mLastDirection = new Vector2(0, 0);
    }
    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.Direction.performed += Direction_performed;
        inputActions.PlayerControls.Move.performed += Move_performed; 

    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        movementInput = obj.ReadValue<Vector2>();
    }

    private void Direction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        lookPosition = obj.ReadValue<Vector2>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (layerIndex == 0)
        {
            Ray lookAtRay = new Ray(transform.position, Camera.main.transform.forward);
            Vector3 lookAtPosition = lookAtRay.GetPoint(20);

            Debug.DrawRay(transform.position, Camera.main.transform.forward * 20, Color.red);
            //  Target.position = lookAtPosition;

            // Animator.SetLookAtPosition(lookAtPosition);
        }
    }

    // Update is called once per frame
    void Update()
    { 

        float horLerp = Mathf.Lerp(mLastDirection.x, movementInput.x, keyFrameDelta * Time.deltaTime);
        float verLerp = Mathf.Lerp(mLastDirection.y, movementInput.y, keyFrameDelta * Time.deltaTime);

      //  if(mLastDirection.sqrMagnitude == 0 && movementInput.sqrMagnitude 0){ }

        animator.SetFloat("Mag", Vector3.ClampMagnitude(movementInput, 1).magnitude);

        Debug.Log(horLerp);

        animator.SetFloat("DirectionHorizontal", horLerp);
        animator.SetFloat("DirectionVertical", verLerp);

        mLastDirection = new Vector2(horLerp, verLerp) ;

        HandleLocomotionRotation();
    }

    private void HandleLocomotionRotation()
    {
        if (mLastDirection.y > 0.1f)
        { 
            Transform cameraTransform = Camera.main.transform;
            Vector3 rotationOffset = cameraTransform.TransformDirection(new Vector2(0, mLastDirection.y));
            
            rotationOffset.y = 0;
            PlayerModel.forward += Vector3.Lerp(PlayerModel.forward, rotationOffset, Time.deltaTime * RotationSpeed);
        
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
