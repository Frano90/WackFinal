using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gruya_Anim : MonoBehaviour
{
    public void PlayAnimation()
    {
        if (GetComponent<Animator>() == null)
            return;
        
        GetComponent<Animator>().SetTrigger("Flap");
    }
}
