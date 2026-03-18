using System.Windows.Input;
using Gym_Logs.Enums;

namespace Gym_Logs.Model.System
{
    /// <summary>
    /// Represents a single option displayed in the calendar bottom sheet.
    /// Defines the visual appearance and the action to execute when selected.
    /// </summary>
    public class BottomSheetOptionModel
    {
        /// <summary>
        /// The logical action associated with this option.
        /// Used to determine what happens when the option is selected.
        /// </summary>
        public CalendarActionEnum Action { get; set; }

        /// <summary>
        /// The display text shown to the user.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// A short icon representation (e.g., emoji or glyph) displayed on the right side.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// The path to the image displayed alongside the text.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Determines whether the option is visible in the bottom sheet.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// The command executed when the user taps this option.
        /// </summary>
        public ICommand Command { get; set; }
    }
}
