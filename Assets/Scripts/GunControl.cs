using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GunControl : MonoBehaviour
{


    [Header("Gun Settings")]
    public float FireRate = 0.5f;
    public float SpherecastRadius = 0.5f; // Increase for more lenient hit detection

    [Header("Crosshair Settings")]
    public float CrosshairSize = 1f;
    public Texture2D CrosshairTexture;
    private float nextFireTime;
    private Camera mainCamera;

    private InputAction shootAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        var actionMap = InputSystem.actions.FindActionMap("Player");

        mainCamera = Camera.main;
        shootAction = actionMap.FindAction("Attack");
        shootAction.performed += Shoot;

    }
    void Start()
    {
        if (CrosshairTexture != null)
        {
            Cursor.SetCursor(CrosshairTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnEnable()
    {
        shootAction.Enable();

    }

    void OnDisable()
    {
        shootAction.Disable();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shoot(InputAction.CallbackContext context)
    {

        if (Time.time < nextFireTime)
        {
            return; // Not enough time has passed since the last shot
        }

        nextFireTime = Time.time + FireRate;

        // Look action is a Vector2 screen position
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);



        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.1f);
        if (Physics.SphereCast(ray, SpherecastRadius, out RaycastHit hit, Mathf.Infinity))
        {
            // Draw the sphere at the point of contact
            DrawDebugSphere(hit.point + ray.direction * SpherecastRadius, SpherecastRadius, Color.green, 0.1f);

            if (hit.collider.TryGetComponent(out IShootable shootable))
                shootable.OnHit();
        }
    }

    // Draws three circle outlines (XY, XZ, YZ planes) to approximate a wire sphere.
    // Only visible in the Scene view during Play Mode — make sure Gizmos are enabled.
    void DrawDebugSphere(Vector3 center, float radius, Color color, float duration)
    {
        int segments = 16;
        float step = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float a0 = Mathf.Deg2Rad * (i * step);
            float a1 = Mathf.Deg2Rad * ((i + 1) * step);

            // XY plane
            Debug.DrawLine(
                center + new Vector3(Mathf.Cos(a0), Mathf.Sin(a0), 0) * radius,
                center + new Vector3(Mathf.Cos(a1), Mathf.Sin(a1), 0) * radius,
                color, duration);

            // XZ plane
            Debug.DrawLine(
                center + new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0)) * radius,
                center + new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1)) * radius,
                color, duration);

            // YZ plane
            Debug.DrawLine(
                center + new Vector3(0, Mathf.Cos(a0), Mathf.Sin(a0)) * radius,
                center + new Vector3(0, Mathf.Cos(a1), Mathf.Sin(a1)) * radius,
                color, duration);
        }
    }
}
