using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartilloAnim_helper : MonoBehaviour
{
    public MartilloOp2 martillo;
    //public Martillo martillo;
    public Vector3 localPosOrigin;

    private void Start()
    {
        localPosOrigin = transform.localPosition;
    }

//    public void GoBackToPos()
//    {
//        martillo.GoBackToPos();
//    }

    public void DamageTarget()
    {
        Debug.Log("le estoy pegando");
        martillo.DamageTarget();
    }

    public void FreeHammerAnimationHit()
    {
        martillo.FreeHammerAnimationHit();
    }
}
