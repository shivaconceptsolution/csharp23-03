using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace CodeFirstApproach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // StudentDB db = new StudentDB();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string con = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd;
            String stmt = "";
            if (btn.Name == "btninsert")
            {
                //stmt="insert into Students(Rno,Sname,Branch,Fees,CourseCourseId,Semester,Mobile) values('"+txt1.Text+"','"+txt2.Text+"','"+txt3.Text+"','"+txt4.Text+"',1,'d','d')";
                stmt = "SP_InsertData";
                
                cmd = new SqlCommand(stmt, conn);
                cmd.Parameters.AddWithValue("@Rno", Convert.ToInt32(txt1.Text));
                cmd.Parameters.AddWithValue("@Sname", txt2.Text);
                cmd.Parameters.AddWithValue("@Branch", txt3.Text);
                cmd.Parameters.AddWithValue("@Fees", txt4.Text);
                cmd.Parameters.AddWithValue("@CourseCourseId", 1);
                cmd.Parameters.AddWithValue("@Semester", "2nd");
                cmd.Parameters.AddWithValue("@Mobile", "98123");
                cmd.CommandType = CommandType.StoredProcedure;
            
            }
            else if(btn.Name=="btnupdate")
            {
                stmt="update Students set Sname='" + txt2.Text + "',Branch='" + txt3.Text + "',Fees='" + txt4.Text + "',CourseCourseId=1,Semester='3rd',Mobile='1111111' where Rno='" + txt1.Text + "'";
                cmd = new SqlCommand(stmt, conn);
            }
            else if (btn.Name == "btndelete")
            {
                stmt = "delete from Students  where Rno='" + txt1.Text + "'";
                cmd = new SqlCommand(stmt, conn);
            }
            else
            {
               // stmt = "select * from students";
                stmt = "SP_GetStudentData";
                cmd = new SqlCommand(stmt, conn);
                cmd.CommandType = CommandType.StoredProcedure;
            }
          
            conn.Open();
            if (btn.Name != "btnshow")
            {
                
                cmd.ExecuteNonQuery();
                MessageBox.Show("DML Successfully");
            }
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                grd1.ItemsSource = dt.DefaultView;
                dr.Close();
    
            }
            conn.Close();
            
        }

        /* private void btnupdate_Click(object sender, RoutedEventArgs e)
         {
             SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Hp\CodeFirstApproach.StudentDB.mdf;Integrated Security=True;Connect Timeout=30");
             SqlCommand cmd = new SqlCommand("update Students set Sname='" + txt2.Text + "',Branch='" + txt3.Text + "',Fees='" + txt4.Text + "',CourseCourseId=1,Semester='3rd',Mobile='1111111' where Rno='" + txt1.Text + "'", conn);
             conn.Open();
             cmd.ExecuteNonQuery();
             conn.Close();
             MessageBox.Show("Data Updated Successfully");


         }

         private void btnshow_Click(object sender, RoutedEventArgs e)
         {
             SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Hp\CodeFirstApproach.StudentDB.mdf;Integrated Security=True;Connect Timeout=30");
             SqlCommand cmd = new SqlCommand("select * from students", conn);
             conn.Open();
             SqlDataReader dr = cmd.ExecuteReader();
             DataTable dt = new DataTable();
             dt.Load(dr);
             grd1.ItemsSource = dt.DefaultView;
         }

         private void btndelete_Click(object sender, RoutedEventArgs e)
         {
             SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Hp\CodeFirstApproach.StudentDB.mdf;Integrated Security=True;Connect Timeout=30");
             SqlCommand cmd = new SqlCommand("delete from Students  where Rno='" + txt1.Text + "'", conn);
             conn.Open();
             cmd.ExecuteNonQuery();
             conn.Close();
             MessageBox.Show("Data Deleted Successfully");
 
         }
         */
        private void grd_selection(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(grd1.SelectedCells[0].Item.ToString());
            DataRowView row = grd1.SelectedCells[0].Item as DataRowView;
            txt1.Text = row[0].ToString();
            txt2.Text = row[1].ToString();
            txt3.Text = row[2].ToString();
            txt4.Text = row[3].ToString();
            

        }
    }
}
