using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItensDrop { LittleHeart, BigHeart, Upgrade, RedBag, PurpleBag, WhiteBag }
public class DestructibleItens : MonoBehaviour
{
    

    public ItensDrop dropItem;

    [SerializeField]
    private GameObject powerUp;
    [SerializeField]
    private GameObject smallHeart;
    [SerializeField]
    private GameObject bigHeart;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Instantiate(smallHeart, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.localRotation);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Whip"))
        {
            DropItem();
        }
    }
}
