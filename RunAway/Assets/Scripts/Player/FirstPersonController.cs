using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class FirstPersonController : MonoBehaviour
{
    // floats
    // 移動速度
    public float WalkSpeed;
    public float DashSpeed;
    private float MoveSpeed;
    // ジャンプ力
    public float JumpPower;
    // 感度
    public float XSensityvity;
    public float YSensityvity;

    // Quartenion
    private Quaternion CamRot;
    private Quaternion CharaRot;

    // Camera
    public Camera MyCam;

    // vector
    private Vector3 JumpVec;

    // bool
    private bool IsJump = false;

    // string
    private string MoveMode = "idol";

    // instances
    [SerializeField] private CurveControlledBob headBob_ = new CurveControlledBob();

    // RigidBody
    public Rigidbody MyRib;

    private void Start()
    {
        // ジャンプする時に加える力のベクトルを準備
        JumpVec = new Vector3(0, JumpPower, 0);

        // 移動速度を設定
        MoveSpeed = WalkSpeed;

        // 最初の向きを取得
        CamRot = MyCam.transform.rotation;
        CharaRot = transform.rotation;

        // bobのセットアップ
        headBob_.Setup(MyCam, 1.0f);
    }

    private void Update()
    {
        // マウスの入力を受け取る
        float xRot = Input.GetAxis("Mouse X") * YSensityvity;
        float yRot = Input.GetAxis("Mouse Y") * XSensityvity;

        // カメラとキャラの回転の向きを変更
        CamRot *= Quaternion.Euler(-yRot, 0, 0);
        CharaRot *= Quaternion.Euler(0, xRot, 0);

        // カメラとキャラを回転
        MyCam.transform.localRotation = CamRot;
        transform.localRotation = CharaRot;
    }

    private void FixedUpdate()
    {
        // 入力を受け取る
        var inputKey = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // 入力があるか
        if (inputKey.magnitude > 0f)
        {
            // 移動モードを変更
            MoveMode = MoveMode == "idol" ? "walk" : MoveMode;

            // 移動
            transform.position += MyCam.transform.forward * inputKey[2] * MoveSpeed * Time.deltaTime + MyCam.transform.right * inputKey[0] * MoveSpeed * Time.deltaTime;
        }
        else
        {
            // 移動モードを変更
            MoveMode = "idol";
        }

        // 歩きのカメラの揺れ
        Vector3 handBob = headBob_.DoHeadBob(0.8f, MoveMode);
        MyCam.transform.localPosition = handBob;

        // スペースキーが押されたら
        if (Input.GetKey(KeyCode.Space) && !IsJump)
        {
            // ジャンプの処理
            MyRib.AddForce(JumpVec, ForceMode.Impulse);

            // フラグの処理
            IsJump = true;

            // モードを変更
            MoveMode = "jump";
        }

        // Shiftが押され時の処理
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 移動速度を変更
            MoveSpeed = DashSpeed;

            // 移動モードを変更
            MoveMode = "dash";
        }

        // Shiftが離された時
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // 移動速度を変更
            MoveSpeed = WalkSpeed;

            // 移動モードを変更
            MoveMode = "walk";
        }
    }

    // 足からジャンプのフラグを変更するためのメソッド
    public void SetFalseIsJump()
    {
        // フラグの変更
        IsJump = false;

        // モードを変更
        MoveMode = "walk";
    }
}
