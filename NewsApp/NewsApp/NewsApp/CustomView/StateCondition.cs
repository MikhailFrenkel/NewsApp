using Xamarin.Forms;

namespace NewsApp
{
    /// <summary>
    /// Application state enum.
    /// </summary>
    public enum State
    {
        Normal,
        Loading,
        NoInternet
    }

    [ContentProperty("Content")]
    public class StateCondition : View
    {
        /// <summary>
        /// Current state.
        /// </summary>
        public object Is { get; set; }
        /// <summary>
        /// View content.
        /// </summary>
        public View Content { get; set; }

    }
}
