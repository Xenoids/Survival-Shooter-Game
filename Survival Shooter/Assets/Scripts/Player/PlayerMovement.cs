using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float spd = 6f;
    Vector3 pergerakan;
    Animator animasi;

    Rigidbody rb;

    int floorMask;

    float camerarayLen = 100f;

    private void Awake()
    {
        // dptkan nilai mask dari layer dgn nama Floor
        floorMask = LayerMask.GetMask("Floor");

        // dptkan komponen Animator
        animasi = GetComponent<Animator>();

        // dptkan komponen RigidBody
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // dptkan nilai input horizontal (-1,0,1)
        float hmove = Input.GetAxisRaw("Horizontal");
        float vmove = Input.GetAxisRaw("Vertical");
        Movement(hmove, vmove);
        Turning();
        Animating(hmove, vmove);
    }

    public void Movement(float h, float v)
    {
        pergerakan.Set(h,0f,v);

        // normalisasi nilai vektor agar len nya 1

        pergerakan = pergerakan.normalized * spd * Time.deltaTime;

        // Kasih Movement

        rb.MovePosition(transform.position + pergerakan);
        
    }

    void Turning()
    {
        // Buat Ray dri posisi mouse di layar
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Buat raycast untuk floorHit (untuk kenain ke floor)
        RaycastHit floorHit;

        // Lakuin raycast
        if(Physics.Raycast(cameraRay, out floorHit, camerarayLen, floorMask))
        {
            // dapatkan vektor dari pos player dan pos floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            // Mendapatkan look rotation baru ke hit pos
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Rotasi player
            rb.MoveRotation(newRotation);
        }

    }

    public void Animating(float h, float v)
    {
        bool berjalan = h != 0f || v != 0f;
        animasi.SetBool("IsWalking",berjalan);
    }


}
