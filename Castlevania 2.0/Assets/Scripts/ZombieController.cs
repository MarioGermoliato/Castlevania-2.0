using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy
{
    [SerializeField]
    private float velMov;

    [SerializeField]
    private int direction;

    [SerializeField]
    private Rigidbody2D zombieRB;

    // Start is called before the first frame update
    void Start()
    {
        zombieRB = GetComponent<Rigidbody2D>();
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ZombieWalk();
    }

    public void ZombieWalk()
    {
        zombieRB.velocity = new Vector2(velMov * direction, zombieRB.velocity.y);
    }

    public  ZombieController(int _direction)
    {
        direction = _direction;
    }
}
