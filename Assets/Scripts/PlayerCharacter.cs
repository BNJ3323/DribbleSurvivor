using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
    [Header("Données")]
    [SerializeField] private PlayerStats stats = new PlayerStats();
    [SerializeField] private List<TechnicalMove> activeMoves = new List<TechnicalMove>();
    [Header("Déplacement")]
    [SerializeField] private float baseForwardSpeed = 6f;
    [SerializeField] private float baseSideSpeed = 5f;
    [SerializeField] private float gravity = -20f;

    public PlayerStats Stats => stats;
    public Defenders CurrentDefender { get; private set; }

    private CharacterController controller;
    private float verticalVelocity;
    private float nextMoveTime;

    private void Awake() => controller = GetComponent<CharacterController>();

    private void OnEnable() => stats.Changed += RefreshUI;
    private void OnDisable() => stats.Changed -= RefreshUI;

    private void Start()
    {
        stats.ResetForRun();
        RefreshUI();
    }

    private void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.IsPlaying) return;
        Move();
        ReadTechnicalMoveInput();
        if (stats.CurrentEndurance <= 0) LoseBall();
    }

    private void Move()
    {
        float lateral = Input.GetAxisRaw("Horizontal");
        float forwardModifier = Input.GetAxisRaw("Vertical");
        float forwardSpeed = baseForwardSpeed + stats.SpeedLevel;
        if (forwardModifier > 0) forwardSpeed *= 1.25f;
        else if (forwardModifier < 0) forwardSpeed *= 0.65f;

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 motion = (transform.forward * forwardSpeed + transform.right * lateral * baseSideSpeed) * Time.deltaTime;
        motion.y = verticalVelocity * Time.deltaTime;
        controller.Move(motion);
        if (controller.isGrounded) verticalVelocity = -2f;
    }

    private void ReadTechnicalMoveInput()
    {
        for (int i = 0; i < activeMoves.Count && i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) UseTechnicalMove(activeMoves[i]);
        }
    }

    public void EnterChallenge(Defenders defender) => CurrentDefender = defender;

    public void UseTechnicalMove(TechnicalMove move)
    {
        if (CurrentDefender == null || Time.time < nextMoveTime || move == null) return;
        if (!move.CanBeUsed(stats)) return;
        stats.SpendEndurance(move.EnduranceCost);
        nextMoveTime = Time.time + move.Cooldown;
        CurrentDefender.ReactToMove(move);
    }

    public void OnDefenderDribbled(Defenders defender)
    {
        if (CurrentDefender != defender) return;
        CurrentDefender = null;
        stats.AddAura(defender.Level);
        stats.AddMoney(defender.Level);
    }

    public void LoseBall() => GameManager.Instance?.LoseRun();

    private void RefreshUI()
    {
        if (UIManager.Instance != null) UIManager.Instance.UpdatePlayerStats(stats);
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!GameManager.Instance || !GameManager.Instance.IsPlaying)
            return;

        Defenders defender = hit.collider.GetComponent<Defenders>();

        if (defender != null)
        {
            LoseBall();
        }
    }
}