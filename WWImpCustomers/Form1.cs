using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WWImpCustomers.Data;
using WWImpCustomers.Models;
using WWImpCustomers.Services;

namespace WWImpCustomers
{
    public partial class Form1 : Form
    {
        private string _connectionString =
            "Server=YOUR_SERVER_NAME;Database=WideWorldImporters;Integrated Security=true;";
     
        private DataTable _customersTable;

        private readonly ICustomerService _customerService;
        private readonly ILookupRepository _lookupRepo;
        private readonly ICustomerRepository _repo;


        public Form1(ICustomerService customerService, ILookupRepository lookupRepo)
        {
            InitializeComponent();
            _customerService = customerService;
            _lookupRepo = lookupRepo;
        }

      
        private void LoadCustomers()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT CustomerID, CustomerName, PhoneNumber, FaxNumber, WebsiteURL
          FROM Sales.Customers", conn))
            {
                _customersTable = new DataTable();
                da.Fill(_customersTable);
                dgvCustomers.DataSource = _customersTable;
            }

            dgvCustomers.AutoResizeColumns();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadLookupsAsync();
            await LoadCustomersAsync();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private async Task LoadLookupsAsync()
        {
            // Customer Categories
            var categories = await _lookupRepo.GetCustomerCategoriesAsync();
            cmbCustomerCategory.DataSource = categories.ToList();
            cmbCustomerCategory.DisplayMember = "Name";
            cmbCustomerCategory.ValueMember = "Id";

            // People (Primary Contact)
            var people = await _lookupRepo.GetPeopleAsync();
            cmbPrimaryContact.DataSource = people.ToList();
            cmbPrimaryContact.DisplayMember = "FullName";
            cmbPrimaryContact.ValueMember = "Id";

            // Delivery Methods
            var methods = await _lookupRepo.GetDeliveryMethodsAsync();
            cmbDeliveryMethod.DataSource = methods.ToList();
            cmbDeliveryMethod.DisplayMember = "Name";
            cmbDeliveryMethod.ValueMember = "Id";

            // Cities
            var cities = await _lookupRepo.GetCitiesAsync();
            cmbDeliveryCity.DataSource = cities.ToList();
            cmbDeliveryCity.DisplayMember = "Name";
            cmbDeliveryCity.ValueMember = "Id";
        }

        private async Task LoadCustomersAsync()
        {
            var customers = await _customerService.GetAllAsync();
            dgvCustomers.DataSource = customers.ToList();
        }



        private async Task LoadGrid()
        {
            var customers = await _customerService.GetAllAsync();
            dgvCustomers.DataSource = customers.ToList();
        }

