using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGunShootLimited : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 40f;

    [Header("Ammo")]
    public int maxAmmo = 30;     // Nombre de balles max
    private int currentAmmo;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        currentAmmo = maxAmmo;

        grabInteractable = GetComponent<XRGrabInteractable>();

        // On écoute l’événement "Activate" = gâchette tir
        grabInteractable.activated.AddListener(OnShoot);
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
            return;
        }

        currentAmmo--;

        // Crée la balle à la position du firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // La fait aller droit devant
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);

        // Détruire la balle après 3 sec
        Destroy(bullet, 3f);

        Debug.Log("Tir ! Balle restante : " + currentAmmo);
    }
}