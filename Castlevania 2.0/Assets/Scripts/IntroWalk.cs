using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroWalk : MonoBehaviour
{
    
    public float velocityWalk;
    public Rigidbody2D introRB;
    public SpriteRenderer characterLooking;
    public SpriteRenderer characterWalking;
    public float timeToChange;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        IntroCharacter();
    }

    public void IntroCharacter()
    {
        introRB.velocity = new Vector2(velocityWalk, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stop"))
        {
            velocityWalk = 0;
            characterWalking.enabled = false;
            characterLooking.enabled = true;
            StartCoroutine("TimeToWaitStart");

        }
        if (collision.CompareTag("Castle"))
        {
            SceneControll.ChangeScene("Castle");           
        }
    }

    IEnumerator TimeToWaitStart()
    {
        yield return new WaitForSeconds(timeToChange);
        SceneControll.ChangeScene("Entrance");
    }
  
}
