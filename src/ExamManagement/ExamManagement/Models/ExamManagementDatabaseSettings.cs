using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class ExamManagementDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        
        public string DatabaseName { get; set; } = null!;

        public CollectionsSettings Collections { get; set; } = null!;
    }

    public class CollectionsSettings
    {
        public string ExamsCollectionName { get; set; } = null!;

        public string ClassesCollectionName { get; set; } = null!;

        public string ModulesCollectionName { get; set; } = null!;

        public string ProctorsCollectionName { get; set; } = null!;
    }
}