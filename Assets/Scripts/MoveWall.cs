using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour {

  public Transform Target;
  Vector3 velocity = Vector3.zero;
  public float SmoothTime = 50f;

  public float StepWidth = 10f;
  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    Vector3 TargetPos = Target.position;

    TargetPos.x = TargetPos.x + StepWidth;
    
    TargetPos.z = transform.position.z;
    TargetPos.y = transform.position.y;

    transform.position = Vector3.SmoothDamp(transform.position, TargetPos, ref velocity, SmoothTime);
  }
}
