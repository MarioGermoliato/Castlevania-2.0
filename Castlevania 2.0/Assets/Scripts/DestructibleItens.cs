using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItensDrop { LittleHeart, BigHeart, Upgrade, RedBag, PurpleBag, WhiteBag, Dagger }
public class DestructibleItens : MonoBehaviour
{
    public enum ItemOrigin { Candle, Torch}

    private SoundManager _SoundManager;
    public ItensDrop dropItem;
    public ItemOrigin originItem;

    [SerializeField]
    private GameObject powerUp;
    [SerializeField]
    private GameObject smallHeart;
    [SerializeField]
    private GameObject bigHeart;
    [SerializeField]
    private GameObject dagger;
    [SerializeField]
    private GameObject redBag;
    [SerializeField]
    private GameObject purpleBag;
    [SerializeField]
    private GameObject WhiteBag;
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
        // caso seja tocha
        if (originItem == ItemOrigin.Torch)
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
                Instantiate(bigHeart, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), this.transform.localRotation);
            }
            else if (dropItem == ItensDrop.LittleHeart)
            {
                Instantiate(smallHeart, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), this.transform.localRotation);
            }
            else if (dropItem == ItensDrop.Dagger)
            {
                Instantiate(dagger, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), dagger.transform.rotation);
            }
            else if (dropItem == ItensDrop.RedBag)
            {
                Instantiate(redBag, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), redBag.transform.rotation);
            }
            else if (dropItem == ItensDrop.PurpleBag)
            {
                Instantiate(purpleBag, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), purpleBag.transform.rotation);
            }
            else if (dropItem == ItensDrop.WhiteBag)
            {
                Instantiate(WhiteBag, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), WhiteBag.transform.rotation);
            }
        }
        // caso seja vela
        else if (originItem == ItemOrigin.Candle)
        {
            if (dropItem == ItensDrop.Upgrade)
            {
                if (GlobalStats.powerUps < 2)
                {
                    Instantiate(powerUp, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.localRotation);
                }
                else
                {
                    Instantiate(bigHeart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.localRotation);
                }
            }
            else if (dropItem == ItensDrop.BigHeart)
            {
                Instantiate(bigHeart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.localRotation);
            }
            else if (dropItem == ItensDrop.LittleHeart)
            {
                Instantiate(smallHeart, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.localRotation);
            }
            else if (dropItem == ItensDrop.Dagger)
            {
                Instantiate(dagger, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), dagger.transform.rotation);
            }
            else if (dropItem == ItensDrop.RedBag)
            {
                Instantiate(redBag, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), redBag.transform.rotation);
            }
            else if (dropItem == ItensDrop.PurpleBag)
            {
                Instantiate(purpleBag, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), purpleBag.transform.rotation);
            }
            else if (dropItem == ItensDrop.WhiteBag)
            {
                Instantiate(WhiteBag, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), WhiteBag.transform.rotation);
            }
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
        if (collision.CompareTag("Dagger"))
        {
            Destroy(collision.gameObject);
            _SoundManager.audioSource.PlayOneShot(_SoundManager.destroyTorch);
            DropItem();
        }
    }
}
