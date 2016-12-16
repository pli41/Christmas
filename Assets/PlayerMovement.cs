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
    public float FlashTime;
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
            StartCoroutine(Invincible(FlashTime));
            Debug.Log("Collide with house");
        }
    }



    IEnumerator Invincible(float duration)
    {
        bCollider.enabled = false;
        for (int i = 0; i < 5; i++)
        {
            sRenderer.enabled = false;
            yield return new WaitForSeconds(duration);
            sRenderer.enabled = true;
            yield return new WaitForSeconds(duration);
        }
        bCollider.enabled = true;
        //}
    }
}
