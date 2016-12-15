using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {

    public float MaxVirticalSpeed;
    public float VirticalSpeed;
    public float TargetVirticalSpeed;
    public float SmoothTime;
    public float ForwardSpeed;

    public float MaxHeight;
    public float MinHeight;

    public void OnTouchStart()
    {
        TargetVirticalSpeed = MaxVirticalSpeed;
    }

    public void OnTouchEnd()
    {
        TargetVirticalSpeed = -MaxVirticalSpeed;
    }

    void Start()
    {
        OnTouchEnd();
    }

    float _tempVelocity;
	void Update () {
        var y = transform.position.y;
        var x = transform.position.x;

        y = Mathf.Clamp(y + VirticalSpeed * Time.deltaTime, MinHeight, MaxHeight);
        x = x + ForwardSpeed * Time.deltaTime;

        transform.position = new Vector3(x, y, transform.position.z);

        VirticalSpeed = Mathf.SmoothDamp(VirticalSpeed, TargetVirticalSpeed, ref _tempVelocity, SmoothTime);
	}
}
