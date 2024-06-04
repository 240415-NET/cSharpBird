namespace cSharpBird.Test;

public class cSharpBirdTest
{

    //Remember this command! dotnet add ./cSharpBird.Test/cSharpBird.Test.csproj reference ./cSharpBird/cSharpBird.csproj
    [Theory]
    [InlineData("invalid@address")]
    [InlineData("whoopsnoat.com")]
    [InlineData("I'mJustAString")]
    [InlineData("maylookgood@butnotreally.com.")]
    public void ValidEmail_False(string testString)
    {
        bool result = UserController.ValidEmail(testString);

        Assert.False(result);
    }
    [Theory]
    [InlineData("valid@address.com")]
    [InlineData("symbols+go@testme.com")]
    [InlineData("whatabout@sub.domain.com")]
    public void ValidEmail_True(string testString)
    {
        bool result = UserController.ValidEmail(testString);

        Assert.True(result);
    }
    [Theory]
    [InlineData("whoops")]
    [InlineData("Nothing but text")]
    [InlineData("BLSK45")]
    public void ValidListUpdate_False(string testString)
    {
        bool result = ChecklistController.ValidListUpdate(testString);

        Assert.False(result);
    }
    [Theory]
    [InlineData("BLSK 45")]
    [InlineData("sora 1")]
    public void ValidListUpdate_True(string testString)
    {
        bool result = ChecklistController.ValidListUpdate(testString);

        Assert.True(result);
    }
    [Theory]
    [InlineData("defaultName","notPassword")]
    public void VerifyPassword_False(string testName,string testPW)
    {
        User testUser = User.FindUser(testName);
        bool result = CryptoController.VerifyPassword(testPW,testUser);

        Assert.False(result);
    }
    [Theory]
    [InlineData("defaultName","password")]
    public void VerifyPassword_True(string testName,string testPW)
    {
        User testUser = User.FindUser(testName);
        bool result = CryptoController.VerifyPassword(testPW,testUser);

        Assert.True(result);
    }
    [Theory]
    [InlineData("defaultName")]
    public void UserDupe_True(string testName)
    {
        bool result = UserCreation.UserDupe(testName);
        
        Assert.True(result);
    }
    [Theory]
    [InlineData("defaultName2")]
    public void UserDupe_False(string testName)
    {
        bool result = UserCreation.UserDupe(testName);
        
        Assert.False(result);
    }
}