using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Ebac.Core.Singleton;
using Cloht;
using Cloth;

public class Player : Singleton<Player>, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private float vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;
    private bool _alive = true;
    private bool isRespawning;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }


    #region LIFE
    private void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            characterController.enabled = false;

            Invoke(nameof(Revive), 3f);
        }
    }

    public void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive"); 
        Respawn();
        characterController.enabled = true;
        Invoke(nameof(TurnOnCollider), 1f);
    }

    private void TurnOnCollider()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i =>
        {
            if (i != null) i.Flash();
            EffectsManager.Instance.ChangeVignette();
            ShakeCamera.Instance.Shake();
        });
    }
    public void Damage(float damage)
    {
        healthBase.Damage(damage);

    }

    public void Damage(float damage, Vector3 dir)
    {
        healthBase.Damage(damage);
    }

    #endregion
    private void Update()
    {
        if (!_alive) return;
        if (isRespawning) return; 
        if (characterController == null || !characterController.enabled) return;

        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        #region JUMP
        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed;
            }
        }
        #endregion

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);

    }
    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckPointManager.Instance.HasCheckPoint())
        {
            isRespawning = true;

            characterController.enabled = false;

            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckpoint();

            vSpeed = 0;

            characterController.enabled = true;

            StartCoroutine(EndRespawn()); //  chama aqui
        }
    }

    private IEnumerator EndRespawn()
    {
        yield return new WaitForEndOfFrame();
        isRespawning = false;
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }
}

