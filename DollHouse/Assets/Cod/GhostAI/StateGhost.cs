using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateGhost : MonoBehaviour
{
    public abstract StateGhost RunCurrentState();
}
