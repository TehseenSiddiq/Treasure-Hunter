using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBehviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.1f;
    public float recoilForce = 0.1f;
    public AudioClip gunShot;
    public AudioClip takeDamage;
    Vector3 mouseWorldPosition;
    public ParticleSystem muzzleFlashPrefab;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] new Camera camera;
    public int magazineSize = 10;
    public float reloadTime = 1f;
    public AudioClip reloadSound;
    public AudioClip noAmmo;
    public TextMeshProUGUI ammoText;

    public int maxAmmo;
    private float nextFireTime = 0f;   
    private int currentAmmo;
    private bool isReloading = false;

    private StarterAssetsInputs _input;

    public int lifes = 3;
    public Image[] hearts;
    public float health = 10f;
    public Image damageScreen;
    public Sprite[] damages;
    public Items[] items;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
      //  cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        UpdateAmmoText();
        damageScreen.color = new Color(damageScreen.color.r, damageScreen.color.g, damageScreen.color.b, 0f);
    }
    void Update()
    {
        mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
        if (isReloading)
        {
            return;
        }

       /* if (_input.Reload && currentAmmo < magazineSize)
        {
            StartCoroutine(Reload());
            return;
        }*/

        if (_input.shoot && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                currentAmmo--;
                UpdateAmmoText();
                
            }
            else
            {
                StartCoroutine(Reload());
            }
            _input.shoot = false;
         
        }
      
    }

    IEnumerator Reload()
    {
        if (isReloading || currentAmmo == magazineSize || maxAmmo <=0)
        {
            AudioSource.PlayClipAtPoint(noAmmo, transform.position);
            yield break;
        }

        isReloading = true;
        AudioSource.PlayClipAtPoint(reloadSound, transform.position);
        yield return new WaitForSeconds(reloadTime);
        if (maxAmmo > magazineSize)
        {
            currentAmmo = magazineSize;
            maxAmmo -= magazineSize;
        }
        else
        {
            currentAmmo = maxAmmo;
            maxAmmo = 0;
        }
            

        UpdateAmmoText();
        isReloading = false;
    }
    void UpdateAmmoText()
    {
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }
    void Shoot()
    {
        // Check if enough time has passed since last shot
        if (Time.time < nextFireTime)
        {
            return;
        }
        nextFireTime = Time.time + fireRate;

        //Find Direction From Gun To Mouse
        Vector3 aimDir = (mouseWorldPosition - firePoint.position).normalized;
          
        //  _input.shoot = false;

        // Instantiate bullet prefab
        Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(aimDir, Vector3.up));

        // Play shooting sound
        AudioSource.PlayClipAtPoint(gunShot, transform.position);

        //Shake Camera
        CinemachineShake.Instance.ShakeCamera(2, 0.1f);

        // Instantiate muzzle flash prefab
        muzzleFlashPrefab.Play();
     //  Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);

    }
    public LayerMask whatIsLava;
    private void LateUpdate()
    {
        damageScreen.color = new Color(damageScreen.color.r, damageScreen.color.g, damageScreen.color.b, health >= 9 ? 0f : 1f);
        damageScreen.sprite = health >= 15 ? null : health > 9 ? damages[0] : damages[1];
        if(health < 0)
        {

         //   transform.position = GameManager.instance.spawnPoint.position;
            GameManager.instance.ResetPlayer();
            health = 20;
            maxAmmo = 20;
            lifes--;
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].gameObject.SetActive(i < lifes);
            }
            if (lifes <= 0)
            {
                Debug.Log("GameOver");
                GameManager.instance.GameLost();
            }
           
        }
    
       
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.CompareTag("Lava"))
        {
            Debug.Log("Player touched lava!");
            health = 0;
            this.Wait(0.5f, () => health -= 1);
        }
    }
    public void TakeDamage(float damage)
    {
        AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        health -= damage;
       
    }


}
