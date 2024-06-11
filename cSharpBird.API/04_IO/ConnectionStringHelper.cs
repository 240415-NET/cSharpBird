namespace cSharpBird.Api;

public static class ConnectionStringHelper
{
    public static string GetConnectionString()
    {
        if (File.Exists("C:\\Users\\U0LA19\\Documents\\cSharpBirdWeb_DataSource.txt")) //Ron
        {
            return File.ReadAllText(@"C:\\Users\\U0LA19\\Documents\\cSharpBirdWeb_DataSource.txt");
        }
        else if (File.Exists(@"C:\\Users\\U1C212\Desktop\\cSharpBirdWeb_DataSource.txt")) //Brad
        {
            return File.ReadAllText(@"C:\\Users\\U1C212\Desktop\\cSharpBirdWeb_DataSource.txt");
        }
        else if (File.Exists(@"C:\\Users\\U0SA29\\Documents\\Revature\\bootcamp\\connstringBird.txt")) //Dave
        {
            return File.ReadAllText(@"C:\\Users\\U0SA29\\Documents\\Revature\\bootcamp\\connstringBird.txt");
        }
        else if (File.Exists(@"C:\\Users\\U1H007\\Revature Engineer Bootcamp\\FernandoCabrejo\\cSharpBirds\\ConnectioncSharpBirdsProject.txt")) //Fernando
        {
            return File.ReadAllText(@"C:\\Users\\U1H007\\Revature Engineer Bootcamp\\FernandoCabrejo\\cSharpBirds\\ConnectioncSharpBirdsProject.txt");
        }
        else //Josh
        {
            return File.ReadAllText(@"C:\\Users\\U1C409\\Documents\\P1Extras\\ConnString.txt");
        }
    }
}