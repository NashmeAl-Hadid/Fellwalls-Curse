using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransiionStart : MonoBehaviour
{
    public Animator transitionAnim;

    void Awake()
    {
        transitionAnim.Play("end");
    }
}
