using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class EventGameObject : UnityEvent<GameObject> { }

public enum InvokeType
{
    Clickmanager, 
    clickable,
    both
}

public class ClickManager : MonoBehaviour
{
    public EventGameObject OnClick = new EventGameObject();
    public InvokeType active = InvokeType.both;
    public static ClickManager Instance;

    private void Start()
    {
        Instance = this;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
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
