using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpButton = KeyCode.Space;
    private const KeyCode AttackButton = KeyCode.Mouse0;
    private const KeyCode VampirismButton = KeyCode.Mouse1;

    public event Action Attacked;
    public event Action VampirismSelected;

    public float Horizontal { get; private set; }
    public bool IsJump {  get; private set; }

    private void Update()
    {
        Horizontal = Input.GetAxis(HorizontalAxis);

        if (Input.GetKeyDown(JumpButton))
        {
            IsJump = true;
        }
        else if (Input.GetKeyUp(JumpButton))
        {
            IsJump = false;
        }
        else if (Input.GetKeyDown(AttackButton))
        {
            Attacked?.Invoke();
        }
        else if (Input.GetKeyUp(VampirismButton))
        {
            VampirismSelected?.Invoke();
        }
    }
}
