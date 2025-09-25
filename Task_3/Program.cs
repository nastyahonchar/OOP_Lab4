using System;

abstract class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }
}

class Patient : Person
{
    public Patient(string name) : base(name) { }
}

class Doctor : Person
{
    public List<Patient> Patients { get; set; }
    public Doctor(string name) : base(name)
    {
        Patients = new List<Patient>();
    }

    public void AddPatient(Patient p)
    {
        Patients.Add(p);
    }
}

class Room
{
    public int Number { get; set; }
    public List<Patient> Patients { get; set; }

    public Room(int number)
    {
        Number = number;
        Patients = new List<Patient>();
    }

    public bool AddPatient(Patient p)
    {
        if (Patients.Count < 3)
        {
            Patients.Add(p);
            return true;
        }
        return false;
    }
}

class Department
{
    public string Name { get; set; }
    public List<Room> Rooms { get; set; }

    public Department(string name)
    {
        Name = name;
        Rooms = new List<Room>();
        for (int i = 1; i <= 20; i++)
        {
            Rooms.Add(new Room(i));
        }
    }

    public bool AddPatient(Patient p)
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (Rooms[i].AddPatient(p))
                return true;
        }
        return false;
    }

    public List<Patient> GetAllPatients()
    {
        List<Patient> all = new List<Patient>();
        for (int i = 0; i < Rooms.Count; i++)
        {
            for (int j = 0; j < Rooms[i].Patients.Count; j++)
            {
                all.Add(Rooms[i].Patients[j]);
            }
        }
        return all;
    }
}

class Hospital
{
    public List<Department> Departments { get; set; }
    public List<Doctor> Doctors { get; set; }

    public Hospital()
    {
        Departments = new List<Department>();
        Doctors = new List<Doctor>();
    }

    private Department GetDepartment(string name)
    {
        for (int i = 0; i < Departments.Count; i++)
        {
            if (Departments[i].Name == name)
                return Departments[i];
        }
        Department newDep = new Department(name);
        Departments.Add(newDep);
        return newDep;
    }

    private Doctor GetDoctor(string fullName)
    {
        for (int i = 0; i < Doctors.Count; i++)
        {
            if (Doctors[i].Name == fullName)
                return Doctors[i];
        }
        Doctor newDoc = new Doctor(fullName);
        Doctors.Add(newDoc);
        return newDoc;
    }

    public void AddPatient(string depName, string docName, string docSurname, string patientName)
    {
        string fullDocName = docName + " " + docSurname;
        Department dep = GetDepartment(depName);
        Doctor doc = GetDoctor(fullDocName);
        Patient patient = new Patient(patientName);

        if (dep.AddPatient(patient))
            doc.AddPatient(patient);
        else
            Console.WriteLine($"There is no place for {patientName} in {depName} department");
    }

    public void PrintByDepartment(string depName)
    {
        Department dep = null;
        for (int i = 0; i < Departments.Count; i++)
        {
            if (Departments[i].Name == depName)
            {
                dep = Departments[i];
                break;
            }
        }

        if (dep != null)
        {
            List<Patient> patients = dep.GetAllPatients();
            for (int i = 0; i < patients.Count; i++)
            {
                Console.WriteLine(patients[i].Name);
            }
        }
    }

    public void PrintByRoom(string depName, int roomNumber)
    {
        Department dep = null;
        for (int i = 0; i < Departments.Count; i++)
        {
            if (Departments[i].Name == depName)
            {
                dep = Departments[i];
                break;
            }
        }

        if (dep != null)
        {
            Room room = null;
            for (int i = 0; i < dep.Rooms.Count; i++)
            {
                if (dep.Rooms[i].Number == roomNumber)
                {
                    room = dep.Rooms[i];
                    break;
                }
            }

            if (room != null)
            {
                List<string> names = new List<string>();
                for (int i = 0; i < room.Patients.Count; i++)
                {
                    names.Add(room.Patients[i].Name);
                }
                names.Sort();
                for (int i = 0; i < names.Count; i++)
                {
                    Console.WriteLine(names[i]);
                }
            }
        }
    }

    public void PrintByDoctor(string docFullName)
    {
        Doctor doc = null;
        for (int i = 0; i < Doctors.Count; i++)
        {
            if (Doctors[i].Name == docFullName)
            {
                doc = Doctors[i];
                break;
            }
        }

        if (doc != null)
        {
            List<string> names = new List<string>();
            for (int i = 0; i < doc.Patients.Count; i++)
            {
                names.Add(doc.Patients[i].Name);
            }
            names.Sort();
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine(names[i]);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Hospital hospital = new Hospital();

        while (true)
        {
            string line = Console.ReadLine();
            if (line == "Output")
                break;

            string[] info = line.Split(' ');
            hospital.AddPatient(info[0], info[1], info[2], info[3]);
        }

        while (true)
        {
            string command = Console.ReadLine();
            if (command == "End")
                break;

            string[] info = command.Split(' ');
            if (info.Length == 1)
                hospital.PrintByDepartment(info[0]);
            else if (info.Length == 2)
            {
                bool isNumber = true;
                for (int i = 0; i < info[1].Length; i++)
                {
                    if (!char.IsDigit(info[1][i]))
                    {
                        isNumber = false;
                        break;
                    }
                }

                if (isNumber)
                {
                    int roomNum = int.Parse(info[1]);
                    hospital.PrintByRoom(info[0], roomNum);
                }
                else
                {
                    string docFullName = info[0] + " " + info[1];
                    hospital.PrintByDoctor(docFullName);
                }
            }
        }
    }
}