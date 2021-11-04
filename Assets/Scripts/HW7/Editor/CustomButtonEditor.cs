using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        var typeProperty = new PropertyField(serializedObject.FindProperty(CustomButton.TypeFieldName));
        var durationProperty = new PropertyField(serializedObject.FindProperty(CustomButton.DurationFieldName));
        var powerProperty = new PropertyField(serializedObject.FindProperty(CustomButton.PowerFieldName));
        var easingProperty = new PropertyField(serializedObject.FindProperty(CustomButton.EasingFieldName));

        root.Add(typeProperty);
        root.Add(durationProperty);
        root.Add(powerProperty);
        root.Add(easingProperty);
        root.Add(new IMGUIContainer(OnInspectorGUI));

        return root;
    }
}
