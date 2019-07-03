using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MoveObject
{
    [SerializeField]
    private GameObject _topPipe = null;

    private float _defalutTopPositionY = 0.0f;
    private float _defalutBasePositionY = 0.0f;

    private bool _bCheck = false;

    private void Start()
    {
        _defalutTopPositionY = _topPipe.transform.localPosition.y;
        _defalutBasePositionY = transform.position.y;
    }

    public void SetHeight( float value )
    {
        Vector3 result = _topPipe.transform.localPosition;
        result.y = value + _defalutTopPositionY;

        _topPipe.transform.localPosition = result;
    }

    public void SetPositionY( float value )
    {
        Vector3 result = transform.position;
        result.y = value + _defalutBasePositionY;

        transform.position = result;
    }

    override public void GameUpdate()
    {
        base.GameUpdate();
    }

    override protected void FinishEndPosition()
    {
        Manager.Instance.Remove( this );
    }

    /// <summary>
    /// Bird 와 위치를 검사하는 처리
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool isNeedInvokeScoreCheck(Vector3 target )
    {
        if( !_bCheck )
        {
            if( transform.position.x <= target.x )
            {
                _bCheck = true;
                return true;
            }
        }
        return false;
    }
}
