using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // floats
    // 移動速度
    public float WalkSpeed;
    public float DashSpeed;
    private float MoveSpeed;
    // ジャンプ力
    public float JumpPower;

    // vector
    private Vector3 JumpVec;

    // bool
    private bool IsJump = false;

    // RigidBody
    public Rigidbody MyRib;

    private void Start()
    {
        // ジャンプする時に加える力のベクトルを準備
        JumpVec = new Vector3(0, JumpPower, 0);

        // 移動速度を設定
        MoveSpeed = WalkSpeed;
    }

    private void FixedUpdate()
    {
        // 入力を受け取る
        var inputKey = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // 入力があるか
        if (inputKey.magnitude > 0f)
        {
            // 移動
            this.gameObject.transform.position += inputKey * MoveSpeed * Time.deltaTime;
        }

        // スペースキーが押されたら
        if (Input.GetKey(KeyCode.Space) && !IsJump)
        {
            // ジャンプの処理
            MyRib.AddForce(JumpVec, ForceMode.Impulse);

            // フラグの処理
            IsJump = true;
        }

        // Shiftが押され時の処理
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 移動速度を変更
            MoveSpeed = DashSpeed;
        }

        // Shiftが離された時
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // 移動速度を変更
            MoveSpeed = WalkSpeed;
        }
    }

    // 足からジャンプのフラグを変更するためのメソッド
    public void SetFalseIsJump()
    {
        IsJump = false;
    }
}
