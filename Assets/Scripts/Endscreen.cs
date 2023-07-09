using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endscreen : MonoBehaviour
{
	public void L1()
	{
		SceneManager.LoadScene(1);
	}
	public void L2()
	{
		SceneManager.LoadScene(2);
	}
	public void Menu()
	{
		SceneManager.LoadScene(0);
	}
}
