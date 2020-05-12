using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 
    
    [Header("HUD")]   
    public Text scoreTxt;   
    public Text timeTxt;  
    public Text heartsTxt;
    public Text lifesTxt;
    public GameObject[] currentItem;

   /* [Header("Item Icons")]
    public Image daggerIcon;
    public Image crucifixIcon;
    public Image axeIcon;
    public Image waterIcon;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowIcon(int itemIconIdex)
    {
        foreach (var item in currentItem)
        {
            item.SetActive(false);
            currentItem[itemIconIdex].SetActive(true);
        }   

    }
}
