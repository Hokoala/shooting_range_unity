using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGunShootLimited : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 40f;

    [Header("Ammo")]
    public int maxAmmo = 30;
    private int currentAmmo;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip emptySound; // son quand plus de balles

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        currentAmmo = maxAmmo;

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(OnShoot);

        // Si pas d'AudioSource assigné, on en ajoute un automatiquement
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnDestroy()
    {
        grabInteractable.activated.RemoveListener(OnShoot);
    }

    private void OnShoot(ActivateEventArgs arg)
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("PLUS DE BALLES !");

            if (emptySound != null)
                audioSource.PlayOneShot(emptySound);

            return;
        }

        currentAmmo--;

        // Son du tir
        if (shootSound != null)
            audioSource.PlayOneShot(shootSound);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);

        Destroy(bullet, 3f);

        Debug.Log("Tir ! Balle restante : " + currentAmmo);
    }
}