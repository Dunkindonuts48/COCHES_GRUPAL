using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelsController : MonoBehaviour
{
   
    public enum pilotType { Human, AI };
    [SerializeField] pilotType pilot;
    [SerializeField] private FixedJoystick moveJoystick;

    [SerializeField] WheelCollider frontRightwheel;
    [SerializeField] WheelCollider frontLeftwheel;
    [SerializeField] WheelCollider backRightwheel;
    [SerializeField] WheelCollider backLeftwheel;

    [SerializeField] Transform frontRightwheelTransform;
    [SerializeField] Transform frontLeftwheelTransform;
    [SerializeField] Transform backRightwheelTransform;
    [SerializeField] Transform backLeftwheelTransform;

    public float accelIA = 600f; //Do NOT modify this value, is public because it is needed on the menu

    private float breakFroce = 5000f;
    private float maxTurnAngle = 30f;
    private float maxTurnAngleIA = 40f;
    private float accel = 400f;
    private float currentAccel = 0f;
    private float currentbreakForce = 0f;
    private float currentTurnAngle = 0f;
    private RacerCollision racerInfo;

    private void Start()
    {
        racerInfo = GetComponentInParent<RacerCollision>();
    }

    private void FixedUpdate()
    {
        
            if (pilot == pilotType.Human)
            {
            if (Application.platform == RuntimePlatform.WindowsEditor || (Application.platform == RuntimePlatform.WindowsPlayer))
            {
                //Get forward/reverse accel from axis w and s
                currentAccel = accel * Input.GetAxis("Vertical");
            }
            if (Application.platform == RuntimePlatform.Android )
            {
                float x = moveJoystick.Vertical; //Equals the joystick handle's position from the center of the joystick on the vertical axis
                currentAccel = accel * x; 
            }

                //if we space is press it gives currentBreakForce a value
                if (Input.GetKey(KeyCode.Space))
                {
                    currentbreakForce = breakFroce;
                }
                else
                    currentbreakForce = 0f;

            
            if (Application.platform == RuntimePlatform.WindowsEditor || (Application.platform == RuntimePlatform.WindowsPlayer))
            {
                //Angle from the car
                currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
            }
            if (Application.platform == RuntimePlatform.Android )
            {
                float z = moveJoystick.Horizontal; //Equals the joystick handle's position from the center of the joystick on the horizontal axis
                currentTurnAngle = maxTurnAngle * z; 
            }
            
            }
            else // AI pilot
            {
                currentAccel = accelIA;
                /*nextCheckpointPosition = CheckpointCollisionData.checkpointArray[racerInfo.nextCheckpoint].transform.position;
                Vector3 direction = nextCheckpointPosition - transform.position;
                direction.y = 0;

                float provisionalAngle =  Vector3.Angle(transform.forward, direction);

                print(provisionalAngle);
                currentTurnAngle = provisionalAngle;*/

                Vector3 steerTo = transform.InverseTransformPoint(CheckpointCollisionData.checkpointArray[racerInfo.nextCheckpoint].transform.position);
                steerTo /= steerTo.magnitude;
                currentTurnAngle = (steerTo.x / steerTo.magnitude) * maxTurnAngleIA;
            }

            frontLeftwheel.steerAngle = currentTurnAngle;
            frontRightwheel.steerAngle = currentTurnAngle;

            //Apply accel to the front wheels
            frontRightwheel.motorTorque = currentAccel;
            frontLeftwheel.motorTorque = currentAccel;

            //Apply breaking force to all wheels
            frontLeftwheel.brakeTorque = currentbreakForce;
            frontRightwheel.brakeTorque = currentbreakForce;
            backLeftwheel.brakeTorque = currentbreakForce;
            backRightwheel.brakeTorque = currentbreakForce;

            //Update Wheels
            UpdateWheelstate(frontLeftwheel, frontLeftwheelTransform);
            UpdateWheelstate(frontRightwheel, frontRightwheelTransform);
            UpdateWheelstate(backLeftwheel, backLeftwheelTransform);
            UpdateWheelstate(backRightwheel, backRightwheelTransform);
        
    }

    void UpdateWheelstate(WheelCollider col, Transform tran)
    {
        //get wheel state
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation); // this function gets the position and rotation of the wheel collider and put it in the vector3 and the Quaternion

        //set wheel state
        tran.position = position;
        tran.rotation = rotation;        

    }

}
