using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace KelimeOyunu
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-FITSEK9;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adp = new SqlDataAdapter("Select ProductName from Products", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                string urunAdi = row["ProductName"].ToString();

                if (urunAdi.Contains(" "))
                {
                    string tekkelimeurunAdi = urunAdi.Substring(0, urunAdi.IndexOf(" "));
                    row["ProductName"] = tekkelimeurunAdi;
                }
            }

            do
            {
                Console.Write("Bir harf giriniz : ");
                string harf = Console.ReadLine();
                Console.WriteLine();
                int count = 0;
                do
                {
                    DataRow[] query = dt.Select("ProductName like '" + harf + "%'");
                    if (query.Count() > 0)
                    {
                        string kelime = query[0]["ProductName"].ToString();
                        harf = kelime.Substring(kelime.Length - 1);
                        Console.WriteLine(query[0]["ProductName"].ToString());
                        count++;
                    }
                    else
                    {
                        break;
                    }
                } while (true);
                Console.WriteLine("{0} tane kelime bulundu", count);
                Console.WriteLine();
            } while (true);

        }
    }
}
