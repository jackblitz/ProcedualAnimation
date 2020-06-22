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
    public Vector2 lastDirection { get; private set; }

    public Transform Target;
    public float turnSpeed = 8;

    public Transform Model;
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
        float horLerp = Mathf.Lerp(lastDirection.x, move.x, mKeyFrameDelta * Time.deltaTime);
        float verLerp = Mathf.Lerp(lastDirection.y, move.y, mKeyFrameDelta * Time.deltaTime);

        lastDirection = new Vector2(horLerp, verLerp);

        HandleInputData();

        //Rotation to face direction
        HandleLocomotionRotation();
    }

    private void HandleLocomotionRotation()
    {
        float yawCamera = Camera.main.transform.rotation.eulerAngles.y;
       // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);

        Vector3 rotationOffset = Camera.main.transform.TransformDirection(new Vector3(move.x, 0 , move.y));
        rotationOffset.y = 0;
       
        Model.forward = rotationOffset;
        Debug.Log(rotationOffset);
    }

    private void HandleInputData()
    {
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
