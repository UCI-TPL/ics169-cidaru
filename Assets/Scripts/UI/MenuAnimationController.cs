using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationController : MonoBehaviour {
    public AnimationClip close;

    private Animator anim;
    private int openParameter;

    public void Start()
    {
        anim = GetComponent<Animator>();

        openParameter = Animator.StringToHash("Open");
        print(openParameter);
    }

    public void OpenPanel()
    {
        anim.SetBool(openParameter, true);
    }

    public void ClosePanel()
    {
        anim.SetBool(openParameter, false);

        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(close.length);
        gameObject.SetActive(false);
    }
}
