using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFall : MonoBehaviour
{
    public Rigidbody2D heartRb;
    public Transform heartGrounded;
    public float heartSpeedX;
    public float heartSpeedY;
    public bool isHeartGrounded;
    public LayerMask floorLayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("HeartZigZag");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeartGrounded == false)
        {
            HeartMove();
        }
        else
        {
            heartRb.velocity = new Vector2(0, 0);
        }
    }
    private void FixedUpdate()
    {
        isHeartGrounded = Physics2D.OverlapCircle(heartGrounded.position, 0.02f, floorLayer);
    }

    public void HeartMove()
    {
        heartRb.velocity = new Vector2(heartSpeedX, heartSpeedY );
    }

    IEnumerator HeartZigZag()
    {
        yield return new WaitForSeconds(0.4f);
        heartSpeedX = heartSpeedX * -1;
        StartCoroutine("HeartZigZag");

    }
}
