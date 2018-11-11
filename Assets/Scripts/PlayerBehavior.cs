using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    [SerializeField]
    private TouchAxisCtrl m_TouchControl;
    [SerializeField]
    private LineRenderer m_Line;
    [SerializeField]
    private ParticleSystem m_Particles;
    [SerializeField]
    private Renderer m_Renderer;
    public float velocityScale = 1f;
    private Rigidbody2D m_RigidBody;
    private Vector2 m_Force;

    // Use this for initialization
    void Start ()
    {
        // Get connected rigidbody
        m_RigidBody = GetComponent<Rigidbody2D>();
        // Subscribe to untouch
        m_TouchControl.OnUntouch += OnUntouch;
    }

    void Update()
    {
        if (m_TouchControl.IsTouching())
        {
            // Get the joystick value
            Vector3 val = Wrj.Utils.ToVector3(m_TouchControl.GetAxis());
            // The line/arrow position is inverted to point the direction the ball will be shot. It's doubled for improved visibility.
            Vector3 pos2 = transform.position + val * -2f;

            // Invert the force and save it globally so it's up-to-date upon untouch. Will be used as the basis of desired velocity.
            m_Force = -val;

            // Draw the indicator arrow
            m_Line.positionCount = 2;
            m_Line.SetPosition(0, transform.position);
            m_Line.SetPosition(1, pos2);
        }
    }
    void OnCollisionEnter2D (Collision2D col)
    {
        // If what we've hit is "deadly," stop physics, hide, and play the explosion particle system.
		if (col.gameObject.GetComponent<Deadly>())
        {
            m_RigidBody.simulated = false;
            m_Renderer.enabled = false;
            m_Particles.Play();
        }
	}

    // When the player un-touches, hide the arrow/line and set a velocity for new movement
    void OnUntouch()
    {
        m_Line.positionCount = 2;
        m_Line.SetPosition(0, transform.position);
        m_Line.SetPosition(1, transform.position);

        // If the user provided a sufficient vector to move, set the velocity.
        if (m_Force.magnitude > .15f)
            m_RigidBody.velocity = (m_Force * velocityScale);
    }
}
