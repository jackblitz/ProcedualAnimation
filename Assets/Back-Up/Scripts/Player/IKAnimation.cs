using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public TwoBoneIKConstraint HipConstraint; public GameObject HipController;
    public MultiParentConstraint HeadContraint; public GameObject HeadController;

    public TwoBoneIKConstraint LeftHandConstraint; public GameObject LeftHandController;
    public TwoBoneIKConstraint RightHandConstraint; public GameObject RightHandController;

    void Start()
    {

    }

    public void AttachIKGameObject(IKGameObject gameObject)
    {
        LeftHandController = gameObject.IKLeftArm;
        RightHandController = gameObject.IKRightArm;

        if(LeftHandController != null)
        {
           // LeftHandConstraint.CreateJob
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public Quaternion getHipRotation()
    {
        return HipController.transform.rotation;
    }

    public Quaternion getHeadRotation()
    {
        return HeadController.transform.rotation;
    }
}
