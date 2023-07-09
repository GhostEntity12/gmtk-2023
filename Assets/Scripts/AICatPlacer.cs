using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AICatPlacer : MonoBehaviour
{
	public Funds Funds { get; private set; } = new();
	[SerializeField] List<Cat> catPrefabs;

	public void DoPlacing()
	{
		// Choose a random tower
		AICatTower chosenTower = GameManager.Instance.Towers[Random.Range(0, GameManager.Instance.Towers.Count)];
		if (chosenTower.Cat)
		{
			// upgrade the cat
			// not yet implemented
			Debug.Log("Cat upgrade");
			//chosenTower.Cat.DoRandomUpgrade();
			return;
		}
		else
		{
			// choose a random cat and place it on the tower
			List<Cat> possibleCats = catPrefabs.Where(c => c.Cost <= Funds.AvailableCurrency).ToList();
			if (possibleCats.Count == 0) return;
			chosenTower.SetCat(possibleCats[Random.Range(0, possibleCats.Count)]);
			Funds.RemoveFunds(chosenTower.Cat.Cost);
		}
	}
}
