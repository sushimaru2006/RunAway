using System;
using System.Numerics;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    [Serializable]
    public class CurveControlledBob
    {
        private float HorizontalBobRange = 0.05f;
        private float VerticalBobRange = 0.05f;
        public AnimationCurve Bobcurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                            new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                            new Keyframe(2f, 0f)); // sin curve for head bob
        public float VerticaltoHorizontalRatio = 1f;

        public float IdolHorizontalBobRange;
        public float IdolVerticalBobRange;

        public float WalkHorizontalBobRange;
        public float WalkVerticalBobRange;

        public float DashHorizontalBobRange;
        public float DashVerticalBobRange;

        private float ChangeBob = 0.005f;

        private float m_CyclePositionX;
        private float m_CyclePositionY;
        private float m_BobBaseInterval;
        private UnityEngine.Vector3 m_OriginalCameraPosition;
        private float m_Time;


        public void Setup(Camera camera, float bobBaseInterval)
        {
            m_BobBaseInterval = bobBaseInterval;
            m_OriginalCameraPosition = camera.transform.localPosition;

            // get the length of the curve in time
            m_Time = Bobcurve[Bobcurve.length - 1].time;
        }


        public UnityEngine.Vector3 DoHeadBob(float speed, string mode)
        {
            switch (mode)
            {
                case "idol":
                    if (HorizontalBobRange == IdolHorizontalBobRange && VerticalBobRange == IdolVerticalBobRange)
                    {
                        HorizontalBobRange += HorizontalBobRange < IdolHorizontalBobRange ? ChangeBob : -ChangeBob;
                        VerticalBobRange += VerticalBobRange < IdolVerticalBobRange ? ChangeBob : -ChangeBob;
                    }
                    break;
                case "walk":
                    if (HorizontalBobRange == WalkHorizontalBobRange && VerticalBobRange == WalkVerticalBobRange)
                    {
                        HorizontalBobRange += HorizontalBobRange < WalkHorizontalBobRange ? ChangeBob : -ChangeBob;
                        VerticalBobRange += VerticalBobRange < WalkVerticalBobRange ? ChangeBob : -ChangeBob;
                    }
                    break;
                case "dash":
                    if (HorizontalBobRange == DashHorizontalBobRange && VerticalBobRange == DashVerticalBobRange)
                    {
                        HorizontalBobRange += HorizontalBobRange < DashHorizontalBobRange ? ChangeBob : -ChangeBob;
                        VerticalBobRange += VerticalBobRange < DashVerticalBobRange ? ChangeBob : -ChangeBob;
                    }
                    break;
                case "jump":
                    if (HorizontalBobRange == 0f && VerticalBobRange == 0f)
                    {
                        HorizontalBobRange -= ChangeBob;
                        VerticalBobRange -= ChangeBob;
                    }
                    break;
            }

            float xPos = m_OriginalCameraPosition.x + (Bobcurve.Evaluate(m_CyclePositionX) * HorizontalBobRange);
            float yPos = m_OriginalCameraPosition.y + (Bobcurve.Evaluate(m_CyclePositionY) * VerticalBobRange);

            m_CyclePositionX += (speed * Time.deltaTime) / m_BobBaseInterval;
            m_CyclePositionY += ((speed * Time.deltaTime) / m_BobBaseInterval) * VerticaltoHorizontalRatio;

            if (m_CyclePositionX > m_Time)
            {
                m_CyclePositionX = m_CyclePositionX - m_Time;
            }
            if (m_CyclePositionY > m_Time)
            {
                m_CyclePositionY = m_CyclePositionY - m_Time;
            }

            return new UnityEngine.Vector3(xPos, yPos, 0f);
        }
    }
}