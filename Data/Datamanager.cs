using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace journalapp.Data
{
    public class Datamanager
    {
        public ClassTeacher classTeacher{get; set;}
        public Student student{get; set;}
        public HealthGroup healthGroup{get; set;}
        public Curator curator{get; set;}
        public CourseOfGroup courseOfGroup{get; set;}
        public Event eventt{get; set;}
        public GraphicVisitsHostel graphicVisitsHostel{get; set;}
        public Group group{get; set;}
        public Hostel hostel{get; set;}
        public LineOfBusiness lineOfBusiness{get; set;}
        public RiskGroup riskGroup{get; set;}
        public Room room{get; set;}
        public Speciality speciality{get; set;}
        public TypeOfCrime typeOfCrime{get; set;}
        public WorkWithParent workWithParent {get; set;}
    }
}