using System.Text.RegularExpressions;

namespace LaravelWinForms
{
    /// <summary>
    /// Every command goes through <c>cmd.exe /c</c>, so any text typed by the user must be
    /// validated first. Characters such as <c>&amp;</c>, <c>|</c>, <c>&gt;</c> or quotes would
    /// otherwise let a value run arbitrary commands (command injection).
    /// </summary>
    public static class InputValidator
    {
        // Composer project folder: letters, digits, dot, dash, underscore. Must not start with a dot or dash.
        private static readonly Regex ProjectNameRegex = new(@"^[A-Za-z0-9_][A-Za-z0-9._-]{0,99}$", RegexOptions.Compiled);

        // Artisan class names: PHP identifiers, optionally namespaced with / or \ (e.g. Admin/UserController).
        private static readonly Regex ClassNameRegex = new(@"^[A-Za-z_][A-Za-z0-9_]*([/\\][A-Za-z_][A-Za-z0-9_]*)*$", RegexOptions.Compiled);

        // Migration names: snake_case words (e.g. create_posts_table).
        private static readonly Regex MigrationNameRegex = new(@"^[a-z_][a-z0-9_]*$", RegexOptions.Compiled);

        // Composer version constraint as offered in the UI (e.g. 12.*, 11.0, ^10.2).
        private static readonly Regex VersionRegex = new(@"^[\^~]?\d+(\.(\d+|\*)){0,2}$", RegexOptions.Compiled);

        public static bool IsValidProjectName(string? value) =>
            !string.IsNullOrEmpty(value) && ProjectNameRegex.IsMatch(value);

        public static bool IsValidClassName(string? value) =>
            !string.IsNullOrEmpty(value) && value.Length <= 150 && ClassNameRegex.IsMatch(value);

        public static bool IsValidMigrationName(string? value) =>
            !string.IsNullOrEmpty(value) && value.Length <= 150 && MigrationNameRegex.IsMatch(value);

        public static bool IsValidVersion(string? value) =>
            !string.IsNullOrEmpty(value) && VersionRegex.IsMatch(value);
    }
}
