using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rainbow_School
{
    public class Teacher
    {
        public string Sch_UID;
        public string Sch_fname;
        public string Sch_ClassNo;
        public string Sch_Section;
    }

    public class School : Teacher
    {
        //Location of the Text File 
        public string path = @"C:\Users\prashant_basavanna\Desktop\test1.txt";
        

        public List<Teacher> TTeach001 = new List<Teacher>();

        public void displaymenu()
        {
            //Main Menu 
            
            Console.WriteLine("");
            Console.WriteLine(" Welcome to RAINBOW SCHOOL ");
            Console.WriteLine(" Choose an Option : ");
            Console.WriteLine("");
            Console.WriteLine(" 1.Add Teacher Records");
            Console.WriteLine(" 2.Show All Records");
            Console.WriteLine(" 3.Search for a Record ");
            Console.WriteLine(" 4.Update a Particular Record ");
            Console.WriteLine("");
        }

        public void Addrecord()
        {
            try
            {
                //Add Teacher Record 
                Console.WriteLine("Enter the ID:");
                string Sch_UID1 = Console.ReadLine();
                Console.WriteLine("Enter the Name:");
                string Sch_fname1 = Console.ReadLine();
                Console.WriteLine("Enter the Class:");
                string Sch_ClassNo1 = Console.ReadLine();
                Console.WriteLine("Enter the Section:");
                string Sch_Section1 = Console.ReadLine();

                this.TTeach001 = new List<Teacher>
                {
                new Teacher { Sch_UID = Sch_UID1, Sch_fname = Sch_fname1, Sch_ClassNo = Sch_ClassNo1, Sch_Section = Sch_Section1 }
                };

                StreamWriter sw = new StreamWriter(path, true);
                foreach (Teacher Tech in TTeach001)
                {
                    // Write to the file and console
                    Console.WriteLine($"\nTeacher Details As Follows: {Tech.Sch_UID}|{Tech.Sch_fname}|{Tech.Sch_ClassNo}|{Tech.Sch_Section}");
                    sw.WriteLine($"{Tech.Sch_UID}:{Tech.Sch_fname}|{Tech.Sch_ClassNo}|{Tech.Sch_Section}");
                    Console.WriteLine("\n\nAdd Record is Succesfull");
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in the Addrecord() Method: " + e.Message);
            }
            finally
            {
                Console.WriteLine("\n----------------------------------------------\n");
            }
        }

        public void getAllRecord()
        {
            try
            {
                int count = 0;

                // Display the Teacher Record
                StreamReader sr1 = new StreamReader(path);
                string msg = sr1.ReadToEnd();
                Console.Write(msg);
                sr1.Close();

                // Count the nos of Teacher Record 
                using (var sr2 = new StreamReader(path))
                {
                    while (sr2.ReadLine() != null)
                    {
                        count++;
                    }
                }
                Console.WriteLine("\nTeacher Record Count:" + count);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in the getAllRecord() Method: " + e.Message);
            }
            finally
            {
                Console.WriteLine("\n----------------------------------------------\n");
            }
        }

        public void getParticularRecord(String Tid)
        {
            try
            {
                int recordcount = 0;

                // Find the particular Record 
                List<string> Filelines = System.IO.File.ReadLines(path).ToList();
                for (int i = 0; i < Filelines.Count; i++)
                {
                    if (Filelines[i].Contains(Tid))
                    {
                        Console.WriteLine("\n Teacher Record Details =>>" + Filelines[i]);
                        recordcount++;
                    }
                }

                if (recordcount == 0)
                {
                    Console.WriteLine("\nDidn't Find the Teacher");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in the getParticularRecord() Method: " + e.Message);
            }
            finally
            {
                Console.WriteLine("\n----------------------------------------------\n");
            }
        }

        public void updateParticularRecord(String TID, String RecordFeild, String Oldvalue, String Newvalue)
        {
            try
            {
                // Read the File
                StreamReader sr = new StreamReader(path, true);
                string msg = sr.ReadToEnd();
                sr.Close();

                if (msg.Contains(TID))
                {
                    msg = Regex.Replace(msg, Oldvalue, Newvalue);
                    Console.WriteLine("Record updated successfully: " + TID + "\n Feild Updated :" + RecordFeild + "\n Old Value :" + Oldvalue + "\n Newvalue :" + Newvalue);
                }
                else
                {
                    Console.WriteLine("Didn't find the Teacher :" + TID);
                }

                //Update the Msg to the File
                StreamWriter fname = new StreamWriter(path);
                fname.Write(msg);
                fname.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in the updateParticularRecord Method(): " + e.Message);
            }
            finally
            {
                Console.WriteLine("\n----------------------------------------------\n");
            }
        }
    }

    class SchoolProject : School
    {
        public static void Main(string[] args)
        {
            int option;
            String userresponse;

            School schobj = new School();

            try
            {
                do
                {
                    schobj.displaymenu();

                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            schobj.Addrecord();
                            break;

                        case 2:
                            schobj.getAllRecord();
                            break;

                        case 3:
                            Console.WriteLine("Please enter the Teacher ID");
                            String TID1 = Console.ReadLine();
                            schobj.getParticularRecord(TID1);
                            break;

                        case 4:
                            Console.WriteLine("Please enter the Teacher ID");
                            String TID = Console.ReadLine();
                            schobj.getParticularRecord(TID);

                            Console.WriteLine("\nDo you want to Update for the record ?");
                            String RecordFeild = Console.ReadLine();
                            Console.WriteLine("\nEnter Existing Value \n");
                            String Oldvalue = Console.ReadLine();
                            Console.WriteLine("\nEnter the new Value \n");
                            String Newvalue = Console.ReadLine();

                            schobj.updateParticularRecord(TID, RecordFeild, Oldvalue, Newvalue);
                            break;

                        default:
                            {
                                Console.WriteLine("\nPlease select the correct option");
                                break;
                            }
                    }

                    Console.WriteLine("Do you want to Continue :(Y/N)");
                    userresponse = Console.ReadLine();
                    Console.WriteLine("\n-------------------------------\n");
                    //Console.Clear();

                }
                while (userresponse.Equals("Y", StringComparison.OrdinalIgnoreCase));
            }

            catch (Exception Ex)
            {
                Console.WriteLine("Exception in the Main Method" + Ex);
            }

            Console.ReadKey();
        }
    }
}
