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
    public partial class Form1 : Form
    {
        FormCompanyEdit forma;
        public static tbl_empresa una = new tbl_empresa();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            llenarGrid();
            dgvEmpresa.CurrentRow.Selected = false;
        }

        public void llenarGrid()
        {
            using(EmpresaEntities db = new EmpresaEntities())
            {
                dgvEmpresa.DataSource = db.sp_listarEmpresas().ToList();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (forma == null)
            {
                forma = new FormCompanyEdit();
                forma.LabelText = "Agregar Nueva Emprsa";
                forma.FormClosed += new FormClosedEventHandler(CerrarForma);
                forma.Show();
            }
            else
            {
                forma.Activate();
            }

        }

        public void CerrarForma(object sender, FormClosedEventArgs e)
        {
            forma = null;
            dgvEmpresa.CurrentRow.Selected = false;
            llenarGrid();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            if (forma == null)
            {
                using (EmpresaEntities db = new EmpresaEntities())
                {
                    forma = new FormCompanyEdit();
                    tbl_empresa emp = new tbl_empresa();
                    emp = db.tbl_empresa.Find(Convert.ToInt32(dgvEmpresa.CurrentRow.Cells[0].Value.ToString()));
                    tbl_ciudad city = new tbl_ciudad();
                    city = db.tbl_ciudad.Find(emp.idCiudad);
                    tbl_depto dep = new tbl_depto();
                    dep = db.tbl_depto.Find(city.idDepto);
                    forma.cmbDepto.DataSource = db.tbl_depto.Where(d => d.idDepto == city.idDepto).ToList();
                    forma.cmbDepto.DisplayMember = "nombreDepto";
                    forma.cmbDepto.ValueMember = "idDepto";

                    forma.CmbCiudad.DataSource = db.tbl_ciudad.Where(c=>c.idCiudad == emp.idCiudad).ToList();                    
                    forma.CmbCiudad.DisplayMember = "nombreCiudad";
                    forma.CmbCiudad.ValueMember = "idCiudad";
                    forma.txtId.Text = dgvEmpresa.CurrentRow.Cells[0].Value.ToString();
                    forma.txtName.Text = dgvEmpresa.CurrentRow.Cells[1].Value.ToString();
                    forma.txtDir.Text = dgvEmpresa.CurrentRow.Cells[2].Value.ToString();
                    forma.txtTel.Text = dgvEmpresa.CurrentRow.Cells[3].Value.ToString();
                    forma.LabelText = "Modificar Emprsa";
                    forma.FormClosed += new FormClosedEventHandler(CerrarForma);
                    forma.Show();
                }
            }
            else
            {
                forma.Activate();
            }
            

            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using(EmpresaEntities db = new EmpresaEntities())
            {
                dgvEmpresa.DataSource = null;
                dgvEmpresa.DataSource = db.sp_listarEmpresasSearch(txtSearch.Text).ToList();
            }
        }
    }
}
