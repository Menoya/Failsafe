using System;
using System.Collections;
using UnityEngine;


public interface ILimitedEffect
{
    public IEnumerator EndEffect(Action callback);

}
