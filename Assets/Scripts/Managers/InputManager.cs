using System;
//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// enum for all actions the player can do in the game
/// </summary>
public enum PlayerAction
{
    //default
    None,

    //game functions
    Interact, PauseGame,

    //character functions

}

/// <summary>
/// InputManager is a Wrapper singleton around the unity Input system
/// </summary>
class InputManager
{
    #region Fields

    //singleton instance
    static InputManager instance;

    //reference to event system object
    EventSystem eventSystem;

    //current controller settings dictionary
    Dictionary<PlayerAction, CustomInput> currentSettings;

    //keyboard functions
    Dictionary<PlayerAction, CustomInput> wasdSettings;
    Dictionary<PlayerAction, CustomInput> ijklSettings;
    Dictionary<PlayerAction, CustomInput> arrowSettings;

    //xbox controller functions
    Dictionary<PlayerAction, CustomInput> xboxController;

    //string key/controller bind dictionaries
    Dictionary<PlayerAction, string> currentKeyStrings;
    Dictionary<PlayerAction, string> wasdKeyStrings;
    Dictionary<PlayerAction, string> ijklKeyStrings;
    Dictionary<PlayerAction, string> arrowKeyStrings;
    Dictionary<PlayerAction, string> xboxKeyStrings;

    #endregion

    #region Constructor

    /// <summary>
    /// private constructor
    /// </summary>
    private InputManager()
    {
        //xbox controller input
        if (Input.GetJoystickNames().Length > 0)
        {
            Debug.Log(Input.GetJoystickNames()[0]);

            ControllerConnected = true;

            //initialize xbox dictionary
            xboxController = new Dictionary<PlayerAction, CustomInput>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, new CustomInput("Xbox_Start_Button", InputType.Button) },
                //{ PlayerAction.ViewInventory, new CustomInput("Xbox_B_Button", InputType.Button) },
                //{ PlayerAction.MoveHorizontal, new CustomInput("Xbox_Left_Joystick_Horizontal", InputType.Axis) },
                //{ PlayerAction.MoveVertical, new CustomInput("Xbox_Left_Joystick_Vertical", InputType.Axis) },
                //{ PlayerAction.Jump, new CustomInput("Xbox_A_Button", InputType.Button) },
                //{ PlayerAction.Interact, new CustomInput("Xbox_X_Button", InputType.Button) },
                //{ PlayerAction.FirePrimary, new CustomInput("Xbox_Right_Trigger", InputType.Axis) },
                //{ PlayerAction.FireSecondary, new CustomInput("Xbox_Left_Trigger", InputType.Axis) },
            };

            //set xbox to current settings
            currentSettings = xboxController;

