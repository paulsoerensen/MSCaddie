namespace MSCaddie.Utils;

public static class RouteUtils
{

    public const string Clubs = "clubs";
    public static string Courses(int clubId) => $"club/{clubId}/course";
    public static string Matches => $"match";
    public static string MatchDetailView(int matchId) => $"matchDetailView/{matchId}";
    public static string MatchResult(int matchId) => $"match/{matchId}/result";
    public static string MatchResultView(int matchId) => $"matchresult/{matchId}";
    public static string MemberDetailView(int memberId) => $"member/{memberId}";

    //public static string DatasetFields(int id) => $"datasets/{id}/fields";
    //public static string DatasetFieldDetails(int datasetId, int fieldId) => $"datasets/{datasetId}/fields/{fieldId}";
    //public static string DatasetGroups(int id) => $"datasets/{id}/groups";
    //public static string DatasetDetails(int id) => $"datasets/{id}";
    //public static string DatasetWatchdog(int id) => $"admin/watchdog/{id}";
    //public static string DatasetDoc(int id) => $"datasetdoc/{id}";


    //public const string Fields = "fields";
    //public static string FieldsDetails(int id) => $"fields/{id}";


    //public const string Groups = "groups";
    //public static string GroupsDetails(int id) => $"groups/{id}";
    //public static string GroupDatasets(int id) => $"groups/{id}/datasets";

    //public const string Operations = "operations";

    //public const string News = "news";
    //public static string NewsDetails(int newsId) => $"news/{newsId}";

}