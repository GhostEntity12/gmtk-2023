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
}
