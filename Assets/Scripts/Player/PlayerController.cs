using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float doubleJumpForce = 9f;
    [SerializeField]private float newGravityValue = -25f;
    [SerializeField] private Transform pointToFindGround;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float distanceToCheck;
    private bool _onGround;
    private bool _canDoubleJump;
    private bool _isPlayerAlive;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _canTakeDamage = true;
    [SerializeField] private float timeToResetPlayer = 2f;

    public Transform modelHolder;
    
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        Physics.gravity = new Vector3(0, newGravityValue, 0);
        _isPlayerAlive = true;

        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    private void Update()
    {
        _onGround = Physics.OverlapSphere(pointToFindGround.position, distanceToCheck, whatIsGround).Length>0;

        if (_isPlayerAlive)
        {
            Jump();
        }
        
        AnimatePlayer();
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) && _onGround)
        {
            _rb.velocity = new Vector3(0f, jumpForce, 0f);
            _canDoubleJump = true;
            AudioManager.Instance.PlayJumpSound();
        }else if (Input.GetMouseButtonDown(0) &&_canDoubleJump)
        {
            _rb.velocity = new Vector3(0f, doubleJumpForce, 0f);
            _canDoubleJump = false;
            AudioManager.Instance.PlayJumpSound();
        }
    }

    private void AnimatePlayer()
    {
        _anim.SetBool(TagManager.PLAYER_IS_RUN_PARAMETR,GameManager.Instance.canMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.OBSTACLE_TAG) && _canTakeDamage)
        {
            
            GameManager.Instance.EndGame();
            
            _isPlayerAlive = false;
            
            _anim.SetBool(TagManager.PLAYER_IS_RUN_PARAMETR,_isPlayerAlive);
            
            _rb.constraints = RigidbodyConstraints.None;
            _rb.velocity =
                new Vector3(
                    Random.Range(GameManager.Instance.GetWorldSpeed() / 2f, -GameManager.Instance.GetWorldSpeed()/2f),
                    2.5f, -GameManager.Instance.GetWorldSpeed());
            
            AudioManager.Instance.PlayGameOverMusic();
        }

        if (other.CompareTag(TagManager.COIN_TAG))
        {
            GameManager.Instance.AddCoin();
            
            Destroy(other.gameObject);
        }
    }

    public void ResetPlayer()
    {
        StartCoroutine(nameof(_ResetPlayerCo));
    }

    private IEnumerator _ResetPlayerCo()
    {
        UIManager.Instance.FadeIn();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;

        transform.rotation = _startRotation;
        transform.position = _startPosition;
        _canTakeDamage = false;

        yield return new WaitForSeconds(timeToResetPlayer);
        
        UIManager.Instance.FadeOut();
        GameManager.Instance.canMove = true;
        _canTakeDamage = true;
        _isPlayerAlive = true;
        
        AudioManager.Instance.PlayGameMusic();
    }
}
