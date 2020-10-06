using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Kursa
{
    class Model
    {

        public SqlConnection connection = null;
        public SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
        static Model self = null;
        public Model()
        {

            //connectionString.DataSource = @"A311_10\SQL_E1118P1";
            connectionString.DataSource = @"DESKTOP-U6037SF\SQLVADIM";
            connectionString.InitialCatalog = @"Kurs_Lab";
            connectionString.UserID = @"E1118P1";
            connectionString.Password = @"E1118P1";
            //connectionString.Password = @"1P8111E";
            connection = new SqlConnection();
            connection.ConnectionString = connectionString.ConnectionString;
        }

        public static Model Self
        {
            get
            {
                if (self == null)
                {
                    self = new Model();

                }
                return self;
            }
        }

      
        /// <summary>
        ///  соединенние с базой данных 
        /// </summary>
        /// <returns></returns>
        public bool Connection()
        {

            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    return true;
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// закрыть соединение с бд
        /// </summary>
        /// <returns></returns>
        public bool RasConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Check(string log_name, string pas_name)
        {
 
            if (Connection())
            {
                SqlCommand com_user = new SqlCommand("select [password],[login] from [Login]", connection);
                SqlDataReader read_user = com_user.ExecuteReader();

                while (read_user.Read())
                {
                    if (pas_name == (string)read_user.GetValue(0) && log_name == (string)read_user.GetValue(1))
                    {
                        read_user.Close();
                        

                   

                        RasConnection();
                        return true;
                    }
                }
                RasConnection();
                return false;                         
            }
            else
            {
                throw new Exception("Соединения нет");
            }
        }

     

        int id_groups = 0;
        public bool Search(string name, string what)
        {
            if (Connection())
            {
                SqlCommand com_user = new SqlCommand("select * from Groups", connection);
                SqlDataReader read_user = com_user.ExecuteReader();

                while (read_user.Read())
                {
                    if (name == (string)read_user.GetValue(1) && what == (string)read_user.GetValue(2))
                    {
                        id_groups = Convert.ToInt32( read_user.GetValue(0)); 
                        read_user.Close();
                        return true;
                        
                    }
                }
                

                RasConnection();
                return false;
            }
            else
            {
                throw new Exception("Соединения нет");
            }
          
        }

        int last_id = 0;
        public bool Search()
        {
            if (Connection())
            {


                SqlCommand com_user = new SqlCommand("select Max(id) from Groups", connection);
                SqlDataReader read_user = com_user.ExecuteReader();

                read_user.Read();
                last_id = Convert.ToInt32(read_user.GetValue(0));
                read_user.Close();
                RasConnection();
                return true;       

                  
            }
            else
            {
                throw new Exception("Соединения нет");
            }

        }

        int id_people = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateIn"> DATEIO</param>
        /// <param name="DateOut">DATEIO</param>
        /// <param name="FirstName">People</param>
        /// <param name="LastName">People</param>
        /// <param name="Otchestvo">People</param>
        /// <param name="nphone">People</param>
        /// <param name="foto">People</param>
        /// <param name="uslov">Costs</param>
        /// <param name="costs">Costs</param>
        /// <param name="group" >groups</param>
        public string id_pep = null;
        public void Zapis_Peop(string FirstName, string LastName, string Otchestvo,string nphone,byte[] foto)//запись и перезапись
        {
     
            if (Connection())
            {       
                            
                SqlCommand command_peop = new SqlCommand("insert into People(foto,name,lastname,otchestvo,nphone,id_group) values(@foto,@FirstName, @LastName, @Otchestvo,@nphone,@id_groups )", connection);
                 
                command_peop.Parameters.AddWithValue("@foto", foto);
                command_peop.Parameters.AddWithValue("@FirstName", FirstName);
                command_peop.Parameters.AddWithValue("@LastName", LastName);
                command_peop.Parameters.AddWithValue("@Otchestvo", Otchestvo);
                command_peop.Parameters.AddWithValue("@nphone", nphone);
                command_peop.Parameters.AddWithValue("@id_groups",id_groups);

                command_peop.ExecuteNonQuery();

                SqlCommand com_user = new SqlCommand("select * from People", connection);
                SqlDataReader read_user = com_user.ExecuteReader();

                while (read_user.Read())
                {
                    if (nphone == (string)read_user.GetValue(5))
                    {
                        id_people = Convert.ToInt32(read_user.GetValue(0));
                        id_pep = read_user.GetValue(0).ToString();
                        read_user.Close();
                        break;
                    }
                }

                RasConnection();
            }
            else throw new Exception("Соединение не установленно");
        }

        public void Zapis_Ab(int cost, string uslov, int vists)//запись и перезапись
        {

            if (Connection())
            {

                SqlCommand command = new SqlCommand("insert into Abonement values(@cost,@uslov, @vists,@id_peopl )", connection);

                command.Parameters.AddWithValue("@cost", cost);
                command.Parameters.AddWithValue("@uslov", uslov);
                command.Parameters.AddWithValue("@vists", vists);
                command.Parameters.AddWithValue("@id_peopl", id_people);


                command.ExecuteNonQuery();
                

                RasConnection();
            }
            else throw new Exception("Соединение не установленно");
        }

        public void Zapis_ProvAdmin( string name, string vid, string ops, string timeS, string timeE)//запись и перезапись
        {

            if (Connection())
            {

                SqlCommand command = new SqlCommand("insert into Proverkas values(@name,@vid, @ops,@start,@ends )", connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@vid", vid);
                command.Parameters.AddWithValue("@ops", ops);
                command.Parameters.AddWithValue("@start", timeS);
                command.Parameters.AddWithValue("@ends", timeE);

                command.ExecuteNonQuery();


                RasConnection();
            }
            else throw new Exception("Соединение не установленно");
        }

        public void Zapis_GroupandStartd(string name, string what, DateTime DateStart, DateTime DateEnd, string timeS, string timeE)//запись и перезапись
        {

            if (Connection())
            {

                SqlCommand command = new SqlCommand("insert into Groups values(@name,@what)", connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@what", what);
                
                command.ExecuteNonQuery();
            
                SqlCommand commandS = new SqlCommand("insert into Schedule values(@DateStart,@DateEnd,@timeS,@timeE,@id_group)", connection);

                commandS.Parameters.AddWithValue("@DateStart", DateStart);
                commandS.Parameters.AddWithValue("@DateEnd", DateEnd);
                commandS.Parameters.AddWithValue("@timeS", timeS);
                commandS.Parameters.AddWithValue("@timeE", timeE);
                Search();
                Connection();
                commandS.Parameters.AddWithValue("@id_group", last_id);



                commandS.ExecuteNonQuery();

               

                RasConnection();
            }
            else throw new Exception("Соединение не установленно");
        }


        public DataTable ProvTab()
        {
            if (Connection())
            {
                SqlCommand comanda = new SqlCommand("select name as [Группа],vid as [Деятельность],opis as [Описание],start as [Начало],ends as [Конец] from Proverkas", connection);
               
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comanda;
                DataTable table = new DataTable();
                adapter.Fill(table);

                RasConnection();
                return table;
            }
            else throw new Exception("Соединение не установленно");
         
          }

        public DataTable Vivod(string a)
        {
            if (Connection())
            {
                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comanda;
                DataTable table = new DataTable();
                adapter.Fill(table);

                RasConnection();
                return table;
            }
            else throw new Exception("Соединение не установленно");

        }
       
        public int Vivod_int(string a)
        {
            if (Connection())
            {
                int pepl_id = -1;
                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataReader read_user = comanda.ExecuteReader();

                read_user.Read();
                pepl_id = Convert.ToInt32(read_user.GetValue(0));
                read_user.Close();

                RasConnection();
                return pepl_id;
            }
            else throw new Exception("Соединение не установленно");

        }

        public void Zapis_Visit(DateTime t, int id)//запись и перезапись
        {

            if (Connection())
            {

                SqlCommand command = new SqlCommand("insert into VisitDay(DateIn,id_people) values(@DateIn,@pepl)", connection);

                command.Parameters.AddWithValue("@DateIn", t);
                command.Parameters.AddWithValue("@pepl", id);


                command.ExecuteNonQuery();


                RasConnection();
            }
            else throw new Exception("Соединение не установленно");
        }

      

        public void Update_Visit(int pepl_id)//запись и перезапись
        {

            if (Connection())
            {

                SqlCommand command = new SqlCommand("update VisitDay set DateOut = GETDATE() where id_people = @pepl and DateOut is null", connection);

                command.Parameters.AddWithValue("@pepl", pepl_id);
        
                command.ExecuteNonQuery();
                RasConnection();
                   
            }
            else throw new Exception("Соединение не установленно");
        }


    

        public bool Vivod_Bool_in(string a)
        {
            if (Connection())
            {
           
                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataReader read_user = comanda.ExecuteReader();
                while (read_user.Read())
                {
                    if (read_user.GetValue(0).ToString() != "" &&  read_user.GetValue(1).ToString() == "")
                    {
                        read_user.Close();
                        RasConnection();
                        return false;
                    }
                }
                read_user.Close();
                RasConnection();
                return true;
            }
            else throw new Exception("Соединение не установленно");

        }

        public bool Vivod_Bool_out(string a)
        {
            if (Connection())
            {

                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataReader read_user = comanda.ExecuteReader();
                while (read_user.Read())
                {
                    if (read_user.GetValue(0).ToString() != ""  )
                    {
                        read_user.Close();
                        RasConnection();
                        return false;
                    }
                }
                read_user.Close();
                RasConnection();
                return true;
            }
            else throw new Exception("Соединение не установленно");

        }

        public void Delete (string d)
        {

            if (Connection())
            {

                SqlCommand command = new SqlCommand(d, connection);

                command.ExecuteNonQuery();

                RasConnection();
            }
            else throw new Exception("Соединение не установленно");
        }

        public string Vivod_String(string a)
        {
            if (Connection())
            {
                string pep = null;
                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataReader read_user = comanda.ExecuteReader();

                read_user.Read();
                pep = read_user.GetValue(0).ToString() + "_" + read_user.GetValue(1).ToString() + "_" + read_user.GetValue(2).ToString();
                read_user.Close();

                RasConnection();
                return pep;
            }
            else throw new Exception("Соединение не установленно");

        }

        public string Vivod_Sting(string a)
        {
            if (Connection())
            {
                string pep = null;
                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataReader read_user = comanda.ExecuteReader();

                read_user.Read();
                pep = read_user.GetValue(0).ToString() + " " + read_user.GetValue(1).ToString() + " " + read_user.GetValue(2).ToString() +" "+ read_user.GetValue(3).ToString();
                read_user.Close();

                RasConnection();
                return pep;
            }
            else throw new Exception("Соединение не установленно");

        }

        public string Vivod_login(string a)
        {
            if (Connection())
            {
                string pep = null;
                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataReader read_user = comanda.ExecuteReader();

                read_user.Read();
                pep = read_user.GetValue(1).ToString() + " " + read_user.GetValue(2).ToString();
                read_user.Close();

                RasConnection();
                return pep;
            }
            else throw new Exception("Соединение не установленно");

        }

        public byte[] Vivod_Byte(string a)
        {
            if (Connection())
            {
                byte []pep;
                SqlCommand comanda = new SqlCommand(a, connection);

                SqlDataReader read_user = comanda.ExecuteReader();

                read_user.Read();
                pep =(byte[])read_user.GetValue(0);
                read_user.Close();

                RasConnection();
                return pep;
            }
            else throw new Exception("Соединение не установленно");

        }

        public DataTable GetStatistic( DateTime from, DateTime to)
        {
            if (Connection())
            {
   
                SqlCommand command = new SqlCommand("execute market_get_statistic @date_from, @date_to", connection);
       
                command.Parameters.AddWithValue("@date_from", from);
                command.Parameters.AddWithValue("@date_to", to);
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();
                command.Connection.Close();
                RasConnection();
                return table;
            }
            else
            {
                throw new Exception("Ошибка соединения!");
            }
        }

    }
}