            //setup xbox key string dictionary
            xboxKeyStrings = new Dictionary<PlayerAction, string>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, "Start Button" },
                //{ PlayerAction.ViewInventory, "B Button" },
                //{ PlayerAction.MoveHorizontal, "Left Joystick Horizontal" },
                //{ PlayerAction.MoveVertical, "Left Joystick Vertical" },
                //{ PlayerAction.Jump, "A Button" },
                //{ PlayerAction.Interact, "X Button" },
                //{ PlayerAction.FirePrimary, "Right Trigger" },
                //{ PlayerAction.FireSecondary, "Left Trigger" },
            };

            //set xbox keys to current keys
            currentKeyStrings = xboxKeyStrings;
        }
        //keyboard input
        else
        {
            //keyboard and mouse
            wasdSettings = new Dictionary<PlayerAction, CustomInput>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, new CustomInput(KeyCode.Escape) },
                //{ PlayerAction.ViewInventory, new CustomInput(KeyCode.B) },
                //{ PlayerAction.MoveHorizontal, new CustomInput("HorizontalWASD", InputType.Axis) },
                //{ PlayerAction.MoveVertical, new CustomInput("VerticalWASD", InputType.Axis) },
                //{ PlayerAction.Jump, new CustomInput(KeyCode.Space) },
                //{ PlayerAction.Interact, new CustomInput(KeyCode.F) },
                //{ PlayerAction.FirePrimary, new CustomInput("0", InputType.MouseButton) },
                //{ PlayerAction.FireSecondary, new CustomInput("1", InputType.MouseButton) },
            };

            ijklSettings = new Dictionary<PlayerAction, CustomInput>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, new CustomInput(KeyCode.Escape) },
                //{ PlayerAction.ViewInventory, new CustomInput(KeyCode.B) },
                //{ PlayerAction.MoveHorizontal, new CustomInput("HorizontalIJKL", InputType.Axis) },
                //{ PlayerAction.MoveVertical, new CustomInput("HorizontalIJKL", InputType.Axis) },
                //{ PlayerAction.Jump, new CustomInput(KeyCode.Space) },
                //{ PlayerAction.Interact, new CustomInput(KeyCode.H) },
                //{ PlayerAction.FirePrimary, new CustomInput("0", InputType.MouseButton) },
                //{ PlayerAction.FireSecondary, new CustomInput("1", InputType.MouseButton) },
            };

            arrowSettings = new Dictionary<PlayerAction, CustomInput>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, new CustomInput(KeyCode.Escape) },
                //{ PlayerAction.ViewInventory, new CustomInput(KeyCode.B) },
                //{ PlayerAction.MoveHorizontal, new CustomInput("HorizontalArrow", InputType.Axis) },
                //{ PlayerAction.MoveVertical, new CustomInput("VerticalArrow", InputType.Axis) },
                //{ PlayerAction.Jump, new CustomInput(KeyCode.UpArrow) },
                //{ PlayerAction.Interact, new CustomInput(KeyCode.RightControl) },
                //{ PlayerAction.FirePrimary, new CustomInput("0", InputType.MouseButton) },
                //{ PlayerAction.FireSecondary, new CustomInput("1", InputType.MouseButton) },
            };

            //set default keybord settings
            currentSettings = wasdSettings;

            //setup the keybord string key dictionaries
            wasdKeyStrings = new Dictionary<PlayerAction, string>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, "Escape" },
                //{ PlayerAction.ViewInventory, "B" },
                //{ PlayerAction.MoveHorizontal, "A and D" },
                //{ PlayerAction.MoveVertical, "W and S" },
                //{ PlayerAction.Jump, "Spacebar" },
                //{ PlayerAction.Interact, "F" },
                //{ PlayerAction.FirePrimary, "Left mouse button" },
                //{ PlayerAction.FireSecondary, "Right mouse button" },
            };

            ijklKeyStrings = new Dictionary<PlayerAction, string>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, "Escape" },
                //{ PlayerAction.ViewInventory, "B" },
                //{ PlayerAction.MoveHorizontal, "J and L" },
                //{ PlayerAction.MoveVertical, "I and K" },
                //{ PlayerAction.Jump, "Spacebar" },
                //{ PlayerAction.Interact, "H" },
                //{ PlayerAction.FirePrimary, "Left mouse button" },
                //{ PlayerAction.FireSecondary, "Right mouse button" },
            };

            arrowKeyStrings = new Dictionary<PlayerAction, string>()
            {
                //leave PlayerAction.None out
                //{ PlayerAction.PauseGame, "Escape" },
                //{ PlayerAction.ViewInventory, "B" },
                //{ PlayerAction.MoveHorizontal, "Left and Right Arrows" },
                //{ PlayerAction.MoveVertical, "Up and Down Arrows" },
                //{ PlayerAction.Jump, "Up Arrow" },
                //{ PlayerAction.Interact, "Right Control" },
                //{ PlayerAction.FirePrimary, "Left mouse button" },
                //{ PlayerAction.FireSecondary, "Right mouse button" },
            };

            //set default key string dictionary
            currentKeyStrings = wasdKeyStrings;
        }

        //update the event system axis
        UpdateEventSystemAxis();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the input manager
    /// </summary>
    public static InputManager Instance
    {
        get { return instance ?? (instance = new InputManager()); }
    }

    /// <summary>
    /// Returns if a controller is connected or not
    /// </summary>
    public bool ControllerConnected
    { get; private set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// wasd settings
    /// </summary>
    public void ChangeToWASD()
    {
        currentSettings = wasdSettings;
        currentKeyStrings = wasdKeyStrings;
        UpdateEventSystemAxis();
    }

    /// <summary>
    /// ijkl settings
    /// </summary>
    public void ChangeToIJKL()
    {
        currentSettings = ijklSettings;
        currentKeyStrings = ijklKeyStrings;
        UpdateEventSystemAxis();
    }

    /// <summary>
    /// arrow settings
    /// </summary>
    public void ChangeToArrows()
    {
        currentSettings = arrowSettings;
        currentKeyStrings = arrowKeyStrings;
        UpdateEventSystemAxis();
    }

    /// <summary>
    /// gets the name of the current controlls
    /// </summary>
    /// <returns>the name of the controlls</returns>
    public string GetCurrentSettingsName()
    {
        if (currentSettings == wasdSettings)
        {
            return "WASD controlls";
        }
        else if (currentSettings == ijklSettings)
        {
            return "IJKL controlls";
        }
        else if (currentSettings == arrowSettings)
        {
            return "Arrow controlls";
        }
        else
        {
            return "Controller";
        }
    }

    /// <summary>
    /// Returns the details of the current controls
    /// </summary>
    /// <param name="action">the player action</param>
    /// <returns>details of the current controls</returns>
    public string GetCurrentSettingsDetails(PlayerAction action)
    {
        return currentKeyStrings[action];
    }

    /// <summary>
    /// Gets the string name of the current horizontal axis
    /// </summary>
    /// <returns>the horizontal axis</returns>
    public string GetCurrentHorizontalAxisName()
    {
        if (currentSettings == wasdSettings)
        {
            return "HorizontalWASD";
        }
        else if (currentSettings == ijklSettings)
        {
            return "HorizontalIJKL";
        }
        else if (currentSettings == arrowSettings)
        {
            return "HorizontalArrow";
        }
        else
        {
            return "Xbox_Left_Joystick_Horizontal";
        }
    }

    /// <summary>
    /// Gets the string name of the current vertical axis
    /// </summary>
    /// <returns>the vertcial axis</returns>
    public string GetCurrentVerticalAxisName()
    {
        if (currentSettings == wasdSettings)
        {
            return "VerticalWASD";
        }
        else if (currentSettings == ijklSettings)
        {
            return "VerticalIJKL";
        }
        else if (currentSettings == arrowSettings)
        {
            return "VerticalArrow";
        }
        else
        {
            return "Xbox_Left_Joystick_Vertical";
        }
    }

    /// <summary>
    /// Updates the event system axis
    /// </summary>
    public void UpdateEventSystemAxis()
    {
        //get the event system and update axis
        eventSystem = EventSystem.current;
        eventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = GetCurrentHorizontalAxisName();
        eventSystem.GetComponent<StandaloneInputModule>().verticalAxis = GetCurrentVerticalAxisName();
    }

    //-----Controlling Action Methods--------

    public float GetAxis(PlayerAction value)
    {
        return currentSettings[value].GetAxis();
    }

    public float GetAxisRaw(PlayerAction value)
    {
        return currentSettings[value].GetAxisRaw();
    }

    public bool GetButton(PlayerAction value)
    {
        return currentSettings[value].GetButton();
    }

    public bool GetButtonDown(PlayerAction value)
    {
        return currentSettings[value].GetButtonDown();
    }

    public bool GetButtonUp(PlayerAction value)
    {
        return currentSettings[value].GetButtonUp();
    }

    #endregion

    #region Private Methods



    #endregion

    #region Internal input class

    /// <summary>
    /// enum for which input type this new input will be
    /// </summary>
    public enum InputType
    {
        MouseButton, Axis, Button
    }

    /// <summary>
    /// CustomInput handles if it's a button, an axis, or a mouse button
    /// and act accordingly. Works for any controller.
    /// </summary>
    class CustomInput
    {
        #region Fields

        string inputAxisString;
        InputType inputType;
        bool canActivate = true;
        KeyCode key = KeyCode.None;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor for axis and joystick controllers
        /// </summary>
        /// <param name="input">the name of the string axis</param>
        /// <param name="type">the type of the input</param>
        public CustomInput(string input, InputType type)
        {
            inputAxisString = input;
            inputType = type;
        }

        /// <summary>
        /// constructor to handle keyboard key codes
        /// </summary>
        /// <param name="keyCode">keyboard key code</param>
        public CustomInput(KeyCode keyCode)
        {
            key = keyCode;
            inputType = InputType.Button;
        }

        #endregion

        #region Properties



        #endregion

        #region Public Methods

        public float GetAxis()
        {
            //axis
            if (inputType == InputType.Axis)
            {
                return Input.GetAxis(inputAxisString);
            }
            ////buttons
            //else if (inputType == InputType.Button)
            //{
            //    //keyboard buttons
            //    if (key != KeyCode.None)
            //    {
            //        return Input.GetKey(key) == true ? 1f : 0f;
            //    }
            //    //controller buttons
            //    else
            //    {
            //        return Input.GetButton(inputAxisString) == true ? 1f : 0f;
            //    }
            //}
            ////mouse buttons
            //else if (inputType == InputType.MouseButton)
            //{
            //    if (inputAxisString == "0")
            //    {
            //        return Input.GetMouseButton(0) == true ? 1f : 0f;
            //    }
            //    else if (inputAxisString == "1")
            //    {
            //        return Input.GetMouseButton(1) == true ? 1f : 0f;
            //    }
            //    else
            //    {
            //        Debug.Log("CustomInput GetAxis() in mouse button is an invalid string button! Returning 0f");
            //        return 0f;
            //    }
            //}
            else
            {
                Debug.Log("Error in CustomInput GetAxis() using " + inputType + ". Returning 0f");
                return 0f;
            }
        }

        public float GetAxisRaw()
        {
            if (inputType == InputType.Axis)
            {
                return Input.GetAxisRaw(inputAxisString);
            }
            else
            {
                Debug.Log("CustomInput GetAxisRaw() using " + inputType + " is an invalid type! Returning 0f");
                return 0f;
            }
        }

        public bool GetButton()
        {
            if (inputType == InputType.Button)
            {
                //keyboard buttons
                if (key != KeyCode.None)
                {
                    return Input.GetKey(key);
                }
                //controller buttons
                else
                {
                    return Input.GetButton(inputAxisString);
                }

            }
            //mouse
            else if (inputType == InputType.MouseButton)
            {
                if (inputAxisString == "0")
                {
                    return Input.GetMouseButton(0);
                }
                else if (inputAxisString == "1")
                {
                    return Input.GetMouseButton(1);
                }
                else
                {
                    Debug.Log("CustomInput GetButton() in mouse button is an invalid string button! Returning false");
                    return false;
                }
            }
            //controller
            else if (inputType == InputType.Axis)
            {
                return Input.GetAxis(inputAxisString) == 1f ? true : false;
            }
            else
            {
                Debug.Log("CustomInput GetButton() using " + inputType + " is an invalid type! Returning false");
                return false;
            }
        }

        public bool GetButtonDown()
        {
            //bool result = false;
            //if (isButton && Input.GetButtonDown(inputAxisString))
            //{
            //    result = true;
            //}
            //else if (!isButton && canActivate && Input.GetAxisRaw(inputAxisString) != 0)
            //{
            //    result = true;
            //    canActivate = false;
            //}
            //else if (!isButton && Input.GetAxisRaw(inputAxisString) == 0)
            //{
            //    canActivate = true;
            //}
            //return result;

            if (inputType == InputType.Button)
            {
                //keyboard
                if (key != KeyCode.None)
                {
                    return Input.GetKeyDown(key);
                }
                //controller
                else
                {
                    return Input.GetButtonDown(inputAxisString);
                }
            }
            else if (inputType == InputType.MouseButton)
            {
                if (inputAxisString == "0")
                {
                    return Input.GetMouseButtonDown(0);
                }
                else if (inputAxisString == "1")
                {
                    return Input.GetMouseButtonDown(1);
                }
                else
                {
                    Debug.Log("CustomInput GetButtonDown() in mouse button is an invalid string button! Returning false");
                    return false;
                }
            }
            //controller
            else if (inputType == InputType.Axis)
            {
                bool result = false;

                if (canActivate && Input.GetAxisRaw(inputAxisString) == 1f)
                {
                    result = true;
                    canActivate = false;
                }
                else if (Input.GetAxisRaw(inputAxisString) == 0f)
                {
                    canActivate = true;
                }

                return result;
            }
            else
            {
                Debug.Log("CustomInput GetButtonDown() using " + inputType + " is an invalid type! Returning false");
                return false;
            }
        }

        public bool GetButtonUp()
        {
            //bool result = false;
            //if (isButton && Input.GetButtonUp(inputAxisString))
            //{
            //    result = true;
            //}
            //else if (!isButton && Input.GetAxisRaw(inputAxisString) == 0)
            //{
            //    result = true;
            //}
            //return result;

            if (inputType == InputType.Button)
            {
                //keyboard
                if (key != KeyCode.None)
                {
                    return Input.GetKeyUp(key);
                }
                //controller
                else
                {
                    return Input.GetButtonUp(inputAxisString);
                }
            }
            else if (inputType == InputType.MouseButton)
            {
                if (inputAxisString == "0")
                {
                    return Input.GetMouseButtonUp(0);
                }
                else if (inputAxisString == "1")
                {
                    return Input.GetMouseButtonUp(1);
                }
                else
                {
                    Debug.Log("CustomInput GetButtonUp() in mouse button is an invalid string button! Returning false");
                    return false;
                }
            }
            //controller
            else if (inputType == InputType.Axis)
            {
                return Input.GetAxis(inputAxisString) == 1f ? true : false;
            }
            else
            {
                Debug.Log("CustomInput GetButtonUp() using " + inputType + " is an invalid type! Returning false");
                return false;
            }
        }

        #endregion
    }

    #endregion
}