using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
  public Transform Target;

  Vector3 velocity = Vector3.zero;

  public float SmoothTime = .15f;

  //Enable and set the maximum Y value
  public bool YMaxEnabled = false;
  public float YMaxValue = 0;

  //Enable and set the minimum Y value
  public bool YMinEnabled = false;
  public float YMinValue = 0;

  public bool XMaxEnabled = false;
  public float XMaxValue = 0;

  public bool XMinEnabled = false;
  public float XMinValue = 0;


  void FixedUpdate()
  {
    Vector3 TargetPos = Target.position;

    //Vertical
    if (YMinEnabled && YMaxEnabled)
    {
      TargetPos.y = Mathf.Clamp(Target.position.y, YMinValue, YMaxValue);
    }
    else if (YMinEnabled)
    {
      TargetPos.y = Mathf.Clamp(Target.position.y, YMinValue, Target.position.y);
    }
    else if (YMaxEnabled)
    {
      TargetPos.y = Mathf.Clamp(Target.position.y, Target.position.y, YMaxValue);
    }

    //Horizontal
    if (XMinEnabled && XMaxEnabled)
    {
      TargetPos.x = Mathf.Clamp(Target.position.x, XMinValue, XMaxValue);
    }
    else if (XMinEnabled)
    {
      TargetPos.x = Mathf.Clamp(Target.position.x, XMinValue, Target.position.x);
    }
    else if (XMaxEnabled)
    {
      TargetPos.x = Mathf.Clamp(Target.position.x, Target.position.x, XMaxValue);
    }

    TargetPos.z = transform.position.z;

    transform.position = Vector3.SmoothDamp(transform.position, TargetPos, ref velocity, SmoothTime);
  }
}
