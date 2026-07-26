using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Equipment Slots")]
    [SerializeField] private Weapon weapon;

    public Weapon GetWeapon()
    {
        return weapon;
    }
}
