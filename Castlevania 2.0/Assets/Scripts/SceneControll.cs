﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneControll 
{
  public static void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }    
}
