#pragma warning disable 0168
#pragma warning disable 0414
using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class xPlayerMovement : MonoBehaviour
{

     public float fSpeed = 6f;

     Vector3 vMovement;
     Animator aAnim;
     Rigidbody playerRigidbody;
     int nFloorMask;
     float fCamRayLength = 100f;

     void Awake()
     {
          nFloorMask = LayerMask.GetMask("Floor");
          aAnim = GetComponent<Animator>();
          playerRigidbody = GetComponent<Rigidbody>();
     }

     void FixedUpdate()
     {
          #region DEBUG tests
          if (Input.GetKey(KeyCode.DownArrow))
          {
               //various debug tests

               /*var something = gameObject.GetComponent<xGameObject>();
               something.fProperties[xEngine.PROPERTY_ATTRIBUTE_DEXTERITY] = 11f;
               PropertyInfo p_info_keys = something.fProperties.GetType().GetProperty("Keys");
               IEnumerable<int> keys = (IEnumerable<int>)p_info_keys.GetValue(something.fProperties, null);
               Debug.Log("something");*/

               /*GameObject e = GameObject.Find("oEnemy");
               GameObject p = GameObject.Find("oPlayer");
               Debug.Log(xEngine.CheckLineOfSightObject(e, p));
               if (xEngine.CheckLineOfSightObject(e,p) == xEngine.TRUE)
               {
                    xEvent _event = xEngine.Event(xEngine.EVENT_TYPE_AOE_HEARTBEAT);
                    _event.oCreator = gameObject;
                    xEngine.SignalEvent(gameObject, _event);
                    //xEventManager.Instance.QueueEvent(_event);
               }*/
               //xEngine.DEBUG_ConsoleCommand("runscript xAddTalent enemy 1000");
               
               //oBase.AddToQueue(new xCommand(EngineConstants.COMMAND_TYPE_MOVE_TO_LOCATION));
               //gameObject.GetComponent<xGameObject>().AddToQueue(new xCommand(xEngine.COMMAND_TYPE_WAIT));
          }
          #endregion

          float h = Input.GetAxisRaw("Horizontal");
          float v = Input.GetAxisRaw("Vertical");

          Move(h, v);
     }

     void Move(float h, float v)
     {
          vMovement.Set(h, 0f, v);

          vMovement = vMovement.normalized * fSpeed * Time.deltaTime;

          playerRigidbody.MovePosition(transform.position + vMovement);
     }
}
