using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    //sound for the button
    [SerializeField]
    UISoundEffect buttonSound;

    //scene to go to if applicable. leave on None if n/a
    [SerializeField]
    Scenes goToScene;

    //animated button fields
    bool increaseScale = false;
    float timer = 0f;
    float animationTimer = 0.2f;
    float scaleRate = 1f;

    // Update is called once per frame
    protected virtual void Update ()
    {
        //icrease the button in an animation effect when true. shrink back when false
        if (increaseScale)
        {
            if (timer < animationTimer)
            {
                timer += Time.deltaTime;
                GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x + (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.y + (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.z);
            }
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x - (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.y - (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.z);
            }
            else
            {
                timer = 0;
                GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, GetComponent<RectTransform>().localScale.z);
            }
        }
    }

    #region Handler Methods

    public void OnPointerEnter(PointerEventData pointerData)
    {
        GetComponent<Button>().Select();
        increaseScale = true;
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuButtonFocused);
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
        increaseScale = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
    }

    #endregion

    #region Application Methods

    public void OnChangeSceneClick()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        MySceneManager.Instance.ChangeScene(goToScene);
    }

    public void Quit()
    {
        Application.Quit();
    }


    //public void OnMainMenuChange()
    //{
    //    UIManager.Instance.MainMenuControl.ChangeMenu(mainMenuToGoTo);
    //}


    //public void OnPreLevelMenuChange()
    //{
    //    UIManager.Instance.PreLevelMenuControl.ChangeMenu(preLevelMenuToGoTo);
    //}

    #endregion

    #region Game Methods



    #endregion
}
