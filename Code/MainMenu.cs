using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator ma;
    public Animator oa;
    public Animator fa;

    public void PlayButton()
    {
        FadeOut();
    }

    public void FadeOut()
    {
        fa.SetTrigger("FadeOut");
    }

    public void OptionsButton()
    {
        ma.SetTrigger("Leave");
        ma.ResetTrigger("Here");
        oa.SetTrigger("Here");
        oa.ResetTrigger("Leave");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