        private Customer BuildCustomerFromInputs()
        {
            return new Customer
            {
                CustomerID = string.IsNullOrWhiteSpace(txtCustomerID.Text)
                    ? 0
                    : int.Parse(txtCustomerID.Text),
                CustomerName = txtCustomerName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                FaxNumber = txtFaxNumber.Text,
                WebsiteURL = txtWebsiteURL.Text
            };
        }

     
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var c = BuildCustomerFromInputs();
            await _service.CreateAsync(c);
            dgvCustomers.DataSource = (await _service.GetAllAsync()).ToList();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            var c = BuildCustomerFromInputs();
            await _service.UpdateAsync(c);
            dgvCustomers.DataSource = (await _service.GetAllAsync()).ToList();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCustomerID.Text, out int id))
            {
                await _service.DeleteAsync(id);
                dgvCustomers.DataSource = (await _service.GetAllAsync()).ToList();
            }
        }


        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;

            txtCustomerID.Text = dgvCustomers.CurrentRow.Cells["CustomerID"].Value?.ToString();
            txtCustomerName.Text = dgvCustomers.CurrentRow.Cells["CustomerName"].Value?.ToString();
            txtPhoneNumber.Text = dgvCustomers.CurrentRow.Cells["PhoneNumber"].Value?.ToString();
            txtFaxNumber.Text = dgvCustomers.CurrentRow.Cells["FaxNumber"].Value?.ToString();
            txtWebsiteURL.Text = dgvCustomers.CurrentRow.Cells["WebsiteURL"].Value?.ToString();
        }

        private void InitializeComponent()
        {
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtFaxNumber = new System.Windows.Forms.TextBox();
            this.txtWebsiteURL = new System.Windows.Forms.TextBox();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.dgvCustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gdcFaxNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvWebsiteURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbCustomerCategory = new System.Windows.Forms.ComboBox();
            this.cmbPrimaryContact = new System.Windows.Forms.ComboBox();
            this.cmbDeliveryMethod = new System.Windows.Forms.ComboBox();
            this.cmbDeliveryCity = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Location = new System.Drawing.Point(229, 48);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.ReadOnly = true;
            this.txtCustomerID.Size = new System.Drawing.Size(250, 20);
            this.txtCustomerID.TabIndex = 0;
            this.txtCustomerID.Tag = "";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(229, 90);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(250, 20);
            this.txtCustomerName.TabIndex = 1;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(229, 141);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(250, 20);
            this.txtPhoneNumber.TabIndex = 2;
            // 
            // txtFaxNumber
            // 
            this.txtFaxNumber.Location = new System.Drawing.Point(229, 184);
            this.txtFaxNumber.Name = "txtFaxNumber";
            this.txtFaxNumber.Size = new System.Drawing.Size(250, 20);
            this.txtFaxNumber.TabIndex = 3;
            // 
            // txtWebsiteURL
            // 
            this.txtWebsiteURL.Location = new System.Drawing.Point(229, 245);
            this.txtWebsiteURL.Name = "txtWebsiteURL";
            this.txtWebsiteURL.Size = new System.Drawing.Size(250, 20);
            this.txtWebsiteURL.TabIndex = 4;
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCustomerID,
            this.dgvCustomerName,
            this.dgvPhoneNumber,
            this.gdcFaxNumber,
            this.dgvWebsiteURL});
            this.dgvCustomers.Location = new System.Drawing.Point(115, 612);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersWidth = 82;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new System.Drawing.Size(1174, 68);
            this.dgvCustomers.TabIndex = 5;
            // 
            // dgvCustomerID
            // 
            this.dgvCustomerID.HeaderText = "Customer ID";
            this.dgvCustomerID.MinimumWidth = 10;
            this.dgvCustomerID.Name = "dgvCustomerID";
            this.dgvCustomerID.ReadOnly = true;
            // 
            // dgvCustomerName
            // 
            this.dgvCustomerName.HeaderText = "Customer Name";
            this.dgvCustomerName.MinimumWidth = 10;
            this.dgvCustomerName.Name = "dgvCustomerName";
            this.dgvCustomerName.ReadOnly = true;
            // 
            // dgvPhoneNumber
            // 
            this.dgvPhoneNumber.HeaderText = "Phone Number";
            this.dgvPhoneNumber.MinimumWidth = 10;
            this.dgvPhoneNumber.Name = "dgvPhoneNumber";
            this.dgvPhoneNumber.ReadOnly = true;
            // 
            // gdcFaxNumber
            // 
            this.gdcFaxNumber.HeaderText = "Fax Number";
            this.gdcFaxNumber.MinimumWidth = 10;
            this.gdcFaxNumber.Name = "gdcFaxNumber";
            this.gdcFaxNumber.ReadOnly = true;
            // 
            // dgvWebsiteURL
            // 
            this.dgvWebsiteURL.HeaderText = "Website URL";
            this.dgvWebsiteURL.MinimumWidth = 10;
            this.dgvWebsiteURL.Name = "dgvWebsiteURL";
            this.dgvWebsiteURL.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(195, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 70);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(28, 0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(110, 70);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(362, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 70);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(521, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(101, 70);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(683, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 70);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Customer ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Customer Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Phone Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Fax Number:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Website URL:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(731, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Delivery City:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(731, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Delivery Method:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(731, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Primary Contact:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(731, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Customer Category:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(741, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 22;
            // 
            // cmbCustomerCategory
            // 
            this.cmbCustomerCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerCategory.DropDownWidth = 250;
            this.cmbCustomerCategory.FormattingEnabled = true;
            this.cmbCustomerCategory.Location = new System.Drawing.Point(948, 62);
            this.cmbCustomerCategory.Name = "cmbCustomerCategory";
            this.cmbCustomerCategory.Size = new System.Drawing.Size(250, 21);
            this.cmbCustomerCategory.TabIndex = 27;
            // 
            // cmbPrimaryContact
            // 
            this.cmbPrimaryContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrimaryContact.DropDownWidth = 250;
            this.cmbPrimaryContact.FormattingEnabled = true;
            this.cmbPrimaryContact.Location = new System.Drawing.Point(948, 106);
            this.cmbPrimaryContact.Name = "cmbPrimaryContact";
            this.cmbPrimaryContact.Size = new System.Drawing.Size(250, 21);
            this.cmbPrimaryContact.TabIndex = 28;
            // 
            // cmbDeliveryMethod
            // 
            this.cmbDeliveryMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeliveryMethod.DropDownWidth = 250;
            this.cmbDeliveryMethod.FormattingEnabled = true;
            this.cmbDeliveryMethod.Location = new System.Drawing.Point(948, 155);
            this.cmbDeliveryMethod.Name = "cmbDeliveryMethod";
            this.cmbDeliveryMethod.Size = new System.Drawing.Size(250, 21);
            this.cmbDeliveryMethod.TabIndex = 29;
            // 
            // cmbDeliveryCity
            // 
            this.cmbDeliveryCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeliveryCity.DropDownWidth = 250;
            this.cmbDeliveryCity.FormattingEnabled = true;
            this.cmbDeliveryCity.Location = new System.Drawing.Point(948, 204);
            this.cmbDeliveryCity.Name = "cmbDeliveryCity";
            this.cmbDeliveryCity.Size = new System.Drawing.Size(250, 21);
            this.cmbDeliveryCity.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Location = new System.Drawing.Point(407, 342);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(856, 100);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(115, 485);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "label7";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(270, 482);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 33;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(497, 479);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 34;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(783, 497);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(75, 23);
            this.btnClearSearch.TabIndex = 35;
            this.btnClearSearch.Text = "Clear";
            this.btnClearSearch.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(254, 485);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(10, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "[";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(376, 489);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(10, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "[";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(578, 485);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "[";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(848, 502);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 13);
            this.label16.TabIndex = 39;
            this.label16.Text = "[";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(481, 485);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(10, 13);
            this.label17.TabIndex = 40;
            this.label17.Text = "[";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(780, 497);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(10, 13);
            this.label18.TabIndex = 41;
            this.label18.Text = "[";
            // 
            // lblSearch
            // 
            this.ClientSize = new System.Drawing.Size(1670, 776);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbDeliveryCity);
            this.Controls.Add(this.cmbDeliveryMethod);
            this.Controls.Add(this.cmbPrimaryContact);
            this.Controls.Add(this.cmbCustomerCategory);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.txtWebsiteURL);
            this.Controls.Add(this.txtFaxNumber);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtCustomerID);
            this.Name = "lblSearch";
            this.Text = "Search :";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null) return;

            txtCustomerID.Text = dgvCustomers.CurrentRow.Cells["CustomerID"].Value?.ToString();
            txtCustomerName.Text = dgvCustomers.CurrentRow.Cells["CustomerName"].Value?.ToString();
            txtPhoneNumber.Text = dgvCustomers.CurrentRow.Cells["PhoneNumber"].Value?.ToString();
            txtFaxNumber.Text = dgvCustomers.CurrentRow.Cells["FaxNumber"].Value?.ToString();
            txtWebsiteURL.Text = dgvCustomers.CurrentRow.Cells["WebsiteURL"].Value?.ToString();

            cmbCustomerCategory.SelectedValue =
                dgvCustomers.CurrentRow.Cells["CustomerCategoryID"].Value;

            cmbPrimaryContact.SelectedValue =
                dgvCustomers.CurrentRow.Cells["PrimaryContactPersonID"].Value;

            cmbDeliveryMethod.SelectedValue =
                dgvCustomers.CurrentRow.Cells["DeliveryMethodID"].Value;

            cmbDeliveryCity.SelectedValue =
                dgvCustomers.CurrentRow.Cells["DeliveryCityID"].Value;
        }
    }
}