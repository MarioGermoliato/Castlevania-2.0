using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class GameController: MonoBehaviour
{
    public Button startBtn;

    public void StartGame()
    {
        StartCoroutine("BtnFlick");
    }
    IEnumerator BtnFlick()
    {
        for(int i = 0; i < 5; i++)
        {
            startBtn.image.color = Color.clear;
            yield return new WaitForSeconds(0.2f);
            startBtn.image.color = Color.white;
            yield return new WaitForSeconds(0.2f);

        }
        SceneControll.ChangeScene("PreEntrance");
        
    }
   
   
}
