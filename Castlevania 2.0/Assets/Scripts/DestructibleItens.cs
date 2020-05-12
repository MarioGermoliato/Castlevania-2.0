﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItensDrop { LittleHeart, BigHeart, Upgrade, RedBag, PurpleBag, WhiteBag, Dagger }
public class DestructibleItens : MonoBehaviour
{

    private SoundManager _SoundManager;
    public ItensDrop dropItem;

    [SerializeField]
    private GameObject powerUp;
    [SerializeField]
    private GameObject smallHeart;
    [SerializeField]
    private GameObject bigHeart;
    [SerializeField]
    private GameObject dagger;
    // Start is called before the first frame update
    void Start()
    {
        _SoundManager = (FindObjectOfType(typeof(SoundManager)) as SoundManager);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        if (dropItem == ItensDrop.Upgrade)
        {
            if (GlobalStats.powerUps < 2)
            {
                Instantiate(powerUp, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), this.transform.localRotation);
            }
            else
            {
                Instantiate(bigHeart, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), this.transform.localRotation);
            }
        }
        else if (dropItem == ItensDrop.BigHeart)
        {
            Instantiate(bigHeart, new Vector3(this.transform.position.x, this.transform.position.y +1f, this.transform.position.z), this.transform.localRotation);
        }
        else if (dropItem == ItensDrop.LittleHeart)
        {
            Instantiate(smallHeart, new Vector3 (this.transform.position.x, this.transform.position.y +1f, this.transform.position.z), this.transform.localRotation);
        }
        else if (dropItem == ItensDrop.Dagger)
        {
            Instantiate(dagger, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), dagger.transform.rotation);
        }
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Whip"))
        {
            _SoundManager.audioSource.PlayOneShot(_SoundManager.destroyTorch);
            DropItem();            
        }
    }
}
