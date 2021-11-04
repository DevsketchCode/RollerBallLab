using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateCode : MonoBehaviour
{

    [SerializeField]
    public int timesClicked = 1;

    [SerializeField]
    public bool singleCodeIsCorrect = false;

    private GameObject codeCanvas;
    private string objectText;

    public void CheckSingleCode(GameObject obj)
    {

        // reset clicks on the object
        if (obj.GetComponent<ValidateCode>().timesClicked > 4)
        {
            obj.GetComponent<ValidateCode>().timesClicked = 1;
        }
        else
        {
            obj.GetComponent<ValidateCode>().timesClicked++;
        }

        string objectNumber = obj.name.Substring(obj.name.Length - 1, 1);

        // Determine which codeObject has been clicked
        if (objectNumber == "1" || objectNumber == "2" || objectNumber == "3")
        {
            codeCanvas = GameObject.Find("CodeCanvas" + objectNumber);
            objectText = codeCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;

            // Check if the number of clicks (the number displayed on the codecylinder, matches the one on the canvas
            if (obj.GetComponent<ValidateCode>().timesClicked.ToString() == objectText)
            {

                obj.GetComponent<ValidateCode>().singleCodeIsCorrect = true;
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 0.2f, obj.transform.position.z);
                obj.GetComponent<BoxCollider>().enabled = false;
                // Validate each code to see if they all match
                ValidateCompleteCode();
            } else
            {
                singleCodeIsCorrect = false;
            }
        }
    }
    
    public void ValidateCompleteCode()
    {
        GameObject cc1 = GameObject.Find("CodeCylinder1");
        GameObject cc2 = GameObject.Find("CodeCylinder2");
        GameObject cc3 = GameObject.Find("CodeCylinder3");


        if (cc1.GetComponent<ValidateCode>().singleCodeIsCorrect && cc2.GetComponent<ValidateCode>().singleCodeIsCorrect && cc3.GetComponent<ValidateCode>().singleCodeIsCorrect)
        {
            // If the entire code matches, then activate the elevator
            GameObject.Find("ElevatorPlank").GetComponent<PingPongXYZ>().enabled = true;
        }
    }
}
