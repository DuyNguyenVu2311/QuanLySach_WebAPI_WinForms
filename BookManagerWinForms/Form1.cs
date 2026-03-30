using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BookManagerWinForms;

public partial class Form1 : Form
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("http://localhost:9999/")
    };

    public Form1()
    {
        InitializeComponent();
    }

    private async void Form1_Load(object sender, EventArgs e)
    {
        await LoadCategoriesAsync();
        await LoadBooksAsync();
    }

    private async void btnLoadAll_Click(object sender, EventArgs e)
    {
        await LoadBooksAsync();
    }

    private async void btnSearch_Click(object sender, EventArgs e)
    {
        var keyword = txtSearch.Text.Trim();
        var url = string.IsNullOrWhiteSpace(keyword)
            ? "api/books"
            : $"api/books/search?keyword={Uri.EscapeDataString(keyword)}";

        await LoadBooksAsync(url);
    }

    private void btnBrowseImage_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp"
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtImagePath.Text = dialog.FileName;
        }
    }

    private async void btnAddBook_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            MessageBox.Show("Vui long nhap ten sach.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTitle.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtAuthor.Text))
        {
            MessageBox.Show("Vui long nhap tac gia.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtAuthor.Focus();
            return;
        }

        if (cboCategory.SelectedItem is not CategoryItem category)
        {
            MessageBox.Show("Vui long chon the loai.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(txtTitle.Text.Trim()), "Title");
        formData.Add(new StringContent(txtAuthor.Text.Trim()), "Author");
        formData.Add(new StringContent(numPrice.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)), "Price");
        formData.Add(new StringContent(numQuantity.Value.ToString()), "Quantity");
        formData.Add(new StringContent(category.Id.ToString()), "CategoryId");

        if (!string.IsNullOrWhiteSpace(txtImagePath.Text) && File.Exists(txtImagePath.Text))
        {
            var stream = File.OpenRead(txtImagePath.Text);
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            formData.Add(fileContent, "ImageFile", Path.GetFileName(txtImagePath.Text));
        }

        try
        {
            var response = await _httpClient.PostAsync("api/books", formData);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(json, "Loi them sach", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = JsonSerializer.Deserialize<CreateBookResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            MessageBox.Show(result?.Message ?? "Them sach thanh cong.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearAddForm();
            await LoadBooksAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Loi ket noi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadCategoriesAsync()
    {
        try
        {
            var categories = await _httpClient.GetFromJsonAsync<List<CategoryItem>>("api/categories") ?? new List<CategoryItem>();
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "Id";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Khong tai duoc the loai", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadBooksAsync(string url = "api/books")
    {
        try
        {
            var books = await _httpClient.GetFromJsonAsync<List<BookItem>>(url) ?? new List<BookItem>();
            dgvBooks.DataSource = books;
            lblStatus.Text = $"So sach hien tai: {books.Count}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Khong tai duoc danh sach sach", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearAddForm()
    {
        txtTitle.Clear();
        txtAuthor.Clear();
        txtImagePath.Clear();
        numPrice.Value = 100000;
        numQuantity.Value = 1;
    }
}
