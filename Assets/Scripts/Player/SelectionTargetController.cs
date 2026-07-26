
public class SelectionTargetController : TargetController
{
    public void TrySelectTarget(Unit target)
    {
        if (target.GetTeam().GetTeamId() == _teamTag.GetTeamId())
            return;

        SetTarget(target);
    }

    public void ResetTarget()
    {
        SetTarget(null);
    }
}
