using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // class
    public FirstPersonController MyFPC;

    private void OnCollisionEnter(Collision collision)
    {
        MyFPC.SetFalseIsJump();
    }
}
