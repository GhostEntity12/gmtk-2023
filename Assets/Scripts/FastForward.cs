using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FastForward : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI speedButton;
	int speedMod = 1;
	public void ChangeSpeed()
	{
		speedMod = speedMod % 3 + 1;
		Time.timeScale = speedMod;
		speedButton.SetText(new string('>', speedMod));
	}
}
