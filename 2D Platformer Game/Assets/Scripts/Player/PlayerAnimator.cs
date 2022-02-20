using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private IPlayerController _player;
    private bool _playerGrounded;
    private Vector2 _movement;

    void Awake() => _player = GetComponentInParent<IPlayerController>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;

        // Flip the sprite
        if(_player.Input.X != 0) transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
        
        // Speed up idle while running
        _anim.SetFloat("Speed", Mathf.Abs(_player.Input.X));

        if (_player.JumpingThisFrame) {
            _anim.SetTrigger(JumpKey);
            _anim.ResetTrigger(GroundedKey);
        }
    }

    private static readonly int GroundedKey = Animator.StringToHash("Grounded");
    private static readonly int IdleSpeedKey = Animator.StringToHash("Idle");
    private static readonly int JumpKey = Animator.StringToHash("Jump");
}
