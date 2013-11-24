using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    private WeaponScript weapon;

    void Awake() {
        // Retrieve the weapon only once
        weapon = GetComponentInChildren<WeaponScript>();
    }

    void Update() {

        // 2 - Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // 3 - Movement per direction
        Vector3 movement = new Vector3(
          speed.x * inputX,
          speed.y * inputY,
          0);

        // 4 - Relative to the time
        movement *= Time.deltaTime;

        // 5 - Move the game object
        transform.Translate(movement);

        // 5 - Shooting
        bool shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");

        if (shoot) {
            if (weapon != null) {
                // false because the player is not an enemy
                weapon.Attack(false);
            }
        }

        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );
    }

    void OnDestroy() {
        // Game Over.
        // Add the script to the parent because the current game
        // object is likely going to be destroyed immediately.
        //transform.parent.gameObject.AddComponent<GameOverScript>();
    }
}
