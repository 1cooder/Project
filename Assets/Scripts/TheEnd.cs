using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour
{


  void OnTriggerEnter2D(Collider2D trig)
  {
   	   if (trig.gameObject.tag == "Player")
	   {
		SceneManager.LoadScene("Menu-YouWin", LoadSceneMode.Single);
		}
  }
}
