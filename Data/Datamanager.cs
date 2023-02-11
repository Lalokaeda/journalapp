using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using journalapp.Data.Repositories;

namespace journalapp.Data
{
    public static class Datamanager
    {
        public static ClassTeacherRepos classTeacher{get; set;}
        public static StudRepos student{get; set;}
        public static HealthGroupRepos healthGroup{get; set;}
        public static CuratorRepos curator{get; set;}
        public static CourseOfGroupRepos courseOfGroup{get; set;}
        public static EventRepos eventt{get; set;}
        // public GraphicVisitsHostelRepos graphicVisitsHostel{get; set;}
        public static GroupRepos group{get; set;}
        public static HostelRepos hostel{get; set;}
        // public LineOfBusinessRepos lineOfBusiness{get; set;}
        public static RiskGroupRepos riskGroup{get; set;}
        public static RoomRepos room{get; set;}
        public static SpecialityRepos speciality{get; set;}
        public static TypeOfCrimeRepos typeOfCrime{get; set;}
        public static WorkWithParentRepos workWithParent {get; set;}

        // public Datamanager(ClassTeacherRepos classTeacherRepos,StudRepos studRepos, HealthGroupRepos healthGroupRepos,
        // CuratorRepos curatorRepos, CourseOfGroupRepos courseOfGroupRepos, EventRepos eventRepos, GroupRepos groupRepos,
        // HostelRepos hostelRepos, RiskGroupRepos riskGroupRepos, RoomRepos roomRepos, SpecialityRepos specialityRepos,
        // TypeOfCrimeRepos typeOfCrimeRepos, WorkWithParentRepos workWithParentRepos){
        
        // classTeacher=classTeacherRepos;
        // student=studRepos;
        // healthGroup=healthGroupRepos;
        // curator=curatorRepos;
        // courseOfGroup=courseOfGroupRepos;
        // eventt=eventRepos;
        // // public GraphicVisitsHostelRepos graphicVisitsHostel{get; set;}
        // group=groupRepos;
        // hostel=hostelRepos;
        // // public LineOfBusinessRepos lineOfBusiness{get; set;}
        // riskGroup=riskGroupRepos;
        // room=roomRepos;
        // speciality=specialityRepos;
        // typeOfCrime=typeOfCrimeRepos;
        // workWithParent =workWithParentRepos;
        // }
    }
}