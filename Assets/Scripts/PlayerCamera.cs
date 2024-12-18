using UnityEngine;
using Photon.Pun;

public class FPSCameraSetup : MonoBehaviour
{
    public Camera playerCamera; // FPSカメラ

    void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();

        // 自分のプレイヤーオブジェクトでない場合、カメラを無効化
        if (!photonView.IsMine)
        {
            if (playerCamera != null)
            {
                playerCamera.gameObject.SetActive(false);
            }
        }
        else
        {
            // ローカルプレイヤーのカメラをMainCameraに設定
            if (playerCamera != null)
            {
                Camera.main.enabled = false; // Main Cameraを無効化
                playerCamera.tag = "MainCamera"; // FPSカメラをMainCameraとして設定
                playerCamera.enabled = true;
            }
        }
    }
}