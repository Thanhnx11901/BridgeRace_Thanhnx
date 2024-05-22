using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtl : MonoBehaviour
{
    protected string currentAnimName;
    [SerializeField] private Animator anim;

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
