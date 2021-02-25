using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    [SerializeField] Transform hatPosition;
    private bool hasHat;

    public bool HasHat 
    {
        get { return hasHat; }
    }

    // Start is called before the first frame update
    void Start()
    {
        hasHat = false; 
    }

    public void GiveHat(GameObject hat) 
    {
        if (hasHat) { return; }
        Instantiate(hat, hatPosition.position, Quaternion.identity);
        hasHat = true;
    }
}
