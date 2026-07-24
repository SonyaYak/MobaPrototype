using UnityEngine;
using System.Collections.Generic;

public interface INeedTarget
{
    float GetViewDistance();
    void SetPotentialTargets(List<Unit> potentialTargets);
}
