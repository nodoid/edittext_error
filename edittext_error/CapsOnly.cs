using System;
using Android.Text;
using Java.Lang;

namespace EditTextFilter
{
    public class CapsOnly : Java.Lang.Object, IInputFilter
    {
        int lastCount { get; set; } = 0;
        int lastLen { get; set; } = 0;
        public bool EnableAlpha { get; set; }
        public bool EnablePunctuation { get; set; }
        public bool EnableNumeric { get; set; }
        public bool EnableSeparators { get; set; }
        public bool EnableUpperCaseOnly { get; set; }

        public CapsOnly(bool enableAlpha, bool enablePunctuation, bool enableNumeric, bool enableUpperCaseOnly = false, bool enableSeparators = true)
        {
            EnableAlpha = enableAlpha;
            EnablePunctuation = enablePunctuation;
            EnableNumeric = enableNumeric;
            EnableSeparators = enableSeparators;
            EnableUpperCaseOnly = enableUpperCaseOnly;
        }
        public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
        {
            if (source is SpannableStringBuilder)
            {
                var sourceAsSpannableBuilder = (SpannableStringBuilder)source;
                for (var i = end - 1; i >= start; i--)
                {
                    if (!isCharacterOk(source.CharAt(i)))
                    {
                        sourceAsSpannableBuilder.Delete(i, i + 1);
                    }
                }
                return sourceAsSpannableBuilder;
            }
            else
            {
                var filteredStringBuilder = new SpannableStringBuilder();
                for (int i = start; i < end; i++)
                {
                    var currentChar = source.CharAt(i);
                    if (isCharacterOk(currentChar))
                    {
                        filteredStringBuilder.Append(currentChar);
                    }
                }
                return filteredStringBuilder;
            }
        }

        bool isCharacterOk(char character)
        {
            var isSeperator = Char.IsSeparator(character);
            var isLetter = Char.IsLetter(character);
            var isPunctuation = Char.IsPunctuation(character);
            var isDigit = Char.IsDigit(character);
            var isUpper = Char.IsUpper(character);
            var isLower = Char.IsLower(character);

            if (!isSeperator && !isLetter && !isPunctuation && !isDigit && !isUpper)
                return false;

            if (isSeperator && !EnableSeparators)
                return false;

            if (isLetter && !EnableAlpha)
                return false;

            if (isLetter && isLower && EnableUpperCaseOnly)
                return false;

            if (isPunctuation && !EnablePunctuation)
                return false;

            if (isDigit && !EnableNumeric)
                return false;

            return true;
        }
    }
}
