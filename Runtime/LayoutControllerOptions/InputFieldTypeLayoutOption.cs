using System;
using TMPro;

namespace Layout.LayoutControllerOptions
{
    [Serializable]
    public class InputFieldTypeLayoutOption : BaseLayoutControllerOption
    {
        public TMP_InputField inputField;

        public float max = 100f;

        public override bool OverrideMaxX => IsInputFieldForNumber;
        public override float GetMaxX => max;
        
        private bool IsInputFieldForNumber =>
            inputField.contentType == TMP_InputField.ContentType.DecimalNumber ||
            inputField.contentType == TMP_InputField.ContentType.IntegerNumber ||
            inputField.characterValidation == TMP_InputField.CharacterValidation.Digit ||
            inputField.characterValidation == TMP_InputField.CharacterValidation.Integer ||
            inputField.characterValidation == TMP_InputField.CharacterValidation.Decimal;
    }
}