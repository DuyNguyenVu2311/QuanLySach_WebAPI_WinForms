namespace BookManagerWinForms;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnLoadAll = new Button();
        dgvBooks = new DataGridView();
        groupBoxAdd = new GroupBox();
        lblHint = new Label();
        btnBrowseImage = new Button();
        txtImagePath = new TextBox();
        label7 = new Label();
        cboCategory = new ComboBox();
        numQuantity = new NumericUpDown();
        numPrice = new NumericUpDown();
        txtAuthor = new TextBox();
        txtTitle = new TextBox();
        btnAddBook = new Button();
        label6 = new Label();
        label5 = new Label();
        label4 = new Label();
        label3 = new Label();
        label2 = new Label();
        lblStatus = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
        groupBoxAdd.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
        SuspendLayout();
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(24, 24);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Nhap ten sach, tac gia hoac the loai...";
        txtSearch.Size = new Size(390, 27);
        txtSearch.TabIndex = 0;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(430, 23);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(94, 29);
        btnSearch.TabIndex = 1;
        btnSearch.Text = "Tim kiem";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        // 
        // btnLoadAll
        // 
        btnLoadAll.Location = new Point(538, 23);
        btnLoadAll.Name = "btnLoadAll";
        btnLoadAll.Size = new Size(105, 29);
        btnLoadAll.TabIndex = 2;
        btnLoadAll.Text = "Tai tat ca";
        btnLoadAll.UseVisualStyleBackColor = true;
        btnLoadAll.Click += btnLoadAll_Click;
        // 
        // dgvBooks
        // 
        dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvBooks.Location = new Point(24, 72);
        dgvBooks.Name = "dgvBooks";
        dgvBooks.ReadOnly = true;
        dgvBooks.RowHeadersWidth = 51;
        dgvBooks.Size = new Size(770, 432);
        dgvBooks.TabIndex = 3;
        // 
        // groupBoxAdd
        // 
        groupBoxAdd.Controls.Add(btnBrowseImage);
        groupBoxAdd.Controls.Add(txtImagePath);
        groupBoxAdd.Controls.Add(label7);
        groupBoxAdd.Controls.Add(cboCategory);
        groupBoxAdd.Controls.Add(numQuantity);
        groupBoxAdd.Controls.Add(numPrice);
        groupBoxAdd.Controls.Add(txtAuthor);
        groupBoxAdd.Controls.Add(txtTitle);
        groupBoxAdd.Controls.Add(lblHint);
        groupBoxAdd.Controls.Add(btnAddBook);
        groupBoxAdd.Controls.Add(label6);
        groupBoxAdd.Controls.Add(label5);
        groupBoxAdd.Controls.Add(label4);
        groupBoxAdd.Controls.Add(label3);
        groupBoxAdd.Controls.Add(label2);
        groupBoxAdd.Location = new Point(818, 24);
        groupBoxAdd.Name = "groupBoxAdd";
        groupBoxAdd.Size = new Size(350, 520);
        groupBoxAdd.TabIndex = 4;
        groupBoxAdd.TabStop = false;
        groupBoxAdd.Text = "Them sach tu Web API";
        // 
        // lblHint
        // 
        lblHint.AutoSize = true;
        lblHint.ForeColor = Color.DimGray;
        lblHint.Location = new Point(22, 395);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(271, 20);
        lblHint.TabIndex = 14;
        lblHint.Text = "Nhap day du ten sach, tac gia, gia, so luong";
        // 
        // btnBrowseImage
        // 
        btnBrowseImage.Location = new Point(253, 360);
        btnBrowseImage.Name = "btnBrowseImage";
        btnBrowseImage.Size = new Size(75, 29);
        btnBrowseImage.TabIndex = 13;
        btnBrowseImage.Text = "Chon";
        btnBrowseImage.UseVisualStyleBackColor = true;
        btnBrowseImage.Click += btnBrowseImage_Click;
        // 
        // txtImagePath
        // 
        txtImagePath.Location = new Point(22, 361);
        txtImagePath.Name = "txtImagePath";
        txtImagePath.Size = new Size(217, 27);
        txtImagePath.TabIndex = 12;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Location = new Point(22, 338);
        label7.Name = "label7";
        label7.Size = new Size(67, 20);
        label7.TabIndex = 11;
        label7.Text = "Anh sach";
        // 
        // cboCategory
        // 
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.FormattingEnabled = true;
        cboCategory.Location = new Point(22, 301);
        cboCategory.Name = "cboCategory";
        cboCategory.Size = new Size(306, 28);
        cboCategory.TabIndex = 10;
        // 
        // numQuantity
        // 
        numQuantity.Location = new Point(22, 241);
        numQuantity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numQuantity.Name = "numQuantity";
        numQuantity.Size = new Size(306, 27);
        numQuantity.TabIndex = 9;
        numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // numPrice
        // 
        numPrice.Location = new Point(22, 181);
        numPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
        numPrice.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
        numPrice.Name = "numPrice";
        numPrice.Size = new Size(306, 27);
        numPrice.TabIndex = 8;
        numPrice.Value = new decimal(new int[] { 100000, 0, 0, 0 });
        // 
        // txtAuthor
        // 
        txtAuthor.Location = new Point(22, 117);
        txtAuthor.Name = "txtAuthor";
        txtAuthor.Size = new Size(306, 27);
        txtAuthor.TabIndex = 7;
        // 
        // txtTitle
        // 
        txtTitle.Location = new Point(22, 57);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(306, 27);
        txtTitle.TabIndex = 6;
        // 
        // btnAddBook
        // 
        btnAddBook.Location = new Point(22, 434);
        btnAddBook.Name = "btnAddBook";
        btnAddBook.Size = new Size(306, 36);
        btnAddBook.TabIndex = 5;
        btnAddBook.Text = "Them sach";
        btnAddBook.UseVisualStyleBackColor = true;
        btnAddBook.Click += btnAddBook_Click;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(22, 278);
        label6.Name = "label6";
        label6.Size = new Size(62, 20);
        label6.TabIndex = 4;
        label6.Text = "The loai";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(22, 218);
        label5.Name = "label5";
        label5.Size = new Size(67, 20);
        label5.TabIndex = 3;
        label5.Text = "So luong";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(22, 154);
        label4.Name = "label4";
        label4.Size = new Size(58, 20);
        label4.TabIndex = 2;
        label4.Text = "Gia ban";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(22, 94);
        label3.Name = "label3";
        label3.Size = new Size(53, 20);
        label3.TabIndex = 1;
        label3.Text = "Tac gia";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(22, 34);
        label2.Name = "label2";
        label2.Size = new Size(65, 20);
        label2.TabIndex = 0;
        label2.Text = "Ten sach";
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(24, 516);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(126, 20);
        lblStatus.TabIndex = 6;
        lblStatus.Text = "So sach hien tai: 0";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1194, 590);
        Controls.Add(lblStatus);
        Controls.Add(groupBoxAdd);
        Controls.Add(dgvBooks);
        Controls.Add(btnLoadAll);
        Controls.Add(btnSearch);
        Controls.Add(txtSearch);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quan ly sach bang Web API";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
        groupBoxAdd.ResumeLayout(false);
        groupBoxAdd.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnLoadAll;
    private DataGridView dgvBooks;
    private GroupBox groupBoxAdd;
    private Button btnAddBook;
    private Label label6;
    private Label label5;
    private Label label4;
    private Label label3;
    private Label label2;
    private NumericUpDown numQuantity;
    private NumericUpDown numPrice;
    private TextBox txtAuthor;
    private TextBox txtTitle;
    private ComboBox cboCategory;
    private TextBox txtImagePath;
    private Label label7;
    private Button btnBrowseImage;
    private Label lblStatus;
    private Label lblHint;
}
