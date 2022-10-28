using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsManager : MonoBehaviour
{
    public PlayerControls controls { get; private set; }

    private void OnEnable() {
        if(controls == null) {
            controls = new PlayerControls();
        }
        controls.Player.Enable();
    }

    private void OnDisable() {
        controls.Player.Disable();
    }
}
