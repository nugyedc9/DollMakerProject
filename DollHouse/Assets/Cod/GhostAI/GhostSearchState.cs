using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSearchState : StateGhost
{
    public override StateGhost RunCurrentState()
    {
        return this;
    }
}
