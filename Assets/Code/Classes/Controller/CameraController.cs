using UnityEngine;

public class CameraController : MonoBehaviour
{
     public float InterpVelocity = 0f;
     public float Smoothing = 12f;
     public float MinDistance = 0f;
     public float FollowDistance = 0f;
     public GameObject Target = null;
     public Vector3 Offset = Vector3.zero;

     private Vector3 TargetPos = Vector3.zero;

     private void Start () 
     {
         TargetPos = transform.position;
     }
     
     private void FixedUpdate () 
     {
         if (Target)
         {
             Vector3 posNoZ = transform.position;
             posNoZ.z = Target.transform.position.z;
  
             Vector3 TargetDirection = (Target.transform.position - posNoZ);
  
             InterpVelocity = TargetDirection.magnitude * Smoothing;
  
             TargetPos = transform.position + (TargetDirection.normalized * InterpVelocity * Time.deltaTime); 
  
             transform.position = Vector3.Lerp( transform.position, TargetPos + Offset, 0.25f);
         }
     }
}
