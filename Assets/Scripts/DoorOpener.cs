using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 5.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _maxDistance) == false)
            {
                return;
            }

            if (hit.collider.TryGetComponent<DoorController>(out DoorController door))
            {
                door = hit.collider.GetComponent<DoorController>();
                door.PlayAnimation();
            }
        }
    }
}