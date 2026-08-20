using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    #region PlayerInput
    private PlayerInput input;
    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
    #endregion

    public GameObject shot;
    public Transform shotSpawn;
    public float nextFire;
    public float fireRate = 2.0f;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void Update()
    {
        if (input.Player.Fire.IsPressed() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

}
