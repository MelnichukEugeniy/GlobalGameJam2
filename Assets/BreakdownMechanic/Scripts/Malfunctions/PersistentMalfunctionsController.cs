using System;
using System.Collections.Generic;

public class PersistentMalfunctionsController : PersistentSingleton<PersistentMalfunctionsController>
{
    private Dictionary<Type, MalfunctionBase> malfunctionsMap;

    protected override void Awake()
    {
        base.Awake();

        malfunctionsMap = new Dictionary<Type, MalfunctionBase>();
    }

    public void RegisterMalfunction<T>(T malfunction) where T : MalfunctionBase
    {
        var type = typeof(T);
        malfunctionsMap[type] = malfunction;
    }
}