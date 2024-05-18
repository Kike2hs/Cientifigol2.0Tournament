using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootScript : MonoBehaviour
{
    public int kickSpeed;
    public bool isJugadorA;
    public GameObject balon;

    private HingeJoint2D hingeJoint2D;
    private bool kick = false;
    private JointMotor2D positiveMotor, negativeMotor;

    void Start() {
        Physics2D.IgnoreCollision(GameObject.Find("PISO").GetComponent<Collider2D>(), GetComponent<Collider2D>());

        this.hingeJoint2D = this.GetComponent<HingeJoint2D>();

        // Cache
        positiveMotor = new JointMotor2D { motorSpeed = kickSpeed, maxMotorTorque = 10000 };
        negativeMotor = new JointMotor2D { motorSpeed = -kickSpeed, maxMotorTorque = 10000 };
    }

    void Update() {
        if (isJugadorA) {
            hingeJoint2D.motor = Input.GetKey(KeyCode.Space) || kick ? positiveMotor : negativeMotor;
        } else {
            var distanceBalon = Vector3.Distance(this.transform.position, balon.transform.position);
            var isBalonPositive = balon.transform.position.x - this.transform.position.x;

            if (distanceBalon < 9 && isBalonPositive > 0) {
                if (this.transform.rotation.z > 0.6) {
                    hingeJoint2D.motor = positiveMotor;
                } else if (this.transform.rotation.z < -0.2) {
                    hingeJoint2D.motor = negativeMotor;
                }
            } else {
                hingeJoint2D.motor = positiveMotor;
            }
        }
    }

    public void kickBall() {
        kick = true;
    }

    public void releaseFoot() {
        kick = false;
    }
}
