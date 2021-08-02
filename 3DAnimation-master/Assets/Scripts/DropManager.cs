using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class DropItem
{
    public GameObject itemToDrop;
    public float dropWeight;
}

public class DropManager : MonoBehaviour
{
    public List<DropItem> dropTable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject DropItem()
    {
        //Create CDF Array
        List<float> CDFArray = new List<float>();
        //For each element in drop table, fill in density of CDF array
        float runningTotal = 0;
        foreach (DropItem item in dropTable)
        {
            //Update running total
            runningTotal += item.dropWeight;
            //Add to CDF
            CDFArray.Add(runningTotal);
        }


        //Choose random number < total density
        float randomNumber = UnityEngine.Random.Range(0, CDFArray[CDFArray.Count - 1]);
        //Go through CDF array 
        for (int index = 0; index < CDFArray.Count; index++)
        {
            //If random number < density at that point...
            if (randomNumber < CDFArray[index])
            {
                Debug.Log("Item Dropped");
                //return item at same point
                return dropTable[index].itemToDrop;
                
            }
        }
        //If number somehow escapes density...
        Debug.Log("ERROR: DropManager-randomNumber exceeded index");
        return null;
    }
}
