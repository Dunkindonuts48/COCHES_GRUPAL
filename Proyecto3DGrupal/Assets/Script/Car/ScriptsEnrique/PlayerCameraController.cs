using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform player;

    public Vector3 idealOffset = new Vector3(0.0f, 2.5f, -5.0f);
    public Vector3 idealLookOffset = new Vector3(0.0f, 1.0f, 10.0f);

    public float springK = 15.0f;
    public float damp = 10.0f;
    public float maxDistFromIdealPos = 2.0f;

    private Vector3 vel = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float dt = Time.deltaTime;

        Vector3 idealPos;
        Vector3 idealLookOffset;

        CalcIdealCamPositions(out idealPos, out idealLookOffset);

        Vector3 pos = transform.position;
        ApplySpringDumperSystem(idealPos, dt, ref pos);

        LimitMovement(idealPos, ref pos);

        transform.position = pos;
        transform.LookAt(idealLookOffset, new Vector3(0.0f, 1.0f, 0.0f));
    }

    private void CalcIdealCamPositions(out Vector3 idealPos, out Vector3 idealLookPos)
    {
        //Vector3 pos = player.transform.position +
        //              idealOffset.x * player.right +
        //              idealOffset.y * player.up +
        //              idealOffset.z * player.forward;

        //Vector3 pos = player.localToWorldMatrix.MultiplyPoint(idealOffset);

        idealPos = player.TransformPoint(idealOffset);

        idealLookPos = player.TransformPoint(idealLookOffset);
    }
    private void ApplySpringDumperSystem(Vector3 idealPos, float dt, ref Vector3 pos)
    {
        float dampVelFactor = Mathf.Max(0.0f, 1.0f - damp * dt);
        vel = vel * dampVelFactor;

        Vector3 offset = idealPos - pos;
        Vector3 springAcccel = springK * offset;

        vel = vel * (1 - damp * dt);

        pos += vel * dt;
    }
    private void LimitMovement(Vector3 idealPos, ref Vector3 pos)
    {
        Vector3 dirToIdealPos = pos - idealPos;

        float distToIdealPos = dirToIdealPos.magnitude;

        if (distToIdealPos > maxDistFromIdealPos)
        {
            pos = idealPos + (maxDistFromIdealPos / distToIdealPos) * dirToIdealPos;
        }
    }
}
