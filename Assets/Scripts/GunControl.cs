using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GunControl : MonoBehaviour
{
    [Header("Gun Settings")]
    public float FireRate = 0.5f;
    public float SpherecastRadius = 0.5f;

    [Header("Crosshair Settings")]
    public Texture2D CrosshairTexture;

    private float nextFireTime;
    private Camera mainCamera;
    private InputAction shootAction;

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
            Cursor.SetCursor(CrosshairTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnEnable()
    {
        shootAction?.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        shootAction?.Disable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
    }

    void Shoot(InputAction.CallbackContext context)
    {
        if (mainCamera == null) return;
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + FireRate;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out IShootable shootable))
                shootable.OnHit();
        }
    }

    void DrawDebugSphere(Vector3 center, float radius, Color color, float duration)
    {
        int segments = 16;
        float step = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float a0 = Mathf.Deg2Rad * (i * step);
            float a1 = Mathf.Deg2Rad * ((i + 1) * step);

            Debug.DrawLine(
                center + new Vector3(Mathf.Cos(a0), Mathf.Sin(a0), 0) * radius,
                center + new Vector3(Mathf.Cos(a1), Mathf.Sin(a1), 0) * radius,
                color, duration);

            Debug.DrawLine(
                center + new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0)) * radius,
                center + new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1)) * radius,
                color, duration);

            Debug.DrawLine(
                center + new Vector3(0, Mathf.Cos(a0), Mathf.Sin(a0)) * radius,
                center + new Vector3(0, Mathf.Cos(a1), Mathf.Sin(a1)) * radius,
                color, duration);
        }
    }
}