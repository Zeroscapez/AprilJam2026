using System.Collections;
using UnityEngine;

public class EnemyPopUpMover : EnemyMovement
{
    [Header("Whack-a-Mole Settings")]
    public float PeekHeight = 3f;       // How far above the start position it rises
    public float PeekDuration = 1.5f;   // How long it stays at the top before retreating

    private Vector3 _hiddenPosition;
    private Vector3 _peekPosition;
    private bool _cycling;

    void Start()
    {
        _hiddenPosition = transform.position;
        _peekPosition = _hiddenPosition + Vector3.up * PeekHeight;

        StartCoroutine(Cycle());
    }

    // EnemyMovement.Update() calls Move() every frame but the
    // coroutine drives this enemy instead, so Move() is left empty
    protected override void Move() { }

    IEnumerator Cycle()
    {
        while (true)
        {
            // Rise up
            yield return MoveToPosition(_peekPosition);

            // Peek
            yield return new WaitForSeconds(PeekDuration);

            // Retreat down
            yield return MoveToPosition(_hiddenPosition);
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
    }
}
