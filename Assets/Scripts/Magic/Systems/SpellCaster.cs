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
            case SelfSpellData selfSpell: CastSelf(selfSpell); break;
            case TargetSpellData targetSpell: CastTarget(targetSpell,worldPosition); break;
            case NonTargetSpellData nonTarget: CastNonTarget(nonTarget); break;
            case AoeSpellData aoeSpell:
                {
                    CastAoe(aoeSpell, aoeSpell.isTarget
                        ? worldPosition
                        : m_casterTransform.position);
                    break;
                }
        }
    }

    private void CastSelf(SelfSpellData spell) { }
    
    private void CastTarget(TargetSpellData spell, Vector3 worldPosition) { }
    
    private void CastNonTarget(NonTargetSpellData spell) { }
    
    private void CastAoe(AoeSpellData spell, Vector3 worldPosition) { }
}

