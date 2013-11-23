using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    private WeaponScript weapon;

    void Awake() {
        // Retrieve the weapon only once
        weapon = GetComponentInChildren<WeaponScript>();
    }

    void Update() {
        // Auto-fire
        if (weapon != null && weapon.CanAttack) {
            weapon.Attack(true);
        }
    }
}
