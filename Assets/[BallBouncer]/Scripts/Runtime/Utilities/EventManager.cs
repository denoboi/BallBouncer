using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //Use this event manager for your custom ingame events.
    
    public static ShapeCreateEvent OnShapeCreated = new ShapeCreateEvent();
    public static UnityEvent CloseShapePanel = new UnityEvent();
    public static UnityEvent OnProgressBarFull = new UnityEvent();


}

public class ShapeCreateEvent : UnityEvent<CreateShapeButton.StairTypes> { }
