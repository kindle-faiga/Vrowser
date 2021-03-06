﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Leap.Unity
{
    enum HandState{Release, Pinch, Hold};

    public class BlockManager : MonoBehaviour
    {
        [SerializeField]
        private HandState handState = HandState.Release;
        [SerializeField]
        private GameObject rightHand;
        [SerializeField]
        private GameObject leftHand;
        [SerializeField]
        private BlockGetter rightHandState;
        [SerializeField]
        private BlockGetter leftHandState;
        [SerializeField]
        private Transform rightHandPos;
        [SerializeField]
        private Transform leftHandPos;
        [SerializeField]
        private Transform[] pageBlocks;
        [SerializeField]
        private LogSave logSave;
        //private Quaternion rightRotation;
        //private Quaternion leftRotation;
        private PinchDetector pinchDetectorRight;
        private PinchDetector pinchDetectorLeft;
        private BlockState blockState;
        private float distance = 0;
        private float rotation_r = 0;
        private float rotation_l = 0;
        private bool isCube = false;
        private bool isMagnetic = true;
        private bool isChangeScale = true;

        /* updateMode管理用 */
        public enum updateModes {Default,ChangeScale,MagneticForce,AttractBlock,None};
        public int updateMode = (int)updateModes.Default;

        /* AttractBlock制御用 */
        private Vector3 lastRightHandPos, lastLeftHandPos;
        private Quaternion lastRightHandRot, lastLeftHandRot;
        [SerializeField]
        private float rotThreshold = 10.0f;

        void Awake()
        {
            blockState = GetComponentInChildren<BlockState>();
        }

        void Start()
        {
            pinchDetectorRight = rightHand.GetComponent<PinchDetector>();

            pinchDetectorLeft = leftHand.GetComponent<PinchDetector>();

            rotation_r = pinchDetectorRight.transform.rotation.y;

            rotation_l = pinchDetectorLeft.transform.rotation.y;
            /*
            rightRotation = rightHand.transform.rotation;

            leftRotation = leftHand.transform.rotation;
            */
        }

        public void ResetBlocks()
        {
            if(rightHand != null && leftHand != null)
            {
                updateMode = (int)updateModes.Default;

                //rightHandState.GetBlock().transform.parent = null;

                //leftHandState.GetBlock().transform.parent = null;

                foreach (Transform block in pageBlocks)
                {
                    block.parent = transform;
                    block.GetComponent<BlockState>().ResetState();
                }

                //StartCoroutine(WaitForReset());
            }
        }

        public void FullResetBlocks()
        {
            updateMode = (int)updateModes.Default;

            GameObject s1 = GameObject.Find("SmallBlock");

            if(s1 != null)
            {
                s1.transform.parent = transform;
            }

            GameObject s2 = GameObject.Find("SmallBlock1");

            if (s2 != null)
            {
                s2.transform.parent = transform;
            }

            GameObject s3 = GameObject.Find("SmallBlock2");

            if (s3 != null)
            {
                s3.transform.parent = transform;
            }

            GameObject s4 = GameObject.Find("SmallBlock3");

            if (s4 != null)
            {
                s4.transform.parent = transform;
            }

            GameObject s5 = GameObject.Find("SmallBlock4");

            if (s5 != null)
            {
                s5.transform.parent = transform;
            }
        }

        public bool GetMagnetic()
        {
            return isMagnetic;
        }

        public bool GetChangeScale()
        {
            return isChangeScale;
        }

        public void SetMagnetic()
        {
            isMagnetic = !isMagnetic;
        }

        public void SetChangeScale()
        {
            isChangeScale = !isChangeScale;
        }

        public bool GetRightHandPinch()
        {
            return pinchDetectorRight.IsPinching;
        }

        public bool GetLeftHandPinch()
        {
            return pinchDetectorLeft.IsPinching;
        }

        public void ChangeCubeScaling()
        {
            isCube = !isCube;
        }

        public void ChangeTarget(BlockState target)
        {
            blockState = target;
        }

        private void RotateBlock(float rotateRange)
        {
            Quaternion rotation_n = blockState.gameObject.transform.rotation;

            iTween.RotateTo(blockState.gameObject, new Vector3(rotation_n.x, rotation_n.y + rotateRange, rotation_n.z), 1.0f);

            blockState.gameObject.transform.rotation = new Quaternion(rotation_n.x, rotation_n.y + rotateRange, rotation_n.z, rotation_n.w);
        }

        private void ChangeScale(float distance)
        {
            if (isChangeScale)
            {
                if (0.01f < distance)
                {
                    //blockState.ChangeScale(0.05f, isCube);
                    GameObject block = rightHandState.GetBlock();
                    if (block != null)
                    {
                        block.GetComponent<BlockState>().ChangeScale(0.05f, false);
                    }
                }
                else if (distance < -0.01f)
                {
                    GameObject block = rightHandState.GetBlock();
                    if (block != null)
                    {
                        block.GetComponent<BlockState>().ChangeScale(-0.05f, false);
                    }
                }
            }
        }

        private void MagneticForce()
        {
            if (pinchDetectorRight.IsPinching)
            {
                StartCoroutine(WaitForRightHand());
                Debug.Log("RightHandStart");
            }
            else if (pinchDetectorLeft.IsPinching)
            {
                StartCoroutine(WaitForLeftHand());
                Debug.Log("LeftHandStart");
            }
        }

        private void AttractBlock(bool rightState, bool leftState, float rotation_r, float rotation_l)
        {
            if (pinchDetectorRight.IsPinching)
            {
                Debug.Log("RightHandPinching");

                if (!rightState)
                {
                    //blockState.gameObject.transform.parent = rightHandPos;
                    GameObject block = rightHandState.GetBlock();
                    if (block != null)
                    {
                        if (block.transform.parent ==null || block.transform.parent.name == "WebPageManager")
                        {
                            block.transform.parent = rightHandPos;
                        }
                    }
                }
            }
            else if (pinchDetectorLeft.IsPinching)
            {
                Debug.Log("LeftHandPinching");

                if (!leftState)
                {
                    //blockState.gameObject.transform.parent = leftHandPos;
                    GameObject block = leftHandState.GetBlock();
                    if (block != null)
                    {
                        if (block.transform.parent == null || block.transform.parent.name == "WebPageManager")
                        {
                            block.transform.parent = leftHandPos;
                        }
                    }
                }
            }
            /*
            if (!pinchDetectorRight.IsPinching)
            {
                if (rightHandState.GetBlock() != null)
                {
                    rightHandState.GetBlock().transform.parent = null;
                }
            }
            else if (!pinchDetectorLeft.IsPinching)
            {
                Debug.Log("LeftHandPinching");

                if (!leftState)
                {
                    if (leftHandState.GetBlock() != null)
                    {
                        leftHandState.GetBlock().transform.parent = null;
                    }
                }
            }
            */
            /*
            else
            {
                blockState.gameObject.transform.parent = null;
                updateMode = (int)updateModes.Default;
            }
            */
        }

        void Update()
        {
            Vector3 rightPos = pinchDetectorRight.transform.position;
            Vector3 leftPos = pinchDetectorLeft.transform.position;

            GameObject leftState = leftHandState.GetBlock();
            GameObject rightState = rightHandState.GetBlock();

            float newDistance = Vector3.Distance(rightPos, leftPos);
            float newRotation_r = pinchDetectorRight.transform.rotation.y;
            float newRotation_l = pinchDetectorLeft.transform.rotation.y;
            float dis = newDistance - distance;
            float rot_r = newRotation_r - rotation_r;
            float rot_l = newRotation_l - rotation_l;

            /*            if (pinchDetectorRight.IsPinching && pinchDetectorLeft.IsPinching)
                        {
                            ChangeScale(dis);

                            Debug.Log("ChangeScale");
                        }
                        else if(rightState == null && leftState == null)
                        {
                            MagneticForce();
                        }
                        else
                        {
                            AttractBlock(rightState == null,leftState == null, rot_r, rot_l);
                        }*/
            if (Input.GetKeyDown(KeyCode.Z))
            {
                FullResetBlocks();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject h_r = GameObject.Find("RigidRoundHand_R(Clone)");
                GameObject r_r = GameObject.Find("LoPoly_Rigged_Hand_Right(Clone)");
                GameObject h_l = GameObject.Find("RigidRoundHand_L(Clone)");
                GameObject r_l = GameObject.Find("LoPoly_Rigged_Hand_Left(Clone)");

                if(h_r != null)
                {
                    Destroy(h_r);
                }

                if (r_r != null)
                {
                    Destroy(r_r);
                }

                if (h_l != null)
                {
                    Destroy(h_l);
                }

                if (r_l != null)
                {
                    Destroy(r_l);
                }
            }

            if (updateMode == (int)updateModes.Default && (pinchDetectorLeft.DidStartPinch || pinchDetectorRight.DidStartPinch))
            {
                if (rightState == null && leftState == null) //ブロックがどちらの手にも接触してなければMagneticForce modeに
                {
                    updateMode = (int)updateModes.MagneticForce;
                }
                else
                {
                    lastLeftHandPos = leftHandPos.position;
                    lastRightHandPos = rightHandPos.position;
                    lastLeftHandRot = leftHandPos.rotation;
                    lastRightHandRot = rightHandPos.rotation;
                    updateMode = (int)updateModes.AttractBlock; //それ以外はAttractBlock modeに
                }
            }
            else if (updateMode == (int)updateModes.ChangeScale)
            {
                if (rightState == null && leftState == null)
                {
                    //CreateCube(dis);
                }
                else
                {
                    ChangeScale(dis);
                    
                    if (!(pinchDetectorRight.IsPinching && pinchDetectorLeft.IsPinching)) //両手がピンチでなくなったらChangeScale modeを解除(条件を緩めてもいいかも)
                    {
                        updateMode = (int)updateModes.Default;
                    }
                }
            }
            else if (updateMode == (int)updateModes.MagneticForce)
            {
                MagneticForce();
                updateMode = (int)updateModes.Default;
            }
            else if (updateMode == (int)updateModes.AttractBlock)
            {
                AttractBlock(rightState == null, leftState == null, rot_r, rot_l);
            }

            //ヤバかったら一番上に
            if (pinchDetectorRight.IsPinching && pinchDetectorLeft.IsPinching) //両手がピンチ中ならChangeScale modeに
            {
                if (rightHandState.GetBlock() != null && leftHandState.GetBlock() != null)
                {
                    if (rightHandState.GetBlock() == leftHandState.GetBlock())
                    {
                        logSave.logSave("HandState : ChangeScale");
                        updateMode = (int)updateModes.ChangeScale;
                    }
                }
            }
            else
            {
                if (!pinchDetectorRight.IsPinching && !pinchDetectorLeft.IsPinching)
                {
                    updateMode = (int)updateModes.Default;
                }

                if (!pinchDetectorRight.IsPinching)
                {
                    if (rightHandState.GetBlock() != null)
                    {
                        rightHandState.GetBlock().transform.parent = null;
                    }
                }
                if (!pinchDetectorLeft.IsPinching)
                {
                    if (leftHandState.GetBlock() != null)
                    {
                        leftHandState.GetBlock().transform.parent = null;
                    }
                }
            }

            distance = newDistance;

            rotation_r = newRotation_r;

            rotation_l = newRotation_l;

            /*
            rightRotation = rightHand.transform.rotation;

            leftRotation = leftHand.transform.rotation;
            */


        }

        IEnumerator WaitForRightHand()
        {
            yield return new WaitForSeconds(0.5f);

            if (isMagnetic && pinchDetectorRight.IsPinching && !pinchDetectorLeft.IsPinching)
            {
                logSave.logSave("HandState : MagneticForceRight");
                iTween.MoveTo(blockState.gameObject, rightHand.transform.position, 2.5f);
            }
        }

        IEnumerator WaitForLeftHand()
        {
            yield return new WaitForSeconds(0.5f);

            if(isMagnetic && !pinchDetectorRight.IsPinching && pinchDetectorLeft.IsPinching)
            {
                logSave.logSave("HandState : MagneticForceLeft");
                iTween.MoveTo(blockState.gameObject, leftHand.transform.position, 2.5f);
            }
        }

        IEnumerator WaitForReset()
        {
            yield return new WaitForSeconds(1.0f);

            updateMode = (int)updateModes.Default;
        }
    }
}