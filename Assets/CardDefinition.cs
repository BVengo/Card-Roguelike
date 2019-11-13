using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardDefinition : ScriptableObject
{
    public string DisplayName => displayName;

    [SerializeField]
    string displayName;
    
    public abstract object CreateData();

    public abstract CardDisplay CreateDisplay(object data);

    public abstract void Apply(CardOffers offers, object data);
}

public class CardInstance
{
    public CardDefinition Definition { get; }

    public object Data { get; }

    public CardInstance(CardDefinition definition)
    {
        Definition = definition;

        Data = Definition.CreateData();
    }

    public CardDisplay CreateDisplay()
    {
        CardDisplay result = Definition.CreateDisplay(Data);

        result.Initialize(this);

        return result;
    }
}

public class CardDisplay : MonoBehaviour, IPointerDownHandler
{
    public event CardInstanceHandler Clicked;

    CardInstance instance;

    public void Initialize(CardInstance instance)
    {
        this.instance = instance;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Clicked?.Invoke(instance);
    }
}