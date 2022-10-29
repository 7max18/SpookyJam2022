using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManager : MonoBehaviour
{
    public GameObject[] lanterns;
    public int numLanterns;

    // Start is called before the first frame update
    void Start()
    {
        lanterns = GameObject.FindGameObjectsWithTag("JackOLantern");
        GenLanterns();
    }

    void GenLanterns()
    {
        List<GameObject> selectablePumpkins = new List<GameObject>();

        foreach (GameObject pumpkin in lanterns)
        {
            selectablePumpkins.Add(pumpkin);
            pumpkin.SetActive(false);
        }

        for (int i = 0; i < numLanterns; i++)
        {
            int j = Random.Range(0, selectablePumpkins.Count);
            selectablePumpkins[j].SetActive(true);
            selectablePumpkins.RemoveAt(j);
        }
    }
}
