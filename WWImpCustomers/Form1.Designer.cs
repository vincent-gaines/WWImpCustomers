using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using WWImpCustomers.Services;

namespace WWImpCustomers
{
    public partial class Form1 : Form
    {
        private readonly ICustomerService _service;

        public Form1(ICustomerService service)
        {
            InitializeComponent();
            _service = service;
        }

        

        private TextBox txtCustomerID;
        private TextBox txtCustomerName;
        private TextBox txtPhoneNumber;
        private TextBox txtFaxNumber;
        private TextBox txtWebsiteURL;
        private DataGridView dgvCustomers;
        private DataGridViewTextBoxColumn dgvCustomerID;
        private DataGridViewTextBoxColumn dgvCustomerName;
        private DataGridViewTextBoxColumn dgvPhoneNumber;
        private DataGridViewTextBoxColumn gdcFaxNumber;
        private DataGridViewTextBoxColumn dgvWebsiteURL;
        private Button btnAdd;
        private Button btnLoad;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private ComboBox cmbCustomerCategory;
        private ComboBox cmbPrimaryContact;
        private ComboBox cmbDeliveryMethod;
        private ComboBox cmbDeliveryCity;
        private GroupBox groupBox1;
        private Label label7;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClearSearch;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
    }


}

