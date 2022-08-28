using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VignetteHack : LocomotionProvider
{
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected virtual void Update()
    {
        locomotionPhase = LocomotionPhase.Moving;
    }
}
