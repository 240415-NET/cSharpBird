namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Data.SqlClient;
public class BirdSQL : IAccessBird
{
    string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");
    public List<Bird> GetFullBirdList()
    {
        //pulls full bird species data from a given csv. two files are currently present; USGSBBL.csv for the USGS Bird Banding Laboratory codes and ABACL for American Birding Association codes.
        //update line 15 to change standards
        string bandCode = "";
        string speciesName = "";
        string path = "data\\BirdCSV\\";
        string pathFile = path + "USGSBBL.csv";
        List<Bird> birdList = new List<Bird>();
        birdList = File.ReadAllLines(pathFile)
            .Select(line => line.Split(','))
            .Select(x => new Bird{
                bandCode = x[0],
                speciesName = x[1]
            }).ToList();

        return birdList;
    }
    public void WriteBirdsForChecklist(Checklist checklist)
    {
        //inserts new birds into sql table with proper checklist id
        //for optimization, this could be adjusted to only write those with new numbers, but is retained this way for compatibility with JSON version
        using SqlConnection connection = new SqlConnection (_connectionstring);
        List<Bird> bird = checklist.birds;
        Bird temp = new Bird();

        connection.Open();

        for (int i =0; i < bird.Count; i++)
        {
            string cmdText = "INSERT INTO birds (checklistID, bandCode, speciesName, numSeen, bbc, bnotes) VALUES (@checklistID, @bandCode, @speciesName, @numSeen, @bbc, @bnotes);";

            using SqlCommand cmd = new SqlCommand(cmdText,connection);
            temp = bird[i];
            cmd.Parameters.AddWithValue("@checklistID",checklist.checklistID);
            cmd.Parameters.AddWithValue("@bandCode",temp.bandCode);
            cmd.Parameters.AddWithValue("@speciesName",temp.speciesName);
            if (temp.numSeen < 1)
                 cmd.Parameters.AddWithValue("@numSeen",0);
            else
                cmd.Parameters.AddWithValue("@numSeen",temp.numSeen);
            if (String.IsNullOrEmpty(temp.bbc))
                cmd.Parameters.AddWithValue("@bbc","");
            else
                cmd.Parameters.AddWithValue("@bbc",temp.bbc);
            if (String.IsNullOrEmpty(temp.bNotes))
                cmd.Parameters.AddWithValue("@bNotes","");
            else
                cmd.Parameters.AddWithValue("@bNotes",temp.bbc);

            cmd.ExecuteNonQuery();
        }
        connection.Close();
    }
    public void UpdateBirdsForChecklist(Checklist checklist)
    {
        //updates birds into sql table with proper checklist id
        //for optimization, this could be adjusted to only write those with new numbers, but is retained this way for compatibility with JSON version
        using SqlConnection connection = new SqlConnection (_connectionstring);
        List<Bird> bird = checklist.birds;
        Bird temp = new Bird();
        connection.Open();

        for (int i =0; i < bird.Count; i++)
        {
            string cmdText = "UPDATE birds SET checklistID = @checklistID, bandCode = @bandCode, speciesName = @speciesName, numSeen = @numSeen, bbc = @bbc, bnotes = @bNotes WHERE checklistID = @checklistID AND bandCode = @bandCode;";

            using SqlCommand cmd = new SqlCommand(cmdText,connection);
            temp = bird[i];
            cmd.Parameters.AddWithValue("@checklistID",checklist.checklistID);
            cmd.Parameters.AddWithValue("@bandCode",temp.bandCode);
            cmd.Parameters.AddWithValue("@speciesName",temp.speciesName);
            if (temp.numSeen < 1)
                 cmd.Parameters.AddWithValue("@numSeen",0);
            else
                cmd.Parameters.AddWithValue("@numSeen",temp.numSeen);
            if (String.IsNullOrEmpty(temp.bbc))
                cmd.Parameters.AddWithValue("@bbc","");
            else
                cmd.Parameters.AddWithValue("@bbc",temp.bbc);
            if (String.IsNullOrEmpty(temp.bNotes))
                cmd.Parameters.AddWithValue("@bNotes","");
            else
                cmd.Parameters.AddWithValue("@bNotes",temp.bbc);

            cmd.ExecuteNonQuery();
        }
        connection.Close();
    }
    public List<Bird> ReadBirdsForChecklist(Guid checklistID)
    {
        //reads each bird for a given checklist GUID
        List<Bird> checklistBirds = new List<Bird>();
        Guid badUID = new Guid("00000000-0000-0000-0000-000000000000");
        using SqlConnection connection = new SqlConnection(_connectionstring);
        connection.Open();
        string bandCode = "";
        string speciesName = "";
        int numSeen = 0;
        string bbc = "";
        string bNotes = "";
        string cmdText = "SELECT bandCode, speciesName, numSeen, bbc, bnotes FROM birds WHERE checklistID = @checklistID;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);
        cmd.Parameters.AddWithValue("@checklistID",checklistID);
        using SqlDataReader reader = cmd.ExecuteReader();
        int i = 0;
        try
        {
            while(reader.Read())
            {
                bandCode = reader.GetString(0);
                speciesName = reader.GetString(1);
                numSeen = reader.GetInt32(2);
                bbc = reader.GetString(3);
                bNotes = reader.GetString(4);
                Bird temp = new Bird(bandCode,speciesName,numSeen,bbc,bNotes);
                checklistBirds.Add(temp);
            }
        }
        catch(Exception e){
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }
        
       
        connection.Close();
        return checklistBirds;
    }
    public void DeleteBirdsForChecklist(Checklist deleteChecklist)
    {
        //used to delete no longer needed records when a checklist is deleted
        using SqlConnection connection = new SqlConnection(_connectionstring);
        connection.Open();

        string cmdText = "DELETE FROM birds WHERE checklistID = @checklistID;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);
        cmd.Parameters.AddWithValue("@checklistID",deleteChecklist.checklistID);

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}