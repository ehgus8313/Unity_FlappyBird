using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Manager : Singleton<Manager>
{
    public Text kwonTx;
    

    [SerializeField]
    private Bird _bird = null;
    [SerializeField]
    private Ground _ground = null;
    [SerializeField]
    private Pipe _pipe = null;

    [SerializeField]
    public float _speed = 0.001f;
    [SerializeField]
    private float _createTime = 1.0f;
    [SerializeField]
    private float _pipeRandomHeight = 0.4f;
    [SerializeField]
    private float _pipeRandomPositionY = 0.5f;

    private float _currentTime = 0.0f;
    


    private List<Pipe> _pipeList = new List<Pipe>();

    private bool _bPlay = false;
    private int _score = 0;
    private int count = 0;
    private int _bestScore = 0;
    private bool _bCurrentBestScore = false;
    

    public int Score { get { return _score; } }
    public int Count { get { return count; } }
    public int BestScore { get { return _bestScore; } }
    public bool isCurrentBestScore { get { return _bCurrentBestScore; } }

    public float Speed { get { return _speed; } }
    public bool isPlay {
        get { return _bPlay; }
        set
        {
            _bPlay = value;
            if( !_bPlay )
            {
                UIManager.Instance.InvokeGameOver();
            }
        }
    }

    private void Start()
    {
        Init();
        UIManager.Instance.ShowTitle();
        _bestScore = PlayerPrefs.GetInt( "_bestScore" );
    }

    private void Init()
    {
        _bCurrentBestScore = false;
        _bPlay = false;
        _score = 0;
        count = 0;
        _currentTime = 0.0f;
        _bird.Init();
        _ground.Init();
        _pipeList.ToArray().ToList().ForEach( x => Remove( x ) );
        UIManager.Instance.Init();
        
    }

    public void Replay()
    {
        Debug.Log( "Replay" );
        Init();
        UIManager.Instance.ShowScore();
        _bPlay = true;
    }

    void Update()
    {
        _bird.FreezePositionY( !_bPlay );
        if( _bPlay )
        {
            _currentTime += Time.deltaTime;
            if( _createTime < _currentTime )
            {
                _currentTime = 0;

                _pipe.SetHeight( Random.Range( 0.0f, _pipeRandomHeight ) );
                _pipe.SetPositionY( Random.Range( 0.0f, _pipeRandomPositionY ) );
                _pipeList.Add( GameObject.Instantiate( _pipe ) );
            }

            _bird.GameUpdate();
            _ground.GameUpdate();
            _pipeList.ForEach( ( x ) =>
            {
                x.GameUpdate();
                if( x.isNeedInvokeScoreCheck( _bird.transform.position ) )
                {
                    InvokeScore();
                }
            } );
        }
    }

    public void Remove( Pipe target )
    {
        _pipeList.Remove( target );
        Debug.Log( target.gameObject.name + " / " + _pipeList.Count );
        DestroyImmediate( target.gameObject );
    }

    private void InvokeScore()
    {
        _score++;
        count++;

        if( _bestScore < _score )
        {
            _bCurrentBestScore = true;
            _bestScore = _score;

            PlayerPrefs.SetInt( "_bestScore", _bestScore );
            PlayerPrefs.Save();
            
        }

        UIManager.Instance.Score = _score;
        
            if (count == 5)
            {
            kwonTx.text = "201444001";
            count = 0;
            }
            else
            {
                kwonTx.text = " ";
            }
            // UI에 업데이트 한다.
        }
    }
    

