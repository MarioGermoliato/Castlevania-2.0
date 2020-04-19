using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float velocityPlayer;
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float x = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector2(x * velocityPlayer, playerRB.velocity.y);
        playerAnimator.SetFloat("MoveSpeed", Mathf.Abs(x));

        if (x > 0.05f)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (x < -0.05f)
            transform.rotation = Quaternion.Euler(0, 0, 0);

    }
}
