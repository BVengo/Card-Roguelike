using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardDefinition : ScriptableObject
{
    public string DisplayName => displayName;

    public bool IsSticky => isSticky;

    [SerializeField]
    string displayName;

    [SerializeField]
    bool isSticky = false;
    
    public abstract CardData CreateData(GameCore core);

    public abstract CardDisplay CreateDisplay(CardInstance instance);

    public abstract void Apply(GameCore core, CardInstance instance);

    public virtual void EndOfRound(GameCore core, CardInstance instance)
    {
        if (!IsSticky)
        {
            core.CardManager.ReplaceCard(instance);
        }
    }
}

public class CardInstance
{
    public CardDefinition Definition { get; }

    public CardData Data { get; }

    public event CardInstanceHandler Changed;

    public CardInstance(GameCore core, CardDefinition definition)
    {
        Definition = definition;

        Data = Definition.CreateData(core);

        Data.Changed += OnChanged;
    }

    void OnChanged()
    {
        Changed?.Invoke(this);
    }

    public CardDisplay CreateDisplay()
    {
        CardDisplay result = Definition.CreateDisplay(this);

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

        instance.Changed += Refresh;
    }

    public virtual void Refresh(CardInstance instance)
    {

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Clicked?.Invoke(instance);
    }
}