namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Text.Json;

public class ChecklistSQL : IAccessChecklistFile
{
    string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");
    
    public List<Checklist> GetLists(User searchUser)
    {
        //retrieves all lists for a given userID
        List<Checklist> userChecklists = new List<Checklist>();
        Guid badUID = new Guid("00000000-0000-0000-0000-000000000000");
        using SqlConnection connection = new SqlConnection(_connectionstring);
        connection.Open();
        
        string cmdText = "SELECT checklistID, userId, locationName, checklistDateTime, birds, distance, duration, stationary, cNotes FROM checklists WHERE userId = @userId;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);
        cmd.Parameters.AddWithValue("@userId",searchUser.userId);
        using SqlDataReader reader = cmd.ExecuteReader();
        int i = 0;
        try{
             while(reader.Read())
            {            
                Guid _checklistID = reader.GetGuid(0);
                Guid _userId = reader.GetGuid(1);
                string _locationName = reader.GetString(2);
                DateTime _checklistDateTime = reader.GetDateTime(3);
                string tempBirds = JsonSerializer.Serialize(reader.GetString(4));

                List<Bird> _birds = BirdController.ReadBirdsForChecklist(_checklistID);
                float _distance = reader.GetFloat(5);
                int _duration = reader.GetInt32(6);
                bool _stationary = reader.GetBoolean(7);
                string _cNotes = reader.GetString(8);
                userChecklists.Add(new Checklist(_checklistID,_userId,_locationName,_checklistDateTime,_birds,_distance,_duration,_stationary,_cNotes));   
            }
        }
        catch(Exception e){
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }       
        connection.Close();
        return userChecklists;
    }
    public void WriteChecklist(Checklist newList)
    {
        //creates new checklist for the current user
        using SqlConnection connection = new SqlConnection (_connectionstring);

        connection.Open();

        string cmdText = "INSERT INTO checklists (checklistID, userId, locationName, checklistDateTime, birds, distance, duration, stationary, cNotes) VALUES (@checklistID, @userId, @locationName, @checklistDateTime, @birds, @distance, @duration, @stationary, @cNotes);";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.Parameters.AddWithValue("@checklistID",newList.checklistID);
        cmd.Parameters.AddWithValue("@userId",newList.userId);
        cmd.Parameters.AddWithValue("@locationName",newList.locationName);
        cmd.Parameters.AddWithValue("@checklistDateTime",newList.checklistDateTime);
        cmd.Parameters.AddWithValue("@birds",JsonSerializer.Serialize(newList.birds));
        cmd.Parameters.AddWithValue("@distance",0);
        cmd.Parameters.AddWithValue("@duration",0);
        cmd.Parameters.AddWithValue("@stationary",0);
        cmd.Parameters.AddWithValue("@cNotes",newList.cNotes);

        cmd.ExecuteNonQuery();
        connection.Close();

        BirdController.WriteBirdsForChecklist(newList);
    }

    public void WriteUpdatedList(Checklist updatedList)
    {
        //updates the selected checklist
        using SqlConnection connection = new SqlConnection (_connectionstring);
        try{
            connection.Open();

            string cmdText = "UPDATE checklists SET checklistID=@checklistID, userId=@userId, locationName=@locationName, checklistDateTime=@checklistDateTime, birds=@birds, distance=@distance, duration=@duration, stationary=@stationary, cNotes=@cNotes WHERE checklistID = @checklistID;";

            using SqlCommand cmd = new SqlCommand(cmdText,connection);

            cmd.Parameters.AddWithValue("@checklistID",updatedList.checklistID);
            cmd.Parameters.AddWithValue("@userId",updatedList.userId);
            cmd.Parameters.AddWithValue("@locationName",updatedList.locationName);
            cmd.Parameters.AddWithValue("@checklistDateTime",updatedList.checklistDateTime);
            cmd.Parameters.AddWithValue("@birds",JsonSerializer.Serialize(updatedList.birds));
            cmd.Parameters.AddWithValue("@distance",updatedList.distance);
            cmd.Parameters.AddWithValue("@duration",updatedList.duration);
            cmd.Parameters.AddWithValue("@stationary",updatedList.stationary);
            cmd.Parameters.AddWithValue("@cNotes",updatedList.cNotes);

            cmd.ExecuteNonQuery();
            connection.Close();

            BirdController.UpdateBirdsForChecklist(updatedList);
        }
        catch (Exception e){
            Console.WriteLine(e.StackTrace);
            Console.WriteLine(e.Message);
        }
        
    }
    public void DeleteChecklist(Checklist deleteChecklist)
    {
        //deletes the checklist, then calls for the matching birds to be removed from that table
        using SqlConnection connection = new SqlConnection(_connectionstring);
        connection.Open();

        string cmdText = "DELETE FROM checklists WHERE checklistID = @checklistID;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);
        cmd.Parameters.AddWithValue("@checklistID",deleteChecklist.checklistID);

        cmd.ExecuteNonQuery();
        connection.Close();

        BirdController.DeleteBirdsForChecklist(deleteChecklist);
    }
}