using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMenu
{


public class CrabMenuScript : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void walkingAnimation()
    {
        animator.SetBool("Walking", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attacking", false);
    }

    public void idleAnimation()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Walking", false);
        animator.SetBool("Attacking", false);
    }

    public void attackAnimation()
    {
        animator.SetBool("Attacking", true);
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", false);
    }
}
}
