using UnityEngine;

public class Defenders : MonoBehaviour
{
    public enum DefenderType { Basic, Fast, Strong, Technical, Elite, Boss }
    public enum DefenderState { Approaching, Challenging, Dribbled, Blocking }

    [SerializeField] private DefenderType type = DefenderType.Basic;
    [SerializeField, Min(1)] private int level = 1;
    [SerializeField] private float approachSpeed = 4f;
    [SerializeField] private float challengeDistance = 2f;
    [SerializeField] private Renderer[] feedbackRenderers;
    [SerializeField] private Color successColor = Color.green;

    public DefenderType Type => type;
    public int Level => level;
    public DefenderState State { get; private set; } = DefenderState.Approaching;

    private PlayerCharacter player;
    private Color[] initialColors;

    private void Awake()
    {
        initialColors = new Color[feedbackRenderers.Length];
        for (int i = 0; i < feedbackRenderers.Length; i++)
            if (feedbackRenderers[i] != null) initialColors[i] = feedbackRenderers[i].material.color;
    }

    private void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.IsPlaying || State != DefenderState.Approaching) return;
        if (player == null) player = FindFirstObjectByType<PlayerCharacter>();
        if (player == null) return;

        Vector3 target = player.transform.position;
        target.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, target, approachSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) <= challengeDistance)
        {
            State = DefenderState.Challenging;
            player.EnterChallenge(this);
        }
    }

    public void ReactToMove(TechnicalMove move)
    {
        if (State != DefenderState.Challenging) return;
        bool success = move != null && move.IsEffectiveAgainst(this) && move.StarLevel >= level;
        if (success) BeDribbled(); else BlockPlayer();
    }

    private void BeDribbled()
    {
        State = DefenderState.Dribbled;
        for (int i = 0; i < feedbackRenderers.Length; i++)
            if (feedbackRenderers[i] != null) feedbackRenderers[i].material.color = successColor;
        if (player != null) player.OnDefenderDribbled(this);
        Destroy(gameObject);
    }

    private void BlockPlayer()
    {
        State = DefenderState.Blocking;
        player?.LoseBall();
    }
}