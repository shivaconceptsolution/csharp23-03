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
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace CodeFirstApproach
{
    /// <summary>
    /// Interaction logic for DisconnedtedExample.xaml
    /// </summary>
    public partial class DisconnedtedExample : Window
    {
        SqlDataAdapter da;
        SqlCommandBuilder cmb;
        DataTable dt;
        String stmt = "";
        SqlConnection conn;
        bool flag = true;
        public DisconnedtedExample()
        {
            InitializeComponent();
            string con = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            conn= new SqlConnection(con);
            //da = new SqlDataAdapter("select * from Students", conn);
            da = new SqlDataAdapter("Sp_GetStudentData",conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            cmb = new SqlCommandBuilder(da);
            dt = new DataTable();
            da.Fill(dt);
            grd1.ItemsSource = dt.DefaultView;
               
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

           
            
           
            if (btn.Name == "btninsert" )
            {
             DataRow dr = dt.NewRow();
             dr[0] = int.Parse(txt1.Text);
             dr[1] = txt2.Text;
             dr[2] = txt3.Text;
             dr[3] = txt4.Text;
             dr[4] = 1;
             dr[5] = "2nd";
             dr[6] = "981234445";
             dt.Rows.Add(dr);
             da.Update(dt);
             grd1.ItemsSource = dt.DefaultView;
            }
            else if (btn.Name == "btnupdate")
            {
                int r = grd1.SelectedIndex;
                dt.Rows[r]["Rno"] = txt1.Text;
                dt.Rows[r]["Sname"] = txt2.Text;
                dt.Rows[r]["Branch"] = txt3.Text;
                dt.Rows[r]["Fees"] = txt4.Text;
                dt.Rows[r]["CourseCourseId"] = 1;
                dt.Rows[r]["Semester"] = "2nd";
                dt.Rows[r]["Mobile"] = "981234445";
                da.Update(dt);
                grd1.ItemsSource = dt.DefaultView;

            }
            else if (btn.Name == "btndelete")
            {
                flag = false;
               int r = grd1.SelectedIndex;
               // MessageBox.Show(""+rowi);
               dt.Rows[r].Delete();
               da.Update(dt);
               grd1.ItemsSource = dt.DefaultView;
            }
            else
            {
                stmt = "select * from students";
            }
          
            
            
           
        }

        private void grd_selection(object sender, SelectionChangedEventArgs e)
        {
           if(flag)
           {
            DataRowView row = grd1.SelectedCells[0].Item as DataRowView;
            txt1.Text = row[0].ToString();
            txt2.Text = row[1].ToString();
            txt3.Text = row[2].ToString();
            txt4.Text = row[3].ToString();
            }
        }

       
    }
}
