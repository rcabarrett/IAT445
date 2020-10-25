using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class WorldInteractions : MonoBehaviour
{
    // set what colour the button will change to when looked at
    [SerializeField] private Material highlightMaterial;
    // set colour to revert to
    [SerializeField] private Material defaultMaterial;
    // set tag layer to isolate button from other in-world objects
    [SerializeField] private string SelectableTag = "Finish";
    // Identify what object this button interacts with (TeleporterObject.cover)
    [SerializeField] private GameObject ButtonControls;
    // Variable that will hold text for UI
    public TextMeshProUGUI InstructionText;

    private Transform _selection;
    // holder for button animator
    private Animator animator;
    // var that holds current state of teleporter cover
    bool CurrentState = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // This resets the affected in game objects back to default settings, 
        // and prevents this code from acting on non-targets.
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            // replace the default material
            selectionRenderer.material = defaultMaterial;
            // reset _selection back to null to prevent other objects from being effected
            _selection = null;
            // Reset UI text back to blank
            InstructionText.text = "";

        }

        RaycastHit hit;

        // Check every frame via raycast to see if we're looking at anything.
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Always fun to know what's being looked at. This will be a core part of my final project
            // Debug.Log(hit.collider.gameObject.name);


            var selection = hit.transform;

            // Check if what's being looked at matches the button layer
            if (selection.CompareTag(SelectableTag))
            {
                // Go to the button's renderer module
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    // apply the highlight by swapping the materials
                    selectionRenderer.material = highlightMaterial;
                    // Display the instructional text on the UI
                    InstructionText.text = "Press 'E' to open and close the Teleporter";

                    // If the player interacts according to the onscreen instructions.
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        animator = ButtonControls.GetComponent<Animator>();
                        // Check current state of cover
                        // If the cover is currently on. Trigger the RemoveCover animation and set state of cover to true
                        if (CurrentState == false)
                        {
                            // Debug.Log("setting to true");
                            animator.SetBool("ButtonPress", true);
                            CurrentState = true;
                        }

                        // ElseIf the cover is currently off. Trigger the ReplaceCover animation and set state of cover to false
                        else
                        {
                            if (CurrentState == true)
                            {
                                // Debug.Log("setting to false");
                                animator.SetBool("ButtonPress", false);
                                CurrentState = false;
                            }
                        }

                        
                    }

                    // initiate the reset script by making _selection any non-null value.
                    _selection = selection;
                }
            }
        }

    }
}