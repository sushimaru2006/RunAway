using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // floats
    // 移動速度
    public float MoveSpeed;
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
        JumpVec = new Vector3(0, JumpPower, 0);
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
    }

    // 足からジャンプのフラグを変更するためのメソッド
    public void SetFalseIsJump()
    {
        IsJump = false;
    }
}
