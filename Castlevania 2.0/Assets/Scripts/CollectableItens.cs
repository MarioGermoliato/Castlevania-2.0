using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItens : MonoBehaviour
{
    private UIManager _UIManager;
    private PlayerController _PlayerController;
    private SoundManager _SoundManager;
    [SerializeField]
    private ItensDrop CurrentItem;

    // Start is called before the first frame update
    void Start()
    {
        _UIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
        _PlayerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CollectHearts(int nHearts)
    {
        GlobalStats.hearts += nHearts;
        _UIManager.heartsTxt.text = "-" + GlobalStats.hearts.ToString();
    }
        private void ToScore(int nPoints)
    {
        GlobalStats.score += nPoints;
        _UIManager.scoreTxt.text = "SCORE-" + GlobalStats.score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CurrentItem == ItensDrop.LittleHeart)
            {
                CollectHearts(1);
                Destroy(this.gameObject);
            }
            else if (CurrentItem == ItensDrop.BigHeart)
            {
                CollectHearts(5);
                Destroy(this.gameObject);
            }
            else if (CurrentItem == ItensDrop.RedBag)
            {
                ToScore(100);
                Destroy(this.gameObject);
            }
            else if (CurrentItem == ItensDrop.PurpleBag)
            {
                ToScore(300);
                Destroy(this.gameObject);
            }
            else if (CurrentItem == ItensDrop.WhiteBag)
            {
                ToScore(700);
                Destroy(this.gameObject);
            }
            else if (CurrentItem == ItensDrop.Upgrade)
            {
                _PlayerController.GetUpgrade();
                Destroy(this.gameObject);
            }
        }
    }
}
