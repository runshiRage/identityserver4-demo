using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_autoquery.ServiceInterface.Services.Events
{
    public class School
    {
        public void Excutes()
        {
            Student student1 = new Student("Evan");
            ClassRoom classRoom1 = new ClassRoom("Class1");

            student1.GradeChange += classRoom1.Student_Grade;
            student1.UpdateGrade("高三", 98);
        }
    }

    public class Student
    {
        public event Action<object, GradeChangeEventArg> GradeChange;

        public string _Name;
        public Student(string Name)
        {
            this._Name = Name;
        }

        protected void OnGradeChange(GradeChangeEventArg arg)
        {
            GradeChange?.Invoke(this, arg);
        }

        //更新学生名字和引发分数变化事件
        public void UpdateGrade(string nm, int grade)
        {
            GradeChangeEventArg arg = new GradeChangeEventArg(nm, grade);
            this.OnGradeChange(arg);
        }
    }

    public class ClassRoom
    {
        public string _classname;

        public ClassRoom(string classname)
        {
            _classname = classname;
        }

        public void Student_Grade(object sender, GradeChangeEventArg arg)
        {
            Student st = sender as Student;
            Console.WriteLine($"{arg._name}班级{_classname}：同学{st._Name}收到期末成绩通知为{arg._Grade}");
        }
    }

    public class GradeChangeEventArg : EventArgs
    {
        public string _name;
        public float _Grade;

        public GradeChangeEventArg(string name, int Grade)
        {
            _name = name;
            _Grade = Grade;
        }
    }

    
}
