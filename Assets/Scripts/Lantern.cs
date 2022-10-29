using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public bool lit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightUp()
    {
        //Animate lighting up
        lit = true;
        Debug.Log("Lantern Lit!");
    }
}
