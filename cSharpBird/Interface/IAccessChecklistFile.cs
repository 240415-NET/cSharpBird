namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IAccessChecklistFile
{
    public List<Checklist> GetLists(User searchUser);
    
    public void WriteChecklist(Checklist newList);

    public void WriteUpdatedList(Checklist updatedList);
    public void DeleteChecklist(Checklist deleteChecklist);
}