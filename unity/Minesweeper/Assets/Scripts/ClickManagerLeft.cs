using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ClickManagerLeft : Singleton<ClickManagerLeft>
{
    public EventGameObject OnClick = new EventGameObject();
    public InvokeType active = InvokeType.both;

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("muis");
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin, direction: Vector2.up);
            if (EventSystem.current != null)
            {
                if (EventSystem.current.IsPointerOverGameObject())    // is the touch on the GUI
                {
                    // GUI Action
                    Debug.Log("hIT");
                    return;
                }
            }
            if (hitInfo.transform != null)
            {
                Debug.Log("Hit");
                Clickable clickeble = hitInfo.transform.GetComponent<Clickable>();
                if (clickeble != null)
                {
                    print("It's working");
                    switch (active)
                    {
                        case InvokeType.Clickmanager:
                            OnClick.Invoke(clickeble.gameObject);
                            break;
                        case InvokeType.clickable:
                            clickeble.OnClick.Invoke();
                            break;
                        case InvokeType.both:
                            OnClick.Invoke(clickeble.gameObject);
                            clickeble.OnClick.Invoke();
                            break;
                        default:
                            break;
                    }
                    
                    
                }
                else
                {
                    Debug.Log("clickeble");
                }
            }
        }
        /*if (Input.touchCount == 1)
        {
            Debug.Log("touch");
            RaycastHit hitInfo = new RaycastHit();
            if (Physics2D.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hitInfo) && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Hit");
                Clickeble clickeble = hitInfo.transform.GetComponent<Clickeble>();
                if (clickeble != null)
                {
                    print("It's working");
                    OnClick.Invoke(clickeble.gameObject);
                    clickeble.OnClick.Invoke();
                }
                else
                {
                    Debug.Log("clickeble");
                }
            }
        }*/



    }

    public void SetActiveState(int invokeType)
    {
        active = (InvokeType)invokeType;
    }
    public void SetActiveState(InvokeType invokeType)
    {
        active = invokeType;
    }

}
