using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graph;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    private ForceDirectedLayout layout;
    

    // Update is called once per frame
    void Update()
    {
        if(ForceDirectedLayout.currentLayout.graphReady)
        {
            animator.SetBool("GraphReady", true);
        }
    }
}
