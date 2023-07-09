using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICatTower : MonoBehaviour
{
    public Cat Cat { get; private set; }
    public Transform catLocation;
    // Start is called before the first frame update
    void Awake()
    {
        catLocation = transform.GetChild(0);
    }
    public void SetCat(Cat c)
    {
        Cat = Instantiate(c, catLocation);
    }

	public void DisplayInfo()
	{
        if (Cat != null)
        {
    		GameManager.Instance.Preview.DisplayInfo(Cat.Header, Cat.Info);
        }
        else
        {
    		GameManager.Instance.Preview.DisplayInfo("Cat Tower", "An empty cat tower. A cat is bound to settle down here soon...");
        }
	}
	public void ResetInfo()
	{
		GameManager.Instance.Preview.DisplayInfo("", "Highlight an item or cat for more info!");
	}
}
