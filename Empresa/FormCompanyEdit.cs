using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empresa
{
    public partial class FormCompanyEdit : Form
    {
        public FormCompanyEdit()
        {
            InitializeComponent();
        }

        private void FormCompanyEdit_Load(object sender, EventArgs e)
        {
            txtId.Visible = false;
            label8.Visible = false;
            LlenarPais();
        }

        private void FormCompanyEdit_FormClosing(Object sender, FormClosingEventArgs e)
        {
            Form1 uno = new Form1();
            uno.llenarGrid();

        }

        public string LabelText
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!validarCampos())
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            else
            {
                using (EmpresaEntities db = new EmpresaEntities())
                {
                    tbl_empresa una = new tbl_empresa();
                    una = db.tbl_empresa.Find(Convert.ToInt32(txtId.Text));
                    if (una == null) { 
                    una.nombreEmpresa = txtName.Text;
                    una.direccionEmpresa = txtDir.Text;
                    una.telEmpresa = txtTel.Text;
                    una.idCiudad = Convert.ToInt32(CmbCiudad.SelectedValue);
                    una.creacionEmpresa = DateTime.Now;
                    db.tbl_empresa.Add(una);
                    db.SaveChanges();
                    MessageBox.Show("Se agrego con exito");
                    this.Close();
                    }
                    else
                    {
                        una.nombreEmpresa = txtName.Text;
                        una.direccionEmpresa = txtDir.Text;
                        una.telEmpresa = txtTel.Text;
                        una.idCiudad = Convert.ToInt32(CmbCiudad.SelectedValue);
                        una.modEmpresa = DateTime.Now;
                        db.SaveChanges();
                        MessageBox.Show("Se modifico con exito");
                        this.Close();
                    }
                }
            }
        }
        private bool validarCampos()
        {
            if (txtName.Text == "" || txtDir.Text == "" || txtTel.Text == "" )
                return false;
            else
                return true;
        }

        private void btnCnl_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LlenarPais()
        {
            using (EmpresaEntities db = new EmpresaEntities())
            {
                cmbPais.DataSource = db.tbl_pais.ToList();
                cmbPais.DisplayMember = "nombrePais";
                cmbPais.ValueMember = "idPais";
            }
        }

         public void LlenarDepto(int id)
        {
            using (EmpresaEntities db = new EmpresaEntities())
            {
                cmbDepto.DisplayMember = "nombreDepto";
                cmbDepto.ValueMember = "idDepto";
                cmbDepto.DataSource = db.tbl_depto.Where(d => d.idPais == id).ToList();
                 
            }
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPais.SelectedValue.ToString() != "")

            {

                //cmbDepto.DataSource = null;
                //cmbDepto.Items.Clear();
                int CountryID = Convert.ToInt32(cmbPais.SelectedValue.ToString());
                LlenarDepto(CountryID);
            }
        }

        private void cmbDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepto.SelectedValue.ToString() != "")

            {

                CmbCiudad.DataSource = null;
                CmbCiudad.Items.Clear();
                int deptoID = Convert.ToInt32(cmbDepto.SelectedValue.ToString());
                LlenarCiudad(deptoID);
            }
        }
        public void LlenarCiudad(int deptoID)
        {
            using (EmpresaEntities db = new EmpresaEntities())
            {
                CmbCiudad.DisplayMember = "nombreCiudad";
                CmbCiudad.ValueMember = "idCiudad";
                CmbCiudad.DataSource = db.tbl_ciudad.Where(c =>c.idDepto  == deptoID).ToList();

            }
        }
    }
}
