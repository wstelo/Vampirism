using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(PlayerAnimatorController), typeof(Mover))]
[RequireComponent(typeof(Health), typeof(Rotator), typeof(ItemDetector))]
[RequireComponent(typeof (CoinWallet), typeof(Vampirism))]

public class Player : Character
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private InputService _inputService;
    [SerializeField] private float _jumpForceHorizontal;
    [SerializeField] private float _jumpForceVertical;
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Weapon _weapon;

    private PlayerAnimatorController _animatorController;
    private StateMachine _stateMachine;
    private Rigidbody2D _rigidbody;
    private Health _health;
    private Mover _mover;

    private void Awake()
    {
        _animatorController = GetComponent<PlayerAnimatorController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();
        _weapon.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(_vampirism != null)
        _inputService.VampirismSelected += ActivateVampirism;
    }

    private void OnDisable()
    {
        if (_vampirism != null)
            _inputService.VampirismSelected -= ActivateVampirism;
    }

    private void Start()
    {
        _health.Ended += ShowGameOverScreen;
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new PlayerIdleAttackState(_stateMachine,_animatorController, _mover, _weapon));
        _stateMachine.AddState(new PlayerRunAttackState(_stateMachine, _animatorController, _mover, _groundChecker,_moveSpeed, _weapon));
        _stateMachine.AddState(new PlayerIdleState(_stateMachine, _animatorController, _mover, _groundChecker, _inputService));
        _stateMachine.AddState(new PlayerMoveState(_stateMachine, _animatorController, _moveSpeed, _mover, _groundChecker, _inputService));
        _stateMachine.AddState(new PlayerJumpState(_stateMachine, _animatorController, _rigidbody, _jumpForceVertical, _jumpForceHorizontal, _mover, _groundChecker, _inputService));
        _stateMachine.SetState<PlayerIdleState>();
    }

    private void Update()
    {
        _mover.SetDirection(_inputService.Horizontal);
        _stateMachine?.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine?.FixedUpdate();
    }

    private void ActivateVampirism()
    {
        _vampirism.Activate();
    }

    private void ShowGameOverScreen (Health health)
    {
        _health.Ended -= ShowGameOverScreen;
        // ��������������� ����� ���������. �� UI ��� �� ����� �� ����� �� ���� ������)
    }
}
