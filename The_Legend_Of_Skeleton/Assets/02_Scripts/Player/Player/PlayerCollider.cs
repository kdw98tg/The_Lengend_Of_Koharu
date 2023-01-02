using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public delegate void VoidVector3Delegate(Vector3 _value);
    private VoidVector3Delegate hitSlapDelegate = null;
    private VoidVector3Delegate hitBiteDelegate = null;
    private VoidVector3Delegate hitTailAttackDelegate = null;
    private VoidVector3Delegate hitTailAttackGroundDelegate = null;
    private VoidVector3Delegate hitHeadbuttDelegate = null;
    private VoidVector3Delegate hitThunderPlaneDelegate = null;
    private VoidVector3Delegate hitWhirlwindDelegate = null;
    private VoidVector3Delegate hitSmallDragonDelegate = null;
   


    private CapsuleCollider m_CC = null;

    private void Awake()
    {
        m_CC = GetComponent<CapsuleCollider>();
    }

    #region Boss_Usurper

    public void HitSlap(Vector3 _slapPos)
    {
        hitSlapDelegate?.Invoke(_slapPos);
    }
    
    public void HitBite(Vector3 _bitePos)
    {
        hitBiteDelegate?.Invoke(_bitePos);
    }

    public void HitTailAttack(Vector3 _tailAttackPos)
    {
        hitTailAttackDelegate?.Invoke(_tailAttackPos);
    }
    public void HitTailAttackGround(Vector3 _tailAttackGround)
    {
        hitTailAttackGroundDelegate?.Invoke(_tailAttackGround);
    }
   

    public void HitHeadbutt(Vector3 _headbuttPos)
    {
        hitHeadbuttDelegate?.Invoke(_headbuttPos);
    }

    public void HitThunderPlane(Vector3 _planePos)
    {
        hitThunderPlaneDelegate?.Invoke(_planePos);
    }

    public void HitWhirlwind(Vector3 _whirlwindPos)
    {
        hitWhirlwindDelegate?.Invoke(_whirlwindPos);
    }
    #endregion

    #region Boss_SoulEater



    public void HitSmallDragon(Vector3 _boomPos)
    {
        hitSmallDragonDelegate?.Invoke(_boomPos);
    }

    #endregion
    public float GetRadius()    
    {
        return m_CC.radius;
    }

  
    public void OnKnockdownCallback()
    {
        this.gameObject.layer = LayerMask.NameToLayer("IGNOREATTACK");
    }
    public void OffKnockdownCallback()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PLAYER");
    }

    public void OnEvadeStateCallback(float _startTime, float _durationTime)
    {
        SetIgnoreLayer(_startTime, _durationTime, "IGNOREATTACK");
    }

    public void OnJumpCallback(float _startTime, float _durationTime)
    {
        SetIgnoreLayer(_startTime, _durationTime, "JUMP");
    }

    // 회피하면 레이어처리로 일정시간 피격무시
    private void SetIgnoreLayer(float _startTime, float _durationTime, string _layer)
    {
        StartCoroutine(SetIgnoreLayerCoroutine(_startTime, _durationTime, _layer));
    }

    private IEnumerator SetIgnoreLayerCoroutine(float _startTime, float _durationTime, string _layer)
    {
        yield return new WaitForSeconds(_startTime);
        this.gameObject.layer = LayerMask.NameToLayer(_layer);

        yield return new WaitForSeconds(_durationTime);
        this.gameObject.layer = LayerMask.NameToLayer("PLAYER");
    }


    public void Init(VoidVector3Delegate _hitSlapCallback, VoidVector3Delegate _hitBiteCallback,
                       VoidVector3Delegate _hitTailAttackCallback, VoidVector3Delegate _hitTailAttackGroundCallback,
                        VoidVector3Delegate _hitHeadbuttCallback, VoidVector3Delegate _hitThunderPlaneCallback,
                        VoidVector3Delegate _hitWhirlwindCallback, VoidVector3Delegate _hitSmallDragonCallback)
    {
        hitSlapDelegate = _hitSlapCallback;
        hitBiteDelegate = _hitBiteCallback;
        hitTailAttackDelegate = _hitTailAttackCallback;
        hitTailAttackGroundDelegate = _hitTailAttackGroundCallback;
        hitHeadbuttDelegate = _hitHeadbuttCallback;
        hitThunderPlaneDelegate = _hitThunderPlaneCallback;
        hitWhirlwindDelegate = _hitWhirlwindCallback;
        hitSmallDragonDelegate = _hitSmallDragonCallback;
    }
}
