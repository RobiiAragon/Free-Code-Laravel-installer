using LaravelWinForms;
using Xunit;

public class InputValidatorTests
{
    [Theory]
    [InlineData("blog")]
    [InlineData("my-app")]
    [InlineData("my_app.v2")]
    public void Accepts_valid_project_names(string name) => Assert.True(InputValidator.IsValidProjectName(name));

    [Theory]
    [InlineData("")]
    [InlineData("-rf")]
    [InlineData("app & del /q C:\\")]
    [InlineData("app\" && calc")]
    [InlineData("app|whoami")]
    [InlineData("..\\outside")]
    [InlineData("a b")]
    public void Rejects_dangerous_project_names(string name) => Assert.False(InputValidator.IsValidProjectName(name));

    [Theory]
    [InlineData("PostController")]
    [InlineData("Admin/UserController")]
    [InlineData("Admin\\User")]
    public void Accepts_valid_class_names(string name) => Assert.True(InputValidator.IsValidClassName(name));

    [Theory]
    [InlineData("")]
    [InlineData("1Post")]
    [InlineData("Post & calc")]
    [InlineData("Post>out.txt")]
    [InlineData("Post%PATH%")]
    [InlineData("Admin//User")]
    public void Rejects_invalid_class_names(string name) => Assert.False(InputValidator.IsValidClassName(name));

    [Theory]
    [InlineData("create_posts_table", true)]
    [InlineData("add_votes_to_users_table", true)]
    [InlineData("CreatePosts", false)]
    [InlineData("x;calc", false)]
    public void Validates_migration_names(string name, bool expected) => Assert.Equal(expected, InputValidator.IsValidMigrationName(name));

    [Theory]
    [InlineData("12.*", true)]
    [InlineData("11.0", true)]
    [InlineData("^10.2", true)]
    [InlineData("12.* && calc", false)]
    [InlineData("dev-master", false)]
    public void Validates_versions(string version, bool expected) => Assert.Equal(expected, InputValidator.IsValidVersion(version));
}
