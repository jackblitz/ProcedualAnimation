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
