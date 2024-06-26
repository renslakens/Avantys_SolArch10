namespace ScheduleManagement.Models;

public class ScheduleManagementDatabaseSettings {
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public CollectionsSettings Collections { get; set; } = null!;
}

public class CollectionsSettings {
    public string ClassesCollectionName { get; set; } = null!;
    public string ModulesCollectionName { get; set; } = null!;
    public string SchedulesCollectionName { get; set; } = null!;
    public string LessonsCollectionName { get; set; } = null!;
    public string TeachersCollectionName { get; set; } = null!;
    public string StudentsCollectionName { get; set; } = null!;
}