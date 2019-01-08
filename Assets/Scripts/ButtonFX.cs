using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour {

  public AudioSource MyFX;
  public AudioClip HoverFX;
  public AudioClip ClickFX;

  public void HoverSound()
  {
    MyFX.PlayOneShot(HoverFX);
    MyFX.ignoreListenerPause = true;
  }

  public void ClickSound()
  {
    MyFX.PlayOneShot(ClickFX);
    MyFX.ignoreListenerPause = true;
  }
}
