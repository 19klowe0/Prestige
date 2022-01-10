using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitch : MonoBehaviour
{
    public GameObject normalForm, butterForm;
    
    // Start is called before the first frame update

    public int whichIsOn = 1;
    void Start()
    {
        normalForm.gameObject.SetActive(true);
        butterForm.gameObject.SetActive(false);
        //set playercontroller to the right movements?
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public method to switch the forms by pressing the button 
    public void SwitchForm()
    {
        switch (whichIsOn)
        {
            case 1:
                whichIsOn = 2;
                normalForm.gameObject.SetActive(false);
                butterForm.gameObject.SetActive(true);
                break;
            case 2:
                whichIsOn = 1;
                normalForm.gameObject.SetActive(true);
                butterForm.gameObject.SetActive(false);
                break;


        }

    }
}

