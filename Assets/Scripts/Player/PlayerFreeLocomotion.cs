using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerFreeLocomotion : MonoBehaviour
{
    private PlayerInputActions mInputActions;

    // Direction Holder, Right Axis Controls Typically used for Camera Control
    private Vector2 direction;

    // Contains the Position of the lext Axis controller. Typically used for Player Movement
    private Vector2 move;

    public float mKeyFrameDelta = 3f;
    private float RotateSpeed = 15f;

    public Animator mAnimation { get; private set; }
    public Vector3 lastDirection { get; private set; }

    public Transform Target;
    public float turnSpeed = 8;

    public Transform Model;
    private float mMomentumShift;

    void Awake()
    {
        mInputActions = new PlayerInputActions();
        mInputActions.PlayerControls.Direction.performed += ct => direction = ct.ReadValue<Vector2>() ;
        mInputActions.PlayerControls.Move.performed += ct => move = ct.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        mAnimation = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleInputData();

        //Rotation to face direction
        HandleLocomotionRotation();
    }

    private void HandleLocomotionRotation()
    {
        float yawCamera = Camera.main.transform.rotation.eulerAngles.y;
       if (lastDirection.magnitude > 0)
     {
            Vector3 rotationOffset = Camera.main.transform.TransformDirection(lastDirection);
            rotationOffset.y = 0;

            float calculatedTurnSpeed = 1;

            if (mMomentumShift >= 1)
            {
                Model.forward = rotationOffset;//rotationOffset;
            }
            else
            {
                Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * (turnSpeed * calculatedTurnSpeed));//rotationOffset;
            }
            // Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
            //transform.rotation = rotation;
            
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationOffset, Vector3.up), turnSpeed * Time.deltaTime);
        }
        
    }

    private void HandleInputData()
    {
        float horLerp = move.x;//Mathf.Lerp(lastDirection.x, move.x, mKeyFrameDelta * Time.deltaTime);
        float verLerp = move.y;//Mathf.Lerp(lastDirection.y, move.y, mKeyFrameDelta * Time.deltaTime);

        Vector3 directionShift = lastDirection - new Vector3(horLerp, 0, verLerp);
        mMomentumShift = directionShift.sqrMagnitude;

        if (mMomentumShift != 0)
        {
            Debug.Log(mMomentumShift);
        }

        lastDirection = new Vector3(horLerp, 0, verLerp);

        mAnimation.SetFloat("Speed", Vector3.ClampMagnitude(move, 1).magnitude);
    }

    private void OnEnable()
    {
        mInputActions.Enable();
    }

    private void OnDisable()
    {
        mInputActions.Disable();
    }
}
