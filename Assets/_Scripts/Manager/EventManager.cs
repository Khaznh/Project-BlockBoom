using System;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public Action OnBlockPlaced;
    public Action OnColumnOrRowComplete;
    public Action OnLose;
}
