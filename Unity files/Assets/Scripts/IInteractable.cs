using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Function will get called if player interacts with this object. This object needs to be set on an "Interactable" Layer to work.
/// </summary>


public interface IInteractable {

    void OnInteractionPressed();

}
