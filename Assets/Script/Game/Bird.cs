using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour, IGameObject
{
    [SerializeField]
    private Rigidbody2D _rigidbody = null;

    [SerializeField]
    private float _jumpValue = 2f;

    private Vector3 _startPosition = Vector3.zero;
    private Quaternion _startRotation = Quaternion.identity;

    private AudioSource audio;
    private AudioSource audio2;
    public AudioSource audio3;
    public AudioClip backSound;
    public AudioClip jumpSound;
    public AudioClip dieSound;


    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio2 = this.gameObject.AddComponent<AudioSource>();
        this.audio3 = this.gameObject.AddComponent<AudioSource>();
        this.audio3.clip = this.backSound;
        this.audio3.Play();
        this.audio.clip = this.jumpSound;
        this.audio2.clip = this.dieSound;
        this.audio.loop = false;
        Screen.SetResolution(400, 800, false);

    }

    public void Init()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
    }

    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        
    }

    public void GameUpdate()
    {
        if( Input.GetKeyDown( KeyCode.Space ))
        {
            _rigidbody.AddForce( new Vector2( 0, _jumpValue ) );
            this.audio.Play();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _rigidbody.AddForce(new Vector2(0, _jumpValue));
            this.audio.Play();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            
            SceneManager.LoadScene("Main");
        }
    }

    public void FreezePositionY( bool value )
    {
        _rigidbody.constraints = value ? RigidbodyConstraints2D.FreezePositionY : RigidbodyConstraints2D.None;
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        //Debug.Log( collision.gameObject.tag );
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                Manager.Instance.isPlay = false;
                gameObject.GetComponent<Animator>().Play("Die");
                this.audio2.Play();
                this.audio3.Stop();
                Time.timeScale = 0;
                break;

                
        }
    }
}
