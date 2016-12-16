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
    public float FlashFrequency;
    public float InvincibleTime;
    public float Health;

    public float MaxHeight;
    public float MinHeight;

    private BoxCollider2D bCollider;
    private SpriteRenderer sRenderer;

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

        bCollider = GetComponent<BoxCollider2D>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();
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




    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            StartCoroutine(Invincible(InvincibleTime));
            Debug.Log("Collide with house");
        }
    }



    IEnumerator Invincible(float duration)
    {
        bCollider.enabled = false;

        var startTime = Time.time;
        while (Time.time - startTime < InvincibleTime)
        {
            sRenderer.enabled = !sRenderer.enabled;
            yield return new WaitForSeconds(FlashFrequency);
        }

        sRenderer.enabled = true;

        bCollider.enabled = true;
    }
}
