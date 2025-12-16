using UnityEngine;

public class SpellCaster
{
    private Transform m_casterTransform;

    public SpellCaster(Transform casterTransforrm)
    {
        m_casterTransform = casterTransforrm;
    }

    public void Cast(BaseSpellData spell, Vector3 worldPosition)
    {
        if (!spell)
        {
            return;
        }
        switch (spell)
        {
            case SelfSpellData selfData: CastSelf(); break;
            case TargetSpellData targetData: CastTarget(); break;
            case NonTargetSpellData nonTargetData: CastNonTarget(); break;
            case AoeSpellData aoeSpellData:
                {
                    CastAoe(aoeSpellData, aoeSpellData.isTarget
                        ? worldPosition
                        : m_casterTransform.position);
                    break;
                }
        }
    }

    private void CastSelf(SelfSpellData spell) { }
    
    private void CastTarget(TargetSpellData spell, Vector3 worldPosition) { }
    
    private void CastNonTarget(NonTargetSpellData spell) { }
    
    private void CastAoe(NonTargetSpellData spell, Vector3 worldPosition) { }
}

