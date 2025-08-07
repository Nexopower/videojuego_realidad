using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// This is a special component that allows triggering a select action programmatically
/// for teleportation from the MasterScript when the thumbstick is pushed forward
/// </summary>
public class XRReleaseController : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor m_Interactor;
    private bool m_ShouldSelect = false;

    void Start()
    {
        // Get the interactor component if not assigned
        if (m_Interactor == null)
            m_Interactor = GetComponent<XRBaseInteractor>();
    }

    void Update()
    {
        if (m_ShouldSelect && m_Interactor != null)
        {
            // Try to select the current hover target if available
            var hoverTargets = m_Interactor.interactablesHovered;
            if (hoverTargets.Count > 0)
            {
                var hoverTarget = hoverTargets[0];
                // Cast to IXRSelectInteractable if possible
                if (hoverTarget is IXRSelectInteractable selectTarget && m_Interactor.CanSelect(selectTarget))
                {
                    // Force the selection
                    var interactionManager = m_Interactor.interactionManager;
                    if (interactionManager != null)
                    {
                        interactionManager.SelectEnter(m_Interactor, selectTarget);
                    }
                }
            }
            m_ShouldSelect = false;
        }
    }

    public void Select()
    {
        m_ShouldSelect = true;
    }
}
