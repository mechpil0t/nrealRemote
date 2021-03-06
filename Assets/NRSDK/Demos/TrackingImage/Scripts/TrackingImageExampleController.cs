/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*
* This file is part of NRSDK.
*
* https://www.nreal.ai/
*
*****************************************************************************/

namespace NRKernal.NRExamples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary> Controller for TrackingImage example. </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/image-tracking")]
    public class TrackingImageExampleController : MonoBehaviour
    {

        /// <summary> A prefab for visualizing an TrackingImage. </summary>
        public TrackingImageVisualizer TrackingImageVisualizerPrefab;

        /// <summary> The overlay containing the fit to scan user guide. </summary>
        public GameObject FitToScanOverlay;

        /// <summary> The visualizers. </summary>
        private Dictionary<int, TrackingImageVisualizer> m_Visualizers
            = new Dictionary<int, TrackingImageVisualizer>();

        /// <summary> The temporary tracking images. </summary>
        private List<NRTrackableImage> m_TempTrackingImages = new List<NRTrackableImage>();

        GameObject _target;

        public GameObject _xyz;
        public GameObject _remoteControl;
        xSmooth _so;

        public bool _localizeOnce;

        void Start()
        {
          _so = _xyz.GetComponent<xSmooth>();
          StartCoroutine(delayStart());
        }

        IEnumerator delayStart()
        {
          yield return new WaitForSeconds(3f);
          _remoteControl.SetActive(true);
        }

        /// <summary> Updates this object. </summary>
        public void Update()
        {
#if !UNITY_EDITOR
            // Check that motion tracking is tracking.
            if (NRFrame.SessionStatus != SessionState.Running)
            {
                return;
            }
#endif
            // Get updated augmented images for this frame.
            NRFrame.GetTrackables<NRTrackableImage>(m_TempTrackingImages, NRTrackableQueryFilter.New);

            // Create visualizers and anchors for updated augmented images that are tracking and do not previously
            // have a visualizer. Remove visualizers for stopped images.
            foreach (var image in m_TempTrackingImages)
            {
                TrackingImageVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.GetDataBaseIndex(), out visualizer);
                if (image.GetTrackingState() == TrackingState.Tracking && visualizer == null)
                {
                    NRDebugger.Info("Create new TrackingImageVisualizer!");
                    // Create an anchor to ensure that NRSDK keeps tracking this augmented image.
                    if(_target == null)
                    {
                      visualizer = (TrackingImageVisualizer)Instantiate(TrackingImageVisualizerPrefab, image.GetCenterPose().position, image.GetCenterPose().rotation);
                      visualizer.name = image.GetDataBaseIndex().ToString();
                      visualizer.Image = image;
                      visualizer.transform.parent = transform;
                      m_Visualizers.Add(image.GetDataBaseIndex(), visualizer);
                      _target = visualizer.gameObject;
                      _xyz.SetActive(true);
                      _so.target = _target.transform;
                      if(_localizeOnce == true)
                      {
                        DisableImageTracking();
                      }
                    }
                }
                else if (image.GetTrackingState() == TrackingState.Stopped && visualizer != null)
                {
                    m_Visualizers.Remove(image.GetDataBaseIndex());
                    Destroy(visualizer.gameObject);
                }

                FitToScanOverlay.SetActive(false);
            }

        }

        /// <summary> Enables the image tracking. </summary>
        public void EnableImageTracking()
        {
            var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            config.ImageTrackingMode = TrackableImageFindingMode.ENABLE;
            NRSessionManager.Instance.SetConfiguration(config);
        }

        /// <summary> Disables the image tracking. </summary>
        public void DisableImageTracking()
        {
            var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            config.ImageTrackingMode = TrackableImageFindingMode.DISABLE;
            NRSessionManager.Instance.SetConfiguration(config);
        }
    }
}
