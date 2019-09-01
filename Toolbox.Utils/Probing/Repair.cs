// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-07-28 14:35
// ===================================================================================================
// = Description :  A simple collection of operations, that
//                  repair/fix some unwanted states.
// ===================================================================================================

namespace Com.Toolbox.Utils.Probing
{
    /// <summary>
    ///     A simple collection of operations, that
    ///     repair/fix some unwanted states.
    /// </summary>
    public static class Repair
    {
        #region Methods

        /// <summary>
        ///     Removes all trailing occurrences of <paramref name="occurence" />
        ///     from the passed string.
        /// </summary>
        /// <param name="text">
        ///     The text from whom all trailing <paramref name="occurence" />
        ///     shall be removed
        /// </param>
        /// <param name="occurence">
        ///     The trailing string or character that is removed from
        ///     <paramref name="text" />
        /// </param>
        /// <returns>
        ///     A new string with all trailing <paramref name="occurence" />
        ///     removed from <paramref name="text" />
        /// </returns>
        public static string RemoveAllTrailing(string text, string occurence)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(occurence))
                return text;

            return RemoveFromEnd(text, occurence);
        }

        private static string RemoveFromEnd(string text, string removeText)
        {
            while (text.EndsWith(removeText))
                text = text.Substring(0, text.Length - removeText.Length);
            return text;
        }

        #endregion
    }
}