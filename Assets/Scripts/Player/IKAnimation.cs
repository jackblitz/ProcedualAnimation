using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public TwoBoneIKConstraint HipConstraint; public GameObject HipController;
    public TwoBoneIKConstraint LeftHandConstraint; public GameObject LeftHandController;
    public TwoBoneIKConstraint RightHandConstraint; public GameObject RightHandController;
    public MultiParentConstraint HeadContraint; public GameObject HeadController;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
