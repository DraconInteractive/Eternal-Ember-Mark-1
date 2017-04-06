using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class HorseInput : MonoBehaviour
{

    private HorseController myHorse;
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    [Header("Inputs Buttons")]
    public string Jump = "Jump";
    public string Shift = "Fire3";
    public string Attack = "Fire1";

#if MOBILE_INPUT && !UFPS
    [Header("Add this Inputs to the INPUT MANAGER")]
    public string Walk = "Walk";
    public string Trot = "Trot";
    public string Gallop = "Gallop";
#endif

#if !MOBILE_INPUT && !UFPS
    public KeyCode Walk = KeyCode.Alpha1;
    public KeyCode Trot = KeyCode.Alpha2;
    public KeyCode Gallop = KeyCode.Alpha3;
#endif

    void Start()
    {
        myHorse = GetComponent<HorseController>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        myHorse.Horizontal = CrossPlatformInputManager.GetAxis("Horizontal");   //Get the Horizontal Axis
        myHorse.Vertical = CrossPlatformInputManager.GetAxis("Vertical");       //Get the Vertical Axis

        myHorse.Attack1 = CrossPlatformInputManager.GetButton(Attack);         //Get the Attack1 button
        myHorse.Shift = CrossPlatformInputManager.GetButton(Shift);          //Get the Shift button  

        myHorse.Jump = CrossPlatformInputManager.GetButton(Jump);     //Get the Jump button
        myHorse.FowardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); //If foward is pressed

#if !MOBILE_INPUT && !UFPS

        myHorse.Death = Input.GetKeyDown(KeyCode.K);         //Get the Death button change the variable entry to manipulate how the death works

        myHorse.Speed1 = Input.GetKeyDown(Walk);            //Walk
        myHorse.Speed2 = Input.GetKeyDown(Trot);            //Trot
        myHorse.Speed3 = Input.GetKeyDown(Gallop);          //Run

#endif

#if MOBILE_INPUT
        myHorse.Speed1 =  CrossPlatformInputManager.GetButtonDown(Walk);             //Walk
        myHorse.Speed2 = CrossPlatformInputManager.GetButtonDown(Trot);              //Trot
        myHorse.Speed3 = CrossPlatformInputManager.GetButtonDown(Gallop);          //Run
        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
            myHorse.FowardPressed = true;
        else  myHorse.FowardPressed = false;
#endif
    }
}
