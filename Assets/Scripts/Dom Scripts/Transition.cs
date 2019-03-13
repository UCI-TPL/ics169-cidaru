using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Material transMat;

    public Texture2D currentTexture;

    public AudioSource audioSource;

    public float fadeTime = 1f;

    [SerializeField]
    private bool fade = false;
    [SerializeField]
    private bool unfade = false;
    private float timer = 0;


    void Start()
    {
        timer = 0;
        transMat.SetFloat("_Cutoff", 1f);
        unfade = true;
    }

    public void Update()
    {
        if (fade && unfade)
            fade = unfade = false;

        transMat.SetTexture("_TransitionTex", currentTexture);
        if (fade && transMat.GetFloat("_Cutoff") <= 0.9999)
        {
            timer += Time.unscaledDeltaTime / fadeTime;
            audioSource.volume = Mathf.Lerp(0.5f, 0f, timer);
            transMat.SetFloat("_Cutoff", Mathf.Lerp(0, 1, timer));
        }
        else if (!unfade)
        {
            fade = false;
            timer = 0f;
        }

        if (unfade && transMat.GetFloat("_Cutoff") >= 0.001)
        {
            timer += Time.unscaledDeltaTime / fadeTime;
            audioSource.volume = Mathf.Lerp(0f, 0.5f, timer);
            transMat.SetFloat("_Cutoff", Mathf.Lerp(1, 0, timer));
        }
        else if (!fade)
        {
            unfade = false;
            timer = 0f;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (transMat != null)
            Graphics.Blit(src, dst, transMat);
    }

    public void fadeToBlack()
    {
        transMat.SetFloat("_Cutoff", 0f);
        fade = true;
    }

    public void fadeToBlack(Texture2D texture)
    {
        currentTexture = texture;
        transMat.SetFloat("_Cutoff", 0f);
        fade = true;
    }

    public void unfadeFromBlack()
    {
        transMat.SetFloat("_Cutoff", 1f);
        unfade = true;
    }

    public void unfadeFromBlack(Texture2D texture)
    {
        currentTexture = texture;
        transMat.SetFloat("_Cutoff", 1f);
        unfade = true;
    }

}
