using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivate
{
    public void Init();

    public void OnActivate();

    public void OnDeactivate();

    public bool IsActive { get; set; }

    public Vector3 SetPosition { get; set; }
}
