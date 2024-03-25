using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class ThirdPersonShooterController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirtualCamera;
    public float Sensitivity;
    public float AimSensitivity;
    public LayerMask aimColliderLayerMask = new LayerMask();
    public Transform pfBulletProjectile;
    public Transform spawnBulletPosition;
    public Transform debugTransform;
    public RenderTexture renderTexture;
    public GameObject Pistol;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    public AudioClip shootSFX;
    private AudioSource audioSource;
    public bool _hasKey = false;
    public bool _secretDone = false;
    public int _secretCount = 0;
    public float useDistance = 8;
    public LayerMask useLayerMask;
    public GameObject Flashlight;
    public AudioClip FlashlightSFX;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        audioSource = Pistol.GetComponent<AudioSource>();
    }
    private void Update()
    {
        _secretDone = _secretCount == GameObject.FindGameObjectsWithTag("Secret").Length;
        if (_secretDone)
        {
            GetComponent<PhraseScript>().PlaySecret();
        }
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(renderTexture.width / 2f, renderTexture.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            debugTransform.position = raycastHit.point;
        }
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            Pistol.GetComponent<MeshRenderer>().enabled = true;
            thirdPersonController.SetSensitivity(AimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            Pistol.GetComponent<MeshRenderer>().enabled = false;
            thirdPersonController.SetSensitivity(Sensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
        if (starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            audioSource.PlayOneShot(shootSFX);
            starterAssetsInputs.shoot = false;
        }
        if (starterAssetsInputs.interact)
        {
            starterAssetsInputs.interact = false;
            RaycastHit usehit;
            if (Physics.Raycast(ray, out usehit, useDistance, useLayerMask))
            {
                if (usehit.transform.tag == "Key")
                {
                    Destroy(usehit.collider.gameObject);
                    _hasKey = true;
                }
                if (usehit.transform.tag == "Secret")
                {
                    Destroy(usehit.collider.gameObject);
                    _secretCount += 1;
                }
                if (usehit.transform.tag == "End" && !_hasKey)
                {
                    GetComponent<PhraseScript>().PlayNoKey();
                }
                if (usehit.transform.tag == "End" && _hasKey && _secretDone)
                {
                    SceneManager.LoadScene("EndingSecret");
                }
                if (usehit.transform.tag == "End" && _hasKey && !_secretDone)
                {
                    SceneManager.LoadScene("EndingMain");
                }
            }
        }
    }
    public void ToggleFlashlight()
    {
        if (starterAssetsInputs.flashlight)
        {
            Flashlight.SetActive(true);
            audioSource.PlayOneShot(FlashlightSFX);
        }
        else
        {
            Flashlight.SetActive(false);
            audioSource.PlayOneShot(FlashlightSFX);
        }
    }
}
