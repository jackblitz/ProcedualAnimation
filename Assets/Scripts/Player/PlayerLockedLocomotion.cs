using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerLockedLocomotion : MonoBehaviour
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
    private float turnSpeed = 8;


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

        mAnimation.SetFloat("DirectionX", horLerp);
        mAnimation.SetFloat("DirectionY", verLerp);

        lastDirection = new Vector2(horLerp, verLerp);

        mAnimation.SetFloat("FacingDirection", 1);

        float yawCamera = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
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
