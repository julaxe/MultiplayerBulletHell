using _Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.SO.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/DrawArrow")]
    public class DrawArrowAction : Action
    {
        public GameObject linePrefab;
        
        public override void OnEnter(StateController controller)
        {
            Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(
                Touchscreen.current.primaryTouch.position.ReadValue());
            
            var line = Instantiate(linePrefab, positionOnScreen, Quaternion.identity);
            
            controller.arrowLine = line.GetComponent<Line>();
        }

        public override void Act(StateController controller)
        {
            DrawArrow(controller);
        }

        private void DrawArrow(StateController controller)
        {
            if (Touchscreen.current.touches.Count == 0) return;
            
            Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(
                Touchscreen.current.primaryTouch.position.ReadValue());
            
            controller.arrowLine.UpdateLine(positionOnScreen);
            
        }
    }
}