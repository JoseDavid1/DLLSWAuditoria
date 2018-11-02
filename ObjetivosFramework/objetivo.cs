using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using Connection;

namespace ObjetivosFramework
{
    public partial class objetivo : Form
    {
        public objetivo()
        {
            InitializeComponent();
        }

        private void ObjetivosFramework_Load(object sender, EventArgs e)
        {
            //String stringpad = "server=localhost;user id=root;database=auditoria";
            String stringpad = "dsn=auditoria;UID=root;PWD=;";
            Connection.cone.dllconnectionDatabase connection = new Connection.cone.dllconnectionDatabase(stringpad);

            if (connection.OpenConnection())
            {
                /*We open the connection*/
                MessageBox.Show("We are connected");

                try
                {
                    
                    OdbcConnection cone = new OdbcConnection(stringpad);
                    string selectQuery = "select * from metodologias;";
                    string selectQuery1 = "select * from objetivo;";
                    
                    OdbcCommand command = new OdbcCommand(selectQuery,cone);
                    OdbcCommand command1 = new OdbcCommand(selectQuery1, cone);                    

                    OdbcDataAdapter mysqldt = new OdbcDataAdapter(command);
                    DataTable dt = new DataTable();
                    mysqldt.Fill(dt);

                    OdbcDataAdapter mysqldt1 = new OdbcDataAdapter(command1);
                    DataTable dt1 = new DataTable();
                    mysqldt1.Fill(dt1);

                    comboBox1.ValueMember = "idMetodologias";
                    comboBox1.DisplayMember = "Nombre";
                    comboBox1.DataSource = dt;

                    comboBox2.ValueMember = "idObjetivo";
                    comboBox2.DisplayMember = "Descripcion";
                    comboBox2.DataSource = dt1;
                                       
                    connection.OpenConnection();
                    dataGridView1.DataSource = connection.llenarDataGridView("select metodologias.Nombre, objetivo.Descripcion from metodologias inner join objetivo inner join metodologias_has_objetivo on metodologias_has_objetivo.metodologias_idMetodologias = metodologias.idMetodologias and metodologias_has_objetivo.objetivo_idObjetivo = objetivo.idObjetivo order by metodologias.Nombre;");
                    connection.CloseConnection();

                }
                catch (Exception ex)
                {


                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                /*We have an error*/
                MessageBox.Show("Error");
            }
            
        }




        }
    }
