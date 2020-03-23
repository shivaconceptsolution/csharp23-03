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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace CodeFirstApproach
{
    /// <summary>
    /// Interaction logic for DisconnectedExample.xaml
    /// </summary>
    public partial class DisconnectedExample : Window
    {
        SqlDataAdapter da;
        SqlCommandBuilder cmb;
       // DataTable dt;
        DataSet ds;
        String stmt = "";
        SqlConnection conn;
        public DisconnectedExample()
        {
            InitializeComponent();
            string con = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            conn = new SqlConnection(con);
            da = new SqlDataAdapter("select * from Students", conn);
            cmb = new SqlCommandBuilder(da);
           // dt = new DataTable();
            ds = new DataSet();
            da.Fill(ds,"Students");
          //  grd1.ItemsSource = dt.DefaultView;
            grd1.ItemsSource = ds.Tables["Students"].DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // da.Update(ds);
            da.Update(ds,"Students");
         //   grd1.ItemsSource = dt.DefaultView;
            grd1.ItemsSource = ds.Tables[0].DefaultView;
            MessageBox.Show("Data inserted Successfully");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
             // da.Update(ds);
             // grd1.ItemsSource = dt.DefaultView;
               da.Update(ds, "Students");
               grd1.ItemsSource = ds.Tables[0].DefaultView;
              MessageBox.Show("Data updated Successfully");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int r = grd1.SelectedIndex;
            // MessageBox.Show(""+rowi);
            //dt.Rows[r].Delete();
            ds.Tables[0].Rows[r].Delete();
            da.Update(ds.Tables[0]);
            grd1.ItemsSource = ds.Tables[0].DefaultView;
           // grd1.ItemsSource = dt.DefaultView;
        }
        
    }
}
